using Eddo.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Eddo.Reflection
{
    public static  class ReflectionHelper
    {
        /// <summary>
        /// 判断原数据是否为引用数据数据类型
        /// </summary>
        /// <param name="givenType">原数据类型</param>
        /// <param name="genericType">引用数据数据类型</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }
        /// <summary>
        /// 获取类型描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return GetDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(Common.GetType<T>(), memberName);
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription(Type type, string memberName)
        {
            if (type == null)
                return string.Empty;
            if (string.IsNullOrWhiteSpace(memberName))
                return string.Empty;
            return GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="member">成员</param>
        public static string GetDescription(MemberInfo member)
        {
            DescriptionAttribute attribute = new DescriptionAttribute();
            if (member == null)
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute? attribute.Description: member.Name;
        }

        /// <summary>
        /// 获取显示名称，使用DisplayNameAttribute设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return GetDisplayName(Common.GetType<T>());
        }

        /// <summary>
        /// 获取显示名称，使用DisplayAttribute或DisplayNameAttribute设置显示名称
        /// </summary>
        public static string GetDisplayName(MemberInfo member)
        {
            DisplayNameAttribute displayNameAttribute=new DisplayNameAttribute();
            DisplayAttribute displayAttribute = new DisplayAttribute() ;
            if (member == null)
                return string.Empty;
            if (member.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute)
                return displayAttribute.Name;
            if (member.GetCustomAttribute<DisplayNameAttribute>() is DisplayNameAttribute)
                return displayNameAttribute.DisplayName;
            return string.Empty;
        }

        /// <summary>
        /// 获取显示名称或描述,使用DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayNameOrDescription<T>()
        {
            return GetDisplayNameOrDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取属性显示名称或描述,使用DisplayAttribute或DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        public static string GetDisplayNameOrDescription(MemberInfo member)
        {
            var result = GetDisplayName(member);
            return string.IsNullOrWhiteSpace(result) ? GetDescription(member) : result;
        }

        /// <summary>
        /// 获取实现了接口的所有具体类型
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="assembly">在该程序集中查找</param>
        public static List<TInterface> GetTypesByInterface<TInterface>(Assembly assembly)
        {
            var typeInterface = typeof(TInterface);
            return assembly.GetTypes()
                .Where(t => typeInterface.GetTypeInfo().IsAssignableFrom(t) && t != typeInterface && t.GetTypeInfo().IsAbstract == false)
                .Select(t => CreateInstance<TInterface>(t)).ToList();
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return ConvertHelper.To<T>(Activator.CreateInstance(type, parameters));
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return true;
            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
                return true;
            if (property.PropertyType == typeof(DateTime?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int32" || member.ToString() == "System.Int16" || member.ToString() == "System.Int64";
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
                return true;
            if (property.PropertyType == typeof(int?))
                return true;
            if (property.PropertyType == typeof(short))
                return true;
            if (property.PropertyType == typeof(short?))
                return true;
            if (property.PropertyType == typeof(long))
                return true;
            if (property.PropertyType == typeof(long?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
                return false;
            if (IsInt(member))
                return true;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Double" || member.ToString() == "System.Decimal" || member.ToString() == "System.Single";
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
                return true;
            if (property.PropertyType == typeof(double?))
                return true;
            if (property.PropertyType == typeof(decimal))
                return true;
            if (property.PropertyType == typeof(decimal?))
                return true;
            if (property.PropertyType == typeof(float))
                return true;
            if (property.PropertyType == typeof(float?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(Type type)
        {
            if (!type.IsGenericType)
                return false;
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IEnumerable<>)
                   || typeDefinition == typeof(IReadOnlyCollection<>)
                   || typeDefinition == typeof(IReadOnlyList<>)
                   || typeDefinition == typeof(ICollection<>)
                   || typeDefinition == typeof(IList<>)
                   || typeDefinition == typeof(List<>);
        }
        /// <summary>
        /// Gets a list of attributes defined for a class member and it's declaring type including inherited attributes.
        /// </summary>
        /// <param name="inherit">Inherit attribute from base classes</param>
        /// <param name="memberInfo">MemberInfo</param>
        public static List<object> GetAttributesOfMemberAndDeclaringType(MemberInfo memberInfo, bool inherit = true)
        {
            var attributeList = new List<object>();

            attributeList.AddRange(memberInfo.GetCustomAttributes(inherit));

            //Add attributes on the class
            if (memberInfo.DeclaringType != null)
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(inherit));
            }

            return attributeList;
        }

        /// <summary>
        /// Gets a list of attributes defined for a class member and it's declaring type including inherited attributes.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="inherit">Inherit attribute from base classes</param>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            //Add attributes on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            //Add attributes on the class
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.GetTypeInfo().IsDefined(typeof(TAttribute), inherit))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>());
            }

            return attributeList;
        }

        /// <summary>
        /// Tries to gets an of attribute defined for a class member and it's declaring type including inherited attributes.
        /// Returns default value if it's not declared at all.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">Default value (null as default)</param>
        /// <param name="inherit">Inherit attribute from base classes</param>
        public static TAttribute GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : Attribute
        {
            //Get attribute on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            //Get attribute from class
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.GetTypeInfo().IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        /// <summary>
        /// Tries to gets an of attribute defined for a class member and it's declaring type including inherited attributes.
        /// Returns default value if it's not declared at all.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">Default value (null as default)</param>
        /// <param name="inherit">Inherit attribute from base classes</param>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default(TAttribute), bool inherit = true)
            where TAttribute : Attribute
        {
            //Get attribute on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets value of a property by it's full path from given object
        /// </summary>
        /// <param name="obj">Object to get value from</param>
        /// <param name="objectType">Type of given object</param>
        /// <param name="propertyPath">Full path of property</param>
        /// <returns></returns>
        internal static object GetValueByPath(object obj, Type objectType, string propertyPath)
        {
            var value = obj;
            var currentType = objectType;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith(objectPath))
            {
                absolutePropertyPath = absolutePropertyPath.Replace(objectPath + ".", "");
            }

            foreach (var propertyName in absolutePropertyPath.Split('.'))
            {
                var property = currentType.GetProperty(propertyName);
                value = property.GetValue(value, null);
                currentType = property.PropertyType;
            }

            return value;
        }

        /// <summary>
        /// Sets value of a property by it's full path on given object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objectType"></param>
        /// <param name="propertyPath"></param>
        /// <param name="value"></param>
        internal static void SetValueByPath(object obj, Type objectType, string propertyPath, object value)
        {
            var currentType = objectType;
            PropertyInfo property;
            var objectPath = currentType.FullName;
            var absolutePropertyPath = propertyPath;
            if (absolutePropertyPath.StartsWith(objectPath))
            {
                absolutePropertyPath = absolutePropertyPath.Replace(objectPath + ".", "");
            }

            var properties = absolutePropertyPath.Split('.');

            if (properties.Length == 1)
            {
                property = objectType.GetProperty(properties.First());
                property.SetValue(obj, value);
                return;
            }

            for (int i = 0; i < properties.Length - 1; i++)
            {
                property = currentType.GetProperty(properties[i]);
                obj = property.GetValue(obj, null);
                currentType = property.PropertyType;
            }

            property = currentType.GetProperty(properties.Last());
            property.SetValue(obj, value);
        }
    }
}
