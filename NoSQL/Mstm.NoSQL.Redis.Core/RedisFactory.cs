using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.NoSQL.Redis.Core
{
    public class RedisFactory
    {
        private static ConcurrentDictionary<string, IRedisProvider> _providerDict = new ConcurrentDictionary<string, IRedisProvider>();


        public static IRedisProvider GetProvider(string groupName = null)
        {
            IRedisProvider provider = null;
            if (_providerDict.ContainsKey(groupName))
            {
                _providerDict.TryGetValue(groupName, out provider);
                if (provider != null) { return provider; }
            }
            RedisProviderConfig config = RedisProviderConfig.New(groupName);

            if (string.IsNullOrEmpty(config.AssemblyName))
            {
                throw new ArgumentNullException(nameof(config.AssemblyName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的程序集名称，请检查配置文件{4}", RedisProviderConfig.ModuleName, groupName, nameof(RedisProviderConfig.AssemblyName), typeof(IRedisProvider).FullName, RedisProviderConfig.ConfigFile));
            }
            if (string.IsNullOrEmpty(config.ClassFullName))
            {
                throw new ArgumentNullException(nameof(config.ClassFullName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的类名，请检查配置文件{4}", RedisProviderConfig.ModuleName, groupName, nameof(RedisProviderConfig.ClassFullName), typeof(IRedisProvider).FullName, RedisProviderConfig.ConfigFile));
            }
            var assembly = Assembly.Load(config.AssemblyName);
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly), string.Format("未找到{0}程序集", config.AssemblyName)); }
            provider = assembly.CreateInstance(config.ClassFullName, true, BindingFlags.CreateInstance, null, new object[] { config.RedisClientConnStr, config.DB }, CultureInfo.CurrentCulture, null) as IRedisProvider;
            if (provider == null) { throw new ArgumentNullException(nameof(provider), string.Format("实例化类型{0}失败", config.ClassFullName)); }
            _providerDict.TryAdd(groupName, provider);
            return provider;
        }
    }
}
