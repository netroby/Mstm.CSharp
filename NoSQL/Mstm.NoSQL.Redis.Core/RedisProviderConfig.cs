using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{
    public class RedisProviderConfig
    {
        private string _groupName;
        static IConfigurationRoot _config;
        public const string ModuleName = "RedisProvider";
        public const string ConfigFile = "appsettings.json";

        public RedisProviderConfig(string groupName)
        {
            _groupName = groupName;
        }

        static RedisProviderConfig()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json").Build();
        }

        public string AssemblyName
        {
            get { return _config[string.Format("{0}:{1}:AssemblyName", ModuleName, _groupName)]; }
        }

        public string ClassFullName
        {
            get { return _config[string.Format("{0}:{1}:ClassFullName", ModuleName, _groupName)]; }
        }

        public string RedisClientConnStr
        {
            get { return _config[string.Format("{0}:{1}:RedisClientConnStr", ModuleName, _groupName)]; }
        }

        public int DB
        {
            get
            {
                try
                {
                    int db = int.Parse(_config[string.Format("{0}:{1}:DB", ModuleName, _groupName)]);
                    return db;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
