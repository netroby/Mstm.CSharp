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
    public class JsonFactory
    {
        private static Assembly _logAssembly;
        private static readonly string _assemblyName;
        private static readonly string _classFullName;
        private static ISerializeProvider _provider;

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

        public static ISerializeProvider GetProvider()
        {
            if (_provider != null) { return _provider; }
            if (_logAssembly == null)
            {
                _logAssembly = Assembly.Load(_assemblyName);
            }
            if (_logAssembly == null) { throw new ArgumentNullException(string.Format("未找到{0}程序集"), _assemblyName); }
            _provider = _logAssembly.CreateInstance(_classFullName, true, BindingFlags.CreateInstance, null, null, CultureInfo.CurrentCulture, null) as ISerializeProvider;
            if (_provider == null) { throw new ArgumentNullException(string.Format("实例化类型{0}失败"), _classFullName); }
            return _provider;
        }
    }
}
