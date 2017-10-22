using Mstm.Common.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Factory
{
    public abstract class Factory<T>
    {
        private static ConcurrentDictionary<string, T> _providerDict = new ConcurrentDictionary<string, T>();

        protected abstract T CreateInstance(Assembly assembly, object[] args);

        protected abstract BaseProviderConfig Config { get; set; }

        public T GetProviderCore(object[] args, string groupName = null)
        {
            if (string.IsNullOrWhiteSpace(groupName)) { groupName = Config.DefaultGroupName; }
            T provider = default(T);
            if (_providerDict.ContainsKey(groupName))
            {
                _providerDict.TryGetValue(groupName, out provider);
                if (provider != null) { return provider; }
            }

            if (string.IsNullOrEmpty(Config.AssemblyName))
            {
                throw new ArgumentNullException(nameof(Config.AssemblyName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的程序集名称，请检查配置文件{4}", Config.ModuleName, groupName, nameof(Config.AssemblyName), typeof(T).FullName, Config.ConfigFile));
            }
            if (string.IsNullOrEmpty(Config.ClassFullName))
            {
                throw new ArgumentNullException(nameof(Config.ClassFullName), string.Format("{0}:{1}:{2} 未获取到{3}具体实现的类名，请检查配置文件{4}", Config.ModuleName, groupName, nameof(Config.ClassFullName), typeof(T).FullName, Config.ConfigFile));
            }
            var assembly = Assembly.Load(Config.AssemblyName);
            if (assembly == null) { throw new ArgumentNullException(nameof(assembly), string.Format("未找到{0}程序集", Config.AssemblyName)); }
            provider = CreateInstance(assembly, args);
            if (provider == null) { throw new ArgumentNullException(nameof(provider), string.Format("实例化类型{0}失败", Config.ClassFullName)); }
            _providerDict.TryAdd(groupName, provider);
            return provider;
        }
    }
}
