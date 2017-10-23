using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Json.Core
{
    /// <summary>
    /// Json序列化操作组件工厂
    /// </summary>
    public class JsonFactory
    {
        private static Assembly _assembly;
        private static readonly string _assemblyName;
        private static readonly string _classFullName;
        private static ISerializeProvider _provider;

        /// <summary>
        /// 静态构造函数
        /// 加载配置文件
        /// </summary>
        static JsonFactory()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            _assemblyName = config["JsonProvider:AssemblyName"];
            _classFullName = config["JsonProvider:ClassFullName"];
            if (string.IsNullOrEmpty(_assemblyName))
            {
                throw new ArgumentNullException("JsonProvider:AssemblyName", "未获取到ISerializeProvider具体实现的程序集名称，请检查配置文件appsettings.json");
            }
            if (string.IsNullOrEmpty(_classFullName))
            {
                throw new ArgumentNullException("JsonProvider:ClassFullName", "未获取到ISerializeProvider具体实现的类名，请检查配置文件appsettings.json");
            }
        }

        /// <summary>
        /// 获取Json序列化组件实例
        /// </summary>
        /// <returns></returns>
        public static ISerializeProvider GetProvider()
        {
            if (_provider != null) { return _provider; }
            if (_assembly == null)
            {
                _assembly = Assembly.Load(_assemblyName);
            }
            if (_assembly == null) { throw new ArgumentNullException(string.Format("未找到{0}程序集"), _assemblyName); }
            _provider = _assembly.CreateInstance(_classFullName, true, BindingFlags.CreateInstance, null, null, CultureInfo.CurrentCulture, null) as ISerializeProvider;
            if (_provider == null) { throw new ArgumentNullException(string.Format("实例化类型{0}失败"), _classFullName); }
            return _provider;
        }
    }
}
