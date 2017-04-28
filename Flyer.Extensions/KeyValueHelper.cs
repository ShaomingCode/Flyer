using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Flyer.Extensions
{
    /// <summary>
    /// 键值工具帮助类
    /// </summary>
    public static class KeyValueHelper
    {
        /// <summary>
        /// 是否是简单类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimple(TypeInfo type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>).GetType())
            {
                return IsSimple(type.GetGenericArguments()[0].GetTypeInfo());
            }
            return type.IsPrimitive || type.IsEnum || type.Equals(typeof(string)) || type.Equals(typeof(decimal));
        }

        /// <summary>
        /// 抓成KV列表集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static IList<KeyValuePair<string, string>> ConvertKvList<T>(this T value, string prefix = "")
        {
            if (value == null)
            {
                return null;
            }
            var result = new List<KeyValuePair<string, string>>();
            if (typeof(IDictionary).IsAssignableFrom(value.GetType()))
            {
                foreach (DictionaryEntry kv in (value as IDictionary))
                {
                    if (IsSimple(kv.Value.GetType().GetTypeInfo()))
                    {
                        result.Add(new KeyValuePair<string, string>((prefix == "" ? "" : prefix + ".") + kv.Key.ToString(), kv.Value.ToString()));
                    }
                    else
                    {
                        var kv1 = ConvertKvList(kv.Value, (prefix == "" ? "" : prefix + ".") + kv.Key.ToString());
                        if (kv1 != null)
                        {
                            result.AddRange(kv1);
                        }
                    }
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(value.GetType()))
            {
                int index = 0;
                foreach (var item in (value as IEnumerable))
                {
                    if (IsSimple(item.GetType().GetTypeInfo()))
                    {
                        result.Add(new KeyValuePair<string, string>(prefix + "[" + index.ToString() + "]", item.ToString()));
                    }
                    else
                    {
                        var kv = ConvertKvList(item, prefix + "[" + index.ToString() + "]");
                        if (kv != null)
                        {
                            result.AddRange(kv);
                        }
                    }
                    index++;
                }
            }
            else
            {
                var propertyList = value.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                foreach (var prop in propertyList)
                {

                    if (IsSimple(prop.PropertyType.GetTypeInfo()))
                    {
                        result.Add(new KeyValuePair<string, string>((prefix == "" ? "" : prefix + ".") + prop.Name, prop.GetValue(value).ToString()));
                    }
                    else
                    {
                        var kv = ConvertKvList(prop.GetValue(value), (prefix == "" ? "" : prefix + ".") + prop.Name);
                        if (kv != null)
                        {
                            result.AddRange(kv);
                        }
                    }
                }
            }
            return result;
        }
       
    }
}
