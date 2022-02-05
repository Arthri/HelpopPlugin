using HelpopPlugin.Redis.Messages;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;
using System.Threading.Tasks;
using TShockAPI;

namespace HelpopPlugin.Redis
{
    /// <summary>
    /// Manages the connection to Redis.
    /// </summary>
    public static class RedisConnector
    {
        private static class Issues_Raise
        {
            internal static readonly string ChannelName = "helpopplugin-issues-raise";

            internal static async Task Subscribe(ConnectionMultiplexer multiplexer)
            {
                var subscriber = multiplexer.GetSubscriber();
                await subscriber.SubscribeAsync(ChannelName, OnMessage);
            }

            private static void OnMessage(RedisChannel channel, RedisValue value)
            {
                try
                {
                    var jsonData = (string)value;
                    var message = JsonConvert.DeserializeObject<IssueRaiseMessage>(jsonData);

                    if (message.Source.Equals(ServerIdentifier))
                    {
                        return;
                    }

                    Events.InvokeOnIssue(message.IssuedIssue);
                }
                catch (Exception e)
                {
                    TShock.Log.Error($"Error while parsing message: {e}");
                }
            }
        }

        /// <summary>
        /// Sends an issue to redis.
        /// </summary>
        /// <param name="issue">The issue to send.</param>
        /// <returns>The count of the message receivers.</returns>
        public static async Task<long> SendIssueAsync(Issue issue)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            var message = new IssueRaiseMessage(ServerIdentifier, new RedisIssue(issue, ServerIdentifier));
            var jsonData = JsonConvert.SerializeObject(message);
            var result = await subscriber.PublishAsync(Issues_Raise.ChannelName, jsonData);
            return result;
        }

        private static ConnectionMultiplexer _connectionMultiplexer;

        /// <summary>
        /// Used to identify this server.
        /// </summary>
        public static readonly string ServerIdentifier = Guid.NewGuid().ToString();

        /// <summary>
        /// Connects to Redis.
        /// </summary>
        /// <param name="config">A function that returns the configuration.</param>
        /// <param name="logger">A logger to log to.</param>
        /// <returns>A task representing this operation.</returns>
        public static async Task ConnectAsync(Func<ConfigurationOptions> config, TextWriter logger = null)
        {
            if (_connectionMultiplexer != null && (_connectionMultiplexer.IsConnected || _connectionMultiplexer.IsConnecting))
            {
                return;
            }

            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(config(), logger);
            await SubscribeAll(_connectionMultiplexer);
        }

        /// <summary>
        /// Disconnects from redis.
        /// </summary>
        public static void Disconnect()
        {
            _connectionMultiplexer.Dispose();
            _connectionMultiplexer = null;
        }

        private static async Task SubscribeAll(ConnectionMultiplexer multiplexer)
        {
            await Task.WhenAll(
                Issues_Raise.Subscribe(multiplexer)
            );
        }
    }
}
