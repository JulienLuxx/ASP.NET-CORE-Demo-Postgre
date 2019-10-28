using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Core.Map
{
    public interface IMapUtil
    {
        IDictionary<string, string> ObjectToDictionary(object obj);

        IDictionary<string, string> DynamicToDictionary(dynamic obj);

        IDictionary<string, string> EntityToDictionary<T>(T obj) where T : class;

        List<string> DynamicToStringList(dynamic obj);

        List<string> DictionaryToStringList(IDictionary<string, string> dict);
    }
}
