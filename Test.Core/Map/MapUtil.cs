using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Test.Core.Map
{
    public static class MapExtensions
    {
        public static string Description<T>(this T instance)
        {
            return GetDescription(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetDescription(Type type, object member)
        {
            return GetDescription(type, GetName(type, member));
        }
        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetName(Type type, object member)
        {
            if (type == null)
                return string.Empty;
            if (member == null)
                return string.Empty;
            if (member is string)
                return member.ToString();
            if (type.GetTypeInfo().IsEnum == true)
                return System.Enum.GetName(type, member);
            return string.Empty;
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
            if (member == null)
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute ? attribute.Description : member.Name;
        }
    }

    public class MapUtil : IMapUtil 
    {
        public IDictionary<string, string> ObjectToDictionary(object obj)
        {
            IDictionary<string,string> dict = new Dictionary<string, string>();

            var type = obj.GetType();
            var propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertys)
            {
                var method = property.GetGetMethod();
                if (null != method && method.IsPublic)
                {
                    if (null != property.GetValue(obj))
                    {
                        var description= property.CustomAttributes.Where(x => x.AttributeType.Equals(typeof(DescriptionAttribute))).Select(s => s.ConstructorArguments.FirstOrDefault()).FirstOrDefault();
                        if (null != description.Value) 
                        {
                            dict.Add(description.Value.ToString(), property.GetValue(obj).ToString());
                        }
                        else
                        {
                            dict.Add(property.Name, property.GetValue(obj).ToString());
                        }
                    }
                }
            }
            return dict;
        }

        public IDictionary<string, string> DynamicToDictionary(dynamic obj)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var type = obj.GetType();
            var propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertys)
            {
                var method = property.GetGetMethod();
                if (null != method && method.IsPublic)
                {
                    if (null != property.GetValue(obj))
                    {
                        //var description = property.CustomAttributes.Where(x => x.AttributeType.Equals(typeof(DescriptionAttribute))).Select(s => s.ConstructorArguments.FirstOrDefault()).FirstOrDefault();
                        //if (null != description.Value)
                        //{
                        //    dict.Add(description.Value.ToString(), property.GetValue(obj).ToString());
                        //}
                        //else
                        //{
                            dict.Add(property.Name, property.GetValue(obj).ToString());
                        //}
                    }
                }
            }
            return dict;
        }

        public IDictionary<string, string> EntityToDictionary<T>(T obj) where T : class 
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();

            var type = obj.GetType();
            var propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertys)
            {
                var method = property.GetGetMethod();
                if (null != method && method.IsPublic)
                {
                    if (null != property.GetValue(obj))
                    {
                        var description = property.CustomAttributes.Where(x => x.AttributeType.Equals(typeof(DescriptionAttribute))).Select(s => s.ConstructorArguments.FirstOrDefault()).FirstOrDefault();
                        if (null != description.Value)
                        {
                            dict.Add(description.Value.ToString(), property.GetValue(obj).ToString());
                        }
                        else
                        {
                            dict.Add(property.Name, property.GetValue(obj).ToString());
                        }
                    }
                }
            }
            return dict;
        }

        public List<string> DynamicToStringList(dynamic obj)
        {
            var list = new List<string>();
            var type = obj.GetType();
            var propertys = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in propertys)
            {
                var method = property.GetGetMethod();
                if (null != method && method.IsPublic)
                {
                    if (null != property.GetValue(obj))
                    {
                        var str = property.Name + "=" + property.GetValue(obj).ToString();
                        list.Add(str);
                    }
                }
            }
            return list;
        }

        public List<string> DictionaryToStringList(IDictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                var str = item.Key + "=" + item.Value;
                list.Add(str);
            }
            return list;
        }
    }
}
