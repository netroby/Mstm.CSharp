using Microsoft.Extensions.Configuration;
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

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public virtual string RedisConnStr
        {
            get
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json").Build();
                return config["Redis:RedisClientConnStr"];

            }
        }
    }
}
