﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Test.Core.Map
{
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
                        dict.Add(property.Name, property.GetValue(obj).ToString());
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
                        dict.Add(property.Name, property.GetValue(obj).ToString());
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
    }
}
