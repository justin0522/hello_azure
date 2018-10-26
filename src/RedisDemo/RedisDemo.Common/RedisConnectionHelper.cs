using StackExchange.Redis;
using System;
using System.Configuration;

namespace RedisDemo.Common
{
    class RedisConnectionHelper
    {
        static string ConfigurationString = ConfigurationManager.AppSettings["RedisConnection"];

        public static ConnectionMultiplexer Connection
        {
            get { return GetConnection().Value; }
        }

        public static Lazy<ConnectionMultiplexer> GetConnection(string connectionString = null)
        {
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                if (connectionString == null)
                {
                    connectionString = ConfigurationString;
                }
                var options = ConfigurationOptions.Parse(connectionString);
                options.AbortOnConnectFail = false;
                options.AllowAdmin = true;
                return ConnectionMultiplexer.Connect(options);
            });
        }
    }
}
