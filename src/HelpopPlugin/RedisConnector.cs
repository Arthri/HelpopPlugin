using HelpopAPI.Core;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HelpopPlugin
{
    public static class RedisConnector
    {
        private static class ChannelNames
        {
            public static readonly string Issues = "helpopapi-issues";
        }

        private static ConnectionMultiplexer _connectionMultiplexer;

        /// <summary>
        /// Connects to Redis.
        /// </summary>
        /// <param name="configure">An action to change the configuration.</param>
        /// <param name="logger">A logger to log to.</param>
        /// <returns>A task representing this operation.</returns>
        public static async Task ConnectAsync(Action<ConfigurationOptions> configure, TextWriter logger = null)
        {
            if (_connectionMultiplexer != null && (_connectionMultiplexer.IsConnected || _connectionMultiplexer.IsConnecting))
            {
                return;
            }

            var config = new ConfigurationOptions();
            configure(config);
            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(config, logger);
            await Subscribe(_connectionMultiplexer);
        }

        /// <summary>
        /// Disconnects from redis.
        /// </summary>
        public static void Disconnect()
        {
            _connectionMultiplexer.Dispose();
            _connectionMultiplexer = null;
        }

        private static async Task Subscribe(ConnectionMultiplexer multiplexer)
        {
            var subscriber = multiplexer.GetSubscriber();
            await subscriber.SubscribeAsync(ChannelNames.Issues, OnMessage);
        }

        private static void OnMessage(RedisChannel channel, RedisValue message)
        {
            var jsonData = (string)message;
            var issue = JsonConvert.DeserializeObject<Issue>(jsonData);

            Events.InvokeOnIssue(issue);
        }

        /// <summary>
        /// Sends an issue to redis.
        /// </summary>
        /// <param name="issue">The issue to send.</param>
        /// <returns>The count of the message receivers.</returns>
        public static async Task<long> SendIssueAsync(Issue issue)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            var jsonData = JsonConvert.SerializeObject(issue);
            var result = await subscriber.PublishAsync(ChannelNames.Issues, jsonData);
            return result;
        }
    }
}
