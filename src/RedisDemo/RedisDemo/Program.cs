using Newtonsoft.Json;
using RedisDemo.Common;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            IRedisMQ queue = new RedisMQ();

            queue.Subscribe("test", (obj) => { Console.WriteLine(obj); }, "flag_channel");

            for (int i = 0; i < 5; i++)
            {
                queue.Push("test", "test value " + i, "flag_channel");
            }

            Console.ReadLine();
        }

        static string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }

        static T ConvertObj<T>(RedisValue value)
        {
            if (value.IsNullOrEmpty)
                return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        static List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                result.Add(model);
            }
            return result;
        }

        static RedisKey[] ConvertRedisKeys(List<string> redisKeys)
        {
            return redisKeys.Select(redisKey => (RedisKey)redisKey).ToArray();
        }
    }
}
