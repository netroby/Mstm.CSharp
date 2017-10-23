﻿using Microsoft.Extensions.Configuration;
using Mstm.Common.Config;
using Mstm.Common.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Mstm.Log.Core
{
    /// <summary>
    /// 日志组件工厂
    /// </summary>
    public class LogFactory : Factory<ILogProvider>
    {
        /// <summary>
        /// 内部数据的并发字典
        /// </summary>
        private static ConcurrentDictionary<string, ILogProvider> _loggerDict = new ConcurrentDictionary<string, ILogProvider>();

        /// <summary>
        /// 私有构造函数，不予许外部直接构造实例
        /// </summary>
        private LogFactory(string groupName)
            : base(groupName)
        {
            Config = LogConfig.New(groupName);
        }

        /// <summary>
        /// 当前日志组件接口实现的配置
        /// </summary>
        protected override BaseProviderConfig Config { get; set; }

        private Type CurrentType { get; set; }

        /// <summary>
        /// 获取日志组件的实例
        /// </summary>
        /// <param name="type">当前日志操作所在的类型</param>
        /// <param name="groupName">组名称</param>
        /// <returns>日志组件ILogProvider实例</returns>
        public static ILogProvider GetLogger(Type type, string groupName = null)
        {
            LogFactory factory = new LogFactory(groupName);
            factory.CurrentType = type ?? typeof(LogFactory);
            var provider = factory.GetProviderCore(new object[] { type });
            return provider;
        }

        /// <summary>
        /// 获取日志组件的实例
        /// </summary>
        /// <typeparam name="T">当前日志操作所在的类型</typeparam>
        /// <param name="groupName">组名称</param>
        /// <returns>日志组件ILogProvider实例</returns>
        public static ILogProvider GetLogger<T>(string groupName = null)
        {
            Type type = typeof(T);
            return GetLogger(type, groupName);
        }

        /// <summary>
        /// 反射创建指定类型的实例
        /// </summary>
        /// <param name="assembly">类型所在的程序集实例</param>
        /// <param name="args">类型实例化构造函数需要的参数</param>
        /// <returns>指定类型的实例</returns>
        protected override ILogProvider CreateInstance(Assembly assembly, object[] args)
        {
            var provider = assembly.CreateInstance(Config.ClassFullName, true, BindingFlags.CreateInstance, null, args, CultureInfo.CurrentCulture, null) as ILogProvider;
            return provider;
        }

        /// <summary>
        /// 获取当前缓存键的值
        /// </summary>
        /// <returns></returns>
        protected override string GetCacheKeyCore()
        {
            string cacheKey = string.Format("{0}:{1}:{2}", Config.ModuleName, Config.GroupName, CurrentType.FullName);
            return cacheKey;
        }
    }
}
