using System;
using System.Collections.Generic;

namespace DataDynamics.Common.Utils;

public class DictionaryUtils
{
    public static Dictionary<string, string> Create(string key, string value)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add(key, value);
        return dic;
    }

    public static Dictionary<string, string> Create(string key1, string value1, string key2, string value2)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add(key1, value1);
        dic.Add(key2, value2);
        return dic;
    }

    public static Dictionary<string, string> Create(string key1, string value1, string key2, string value2, string key3,
        string value3)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add(key1, value1);
        dic.Add(key2, value2);
        dic.Add(key3, value3);
        return dic;
    }
}