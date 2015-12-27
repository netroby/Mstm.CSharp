using _11抽象工厂模式.Classics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _11抽象工厂模式.SimpleFactoryV2
{

    /// <summary>
    /// 利用简单工厂封装经典的抽象工厂
    /// 并利用反射+配置文件的方式动态获取所需的工厂
    /// </summary>
    public class DataSourceFactoryV2
    {
        private static readonly string factoryAssemblyName = ConfigurationManager.AppSettings["DataSourceFactoryAssemblyName"];
        private static readonly string factoryClassFullName = ConfigurationManager.AppSettings["DataSourceFactoryClassFullName"];

        public static IUser CreateUserInstance()
        {
            IUser userInsrance = GetFactory().CreateUserInstance();
            return userInsrance;
        }

        public static IProduct CreateProductInstance()
        {
            IProduct productInstance = GetFactory().CreateProductInstance();
            return productInstance;
        }

        private static IFactory GetFactory()
        {
            IFactory factory = CreateInstance<IFactory>(factoryAssemblyName, factoryClassFullName);
            return factory;
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
