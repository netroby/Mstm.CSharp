using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using System.Configuration;
using Mstm.NoSQL.Redis.Core;
using Xunit;

namespace Mstm.NoSQL.Redis.StackExchange.Tests
{

    /// <summary>
    /// IRedisString接口测试
    /// </summary>
    public class IRedisStringTests
    {
        private static string _redisClientConnStr = ConfigurationManager.AppSettings["RedisClientConnStr"];

        private IRedisString _provider;


        [Fact]
        public void GetStringTest()
        {
            string key = "a";
            _provider = new StackExchangeRedisProvider(_redisClientConnStr, 0);
            string result = _provider.GetString(key);
            result.ShouldNotBeNullOrWhiteSpace(result);
        }
    }
}
