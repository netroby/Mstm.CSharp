using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Mstm.Log.Core
{
    public class LogFactory
    {
        private static ConcurrentDictionary<string, ILogProvider> _loggerDict = new ConcurrentDictionary<string, ILogProvider>();

        public static ILogProvider GetLogger(Type type, string groupName = null)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = LogConfig.DefaultGroupName; }
            if (type == null) { type = typeof(LogFactory); }
            ILogProvider logger = null;
            string key = groupName + "_" + type.FullName;
            if (_loggerDict.ContainsKey(key))
            {
                _loggerDict.TryGetValue(key, out logger);
                if (logger != null) { return logger; }
            }
            LogConfig config = LogConfig.New(groupName);

            if (string.IsNullOrEmpty(config.AssemblyName))
            {
                throw new ArgumentNullException(nameof(config.AssemblyName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的程序集名称，请检查配置文件{4}", LogConfig.ModuleName, groupName, nameof(LogConfig.AssemblyName), typeof(ILogProvider).FullName, LogConfig.ConfigFile));
            }
            if (string.IsNullOrEmpty(config.ClassFullName))
            {
                throw new ArgumentNullException(nameof(config.ClassFullName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的类名，请检查配置文件{4}", LogConfig.ModuleName, groupName, nameof(LogConfig.ClassFullName), typeof(ILogProvider).FullName, LogConfig.ConfigFile));
            }
            var assembly = Assembly.Load(config.AssemblyName);
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly), string.Format("未找到{0}程序集", config.AssemblyName)); }
            logger = assembly.CreateInstance(config.ClassFullName, true, BindingFlags.CreateInstance, null, new object[] { type }, CultureInfo.CurrentCulture, null) as ILogProvider;
            if (logger == null) { throw new ArgumentNullException(nameof(logger), string.Format("实例化类型{0}失败", config.ClassFullName)); }
            _loggerDict.TryAdd(key, logger);
            return logger;
        }

        public static ILogProvider GetLogger<T>(string groupName = null)
        {
            Type type = typeof(T);
            return GetLogger(type, groupName);
        }
    }
}
