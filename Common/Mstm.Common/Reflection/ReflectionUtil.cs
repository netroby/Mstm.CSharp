using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mstm.Common.Reflection
{
    public class ReflectionUtil
    {
        #region 成员与句柄之间的转换

        public static RuntimeTypeHandle GetTypeHandle(Object obj)
        {
            return Type.GetTypeHandle(obj);
        }

        public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
        {
            return Type.GetTypeFromHandle(handle);
        }

        public static RuntimeFieldHandle GetFieldHandle(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldHandle;
        }

        public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
        {
            return FieldInfo.GetFieldFromHandle(handle);
        }

        public static RuntimeMethodHandle GetMethodHandle(MethodInfo methodInfo)
        {
            return methodInfo.MethodHandle;
        }

        public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
        {
            return MethodInfo.GetMethodFromHandle(handle);
        }

        #endregion

        #region FieldInfo

        public static FieldInfo[] GetFields(Type type)
        {
            return type.GetFields();
        }

        public static FieldInfo[] GetFields(Type type, BindingFlags bindingAttr)
        {
            return type.GetFields(bindingAttr);
        }

        public static FieldInfo[] GetFields(Object obj)
        {
            Type type = obj.GetType();
            return GetFields(type);
        }

        public static FieldInfo[] GetFields(Object obj, BindingFlags bindingAttr)
        {
            Type type = obj.GetType();
            return GetFields(type, bindingAttr);
        }
        #endregion

        #region MethodInfo

        public static MethodInfo[] GetMethods(Type type)
        {
            return type.GetMethods();
        }

        public static MethodInfo[] GetMethods(Type type, BindingFlags bindingAttr)
        {
            return type.GetMethods(bindingAttr);
        }

        public static MethodInfo[] GetMethods(Object obj)
        {
            Type type = obj.GetType();
            return GetMethods(type);
        }

        public static MethodInfo[] GetMethods(Object obj, BindingFlags bindingAttr)
        {
            Type type = obj.GetType();
            return GetMethods(type, bindingAttr);
        }

        #endregion

        #region PropertyInfo

        public static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties();
        }

        public static PropertyInfo[] GetProperties(Type type, BindingFlags bindingAttr)
        {
            return type.GetProperties(bindingAttr);
        }

        public static PropertyInfo[] GetProperties(Object obj)
        {
            Type type = obj.GetType();
            return GetProperties(type);
        }

        public static PropertyInfo[] GetProperties(Object obj, BindingFlags bindingAttr)
        {
            Type type = obj.GetType();
            return GetProperties(type, bindingAttr);
        }

        #endregion

        #region ConstructorInfo

        public static ConstructorInfo[] GetConstructors(Type type)
        {
            return type.GetConstructors();
        }

        public static ConstructorInfo[] GetConstructors(Type type, BindingFlags bindingAttr)
        {
            return type.GetConstructors(bindingAttr);
        }

        public static ConstructorInfo[] GetConstructors(Object obj)
        {
            Type type = obj.GetType();
            return GetConstructors(type);
        }

        public static ConstructorInfo[] GetConstructors(Object obj, BindingFlags bindingAttr)
        {
            Type type = obj.GetType();
            return GetConstructors(type, bindingAttr);
        }

        #endregion

        #region CustomAttributes

        public static List<Attribute> GetCustomAttributes(Type type)
        {
            return type.GetCustomAttributes().ToList();
        }

        public static List<Attribute> GetCustomAttributes(Object obj)
        {
            Type type = obj.GetType();
            return GetCustomAttributes(type);
        }

        #endregion

        #region Interfaces

        public static Type[] GetInterfaces(Type type)
        {
            return type.GetInterfaces();
        }

        public static Type[] GetInterfaces(Object obj)
        {
            Type type = obj.GetType();
            return GetInterfaces(type);
        }

        #endregion
    }
}
