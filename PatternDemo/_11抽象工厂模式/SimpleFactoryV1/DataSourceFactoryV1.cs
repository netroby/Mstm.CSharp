using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _11抽象工厂模式.SimpleFactoryV1
{
    /// <summary>
    /// 利用简单工厂和反射+配置文件的方式动态创建所需的操作实例
    /// </summary>
    public class DataSourceFactoryV1
    {
        private static readonly string userAssemblyName = ConfigurationManager.AppSettings["UserAssemblyName"];
        private static readonly string userClassFullName = ConfigurationManager.AppSettings["UserClassFullName"];
        private static readonly string productAssemblyName = ConfigurationManager.AppSettings["ProductAssemblyName"];
        private static readonly string productClassFullName = ConfigurationManager.AppSettings["ProductClassFullName"];
        public static IUser CreateUserInstance()
        {
            IUser userInsrance = CreateInstance<IUser>(userAssemblyName, userClassFullName);
            return userInsrance;
        }

        public static IProduct CreateProductInstance()
        {
            IProduct productInstance = CreateInstance<IProduct>(productAssemblyName, productClassFullName);
            return productInstance;
        }


        private static T CreateInstance<T>(string assemblyName, string classFullName)
            where T : class
        {
            try
            {
                T result = Assembly.Load(assemblyName).CreateInstance(classFullName) as T;
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
