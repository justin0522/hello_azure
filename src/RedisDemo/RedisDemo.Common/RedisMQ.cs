using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisDemo.Common
{
    public interface IRedisMQ
    {
        void Push(RedisKey key, RedisValue value, RedisChannel channel);

        void Subscribe(RedisKey key, WaitCallback callback, RedisChannel channel);
    }
    public class RedisMQ : IRedisMQ
    {
        public IConnectionMultiplexer Connection { get { return RedisConnectionHelper.Connection; } }
        public ISubscriber Subscriber { get { return Connection.GetSubscriber(); } }
        public IDatabase Database { get { return Connection.GetDatabase(); } }

        public void Push(RedisKey key, RedisValue value, RedisChannel channel)
        {
            Database.ListLeftPush(key, value, flags: CommandFlags.FireAndForget);
            Subscriber.Publish(channel, RedisValue.EmptyString);
        }

        public void Subscribe(RedisKey key, WaitCallback callback, RedisChannel channel)
        {
            Subscriber.Subscribe(channel, (c, v) =>
            {
                var value = Database.ListRightPop(key);
                if (!value.IsNullOrEmpty)
                {
                    callback(value);
                }
            });
        }
    }
}
