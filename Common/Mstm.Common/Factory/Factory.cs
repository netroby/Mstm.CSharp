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
    /// <summary>
    /// 工厂类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Factory<T>
    {
        /// <summary>
        /// 内部并发字典，记录内部数据
        /// </summary>
        private static ConcurrentDictionary<string, T> _providerDict = new ConcurrentDictionary<string, T>();

        /// <summary>
        /// 反射创建指定类型的实例
        /// </summary>
        /// <param name="assembly">类型所在的程序集实例</param>
        /// <param name="args">类型实例化构造函数需要的参数</param>
        /// <returns>指定类型的实例</returns>
        protected abstract T CreateInstance(Assembly assembly, object[] args);

        /// <summary>
        /// 动态加载程序集相关的配置信息
        /// </summary>
        protected abstract BaseProviderConfig Config { get; set; }

        /// <summary>
        /// 获取指定类型的具体实现类的实例
        /// </summary>
        /// <param name="args">实例化需要的构造函数参数</param>
        /// <param name="groupName">组名称</param>
        /// <returns></returns>
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
