using StackExchange.Redis;
using System;
using System.Diagnostics;
using TerrariaApi.Server;
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
        }

        private void Dispose_Redis()
        {
            RedisConnector.Disconnect();
            ServerApi.Hooks.GamePostInitialize.Deregister(this, OnGamePostInitialize_Redis);
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
    }
}
