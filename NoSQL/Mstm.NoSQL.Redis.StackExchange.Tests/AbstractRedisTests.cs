using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.StackExchange.Tests
{
    public abstract class AbstractRedisTests
    {

        private static string _redisClientConnStr = ConfigurationManager.AppSettings["RedisClientConnStr"];

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public virtual string RedisConnStr
        {
            get { return _redisClientConnStr; }
        }
    }
}
