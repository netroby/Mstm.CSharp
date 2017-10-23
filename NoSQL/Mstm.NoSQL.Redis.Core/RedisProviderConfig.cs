using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{
    /// <summary>
    /// Redis组件配置文件信息
    /// </summary>
    public class RedisProviderConfig
    {
        private string _groupName;
        /// <summary>
        /// 默认组名称
        /// </summary>
        public const string DefaultGroupName = "Default";

        /// <summary>
        /// 配置信息
        /// </summary>
        static IConfigurationRoot _config;

        /// <summary>
        /// Redis模块名称
        /// </summary>
        public const string ModuleName = "RedisProvider";

        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ConfigFile = "appsettings.json";

        /// <summary>
        /// 私有构造函数，禁止外部构造
        /// </summary>
        /// <param name="groupName">组名称</param>
        private RedisProviderConfig(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = DefaultGroupName; }
            _groupName = groupName;
        }

        /// <summary>
        /// 静态构造函数，加载配置文件
        /// </summary>
        static RedisProviderConfig()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile(ConfigFile).Build();
        }

        /// <summary>
        /// 创建一个新的配置
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
        public static RedisProviderConfig New(string groupName = null)
        {
            return new RedisProviderConfig(groupName);
        }

        /// <summary>
        /// IRedisProvider实现的程序集名称
        /// </summary>
        public string AssemblyName
        {
            get { return _config[string.Format("{0}:{1}:AssemblyName", ModuleName, _groupName)]; }
        }

        /// <summary>
        /// IRedisProvider实现的类型全称
        /// </summary>
        public string ClassFullName
        {
            get { return _config[string.Format("{0}:{1}:ClassFullName", ModuleName, _groupName)]; }
        }

        /// <summary>
        /// Redis的连接字符串
        /// </summary>
        public string RedisClientConnStr
        {
            get { return _config[string.Format("{0}:{1}:RedisClientConnStr", ModuleName, _groupName)]; }
        }

        /// <summary>
        /// Redis DB信息
        /// </summary>
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
