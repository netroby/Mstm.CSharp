using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Log.Core
{
    /// <summary>
    /// 日志配置信息
    /// </summary>
    public class LogConfig
    {
        private string _groupName;

        /// <summary>
        /// 默认组名称
        /// </summary>
        public const string DefaultGroupName = "Default";

        /// <summary>
        /// 配置信息
        /// </summary>
        private static IConfigurationRoot _config;

        /// <summary>
        /// 当前模块名称
        /// </summary>
        public const string ModuleName = "Logger";

        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ConfigFile = "appsettings.json";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupName">组名称</param>
        private LogConfig(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = DefaultGroupName; }
            _groupName = groupName;
        }

        /// <summary>
        /// 静态构造函数
        /// 加载配置文件
        /// </summary>
        static LogConfig()
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
        public static LogConfig New(string groupName = null)
        {
            return new LogConfig(groupName);
        }

        /// <summary>
        /// ILogProvider实现类所在的程序集名称
        /// </summary>
        public string AssemblyName
        {
            get { return _config[string.Format("{0}:{1}:AssemblyName", ModuleName, _groupName)]; }
        }

        /// <summary>
        /// ILogProvider实现类的全称
        /// </summary>
        public string ClassFullName
        {
            get { return _config[string.Format("{0}:{1}:ClassFullName", ModuleName, _groupName)]; }
        }
    }
}
