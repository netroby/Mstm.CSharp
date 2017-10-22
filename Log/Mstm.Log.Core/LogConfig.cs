using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Log.Core
{
    public class LogConfig
    {
        private string _groupName;
        private const string DefaultGroupName = "Default";
        private static IConfigurationRoot _config;
        public const string ModuleName = "Logger";
        public const string ConfigFile = "appsettings.json";

        private LogConfig(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = DefaultGroupName; }
            _groupName = groupName;
        }

        static LogConfig()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json").Build();
        }

        /// <summary>
        /// 创建一个新的配置
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static LogConfig New(string groupName = null)
        {
            return new LogConfig(groupName);
        }

        public string AssemblyName
        {
            get { return _config[string.Format("{0}:{1}:AssemblyName", ModuleName, _groupName)]; }
        }

        public string ClassFullName
        {
            get { return _config[string.Format("{0}:{1}:ClassFullName", ModuleName, _groupName)]; }
        }
    }
}
