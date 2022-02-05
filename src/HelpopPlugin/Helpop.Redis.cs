using HelpopPlugin.Redis;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using static HelpopPlugin.Configuration.RedisSettings;

namespace HelpopPlugin
{
    public partial class Helpop
    {
        private void Initialize_Redis()
        {
            ConnectionTimeEnum connectionTime = RedisSettings.ConnectionTime;
            switch (connectionTime)
            {
                case ConnectionTimeEnum.Initialization:
                    ConnectRedis();
                    break;
                case ConnectionTimeEnum.ServerStarted:
                    ServerApi.Hooks.GamePostInitialize.Register(this, OnGamePostInitialize_Redis);
                    break;
                default:
                    ServerApi.LogWriter.PluginWriteLine(
                        this,
                        $"Fatal error. Unrecognized {nameof(ConnectionTimeEnum)} value: {connectionTime}",
                        TraceLevel.Error);
                    break;
            }

            Events.OnIssue += HandleOnIssue_RedisBroadcast;
        }

        private void Dispose_Redis()
        {
            RedisConnector.Disconnect();
            ServerApi.Hooks.GamePostInitialize.Deregister(this, OnGamePostInitialize_Redis);
            Events.OnIssue -= HandleOnIssue_RedisBroadcast;
        }

        private void ConnectRedis()
        {
            try
            {
                RedisConnector
                    .ConnectAsync(
                        () => ConfigurationOptions.Parse(RedisSettings.ConfigString)
                    )
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception e)
            {
                ServerApi.LogWriter.PluginWriteLine(this, "Fatal error. Unable to connect to Redis", TraceLevel.Info);
                ServerApi.LogWriter.PluginWriteLine(this, e.ToString(), TraceLevel.Error);
            }
        }

        private void OnGamePostInitialize_Redis(EventArgs args)
        {
            ConnectRedis();
        }

        private void HandleOnIssue_RedisBroadcast(OnIssueEventArgs args)
        {
            var issue = args.IssuedIssue;

            if (args.IssuedIssue is RedisIssue)
            {
                return;
            }

            async Task SendIssue()
            {
                try
                {
                    await RedisConnector.SendIssueAsync(issue);
                }
                catch (Exception e)
                {
                    Main.QueueMainThreadAction(() => TShock.Log.Error($"Error while sending report: {e}"));
                }
            }

            Task.Run(SendIssue);
        }
    }
}
