using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Flyer.Extensions
{

    /// <summary>
    /// 序列化工具
    /// </summary>
    public static class SerializerHelper
    {

        #region JSON

        /// <summary>
        /// 序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson<T>(T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            return ToJson<T>(value, dateFormatString: null);
        }
        /// <summary>
        /// 序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="dateFormatString"></param>
        /// <returns></returns>
        public static string ToJson<T>(T value, string dateFormatString)
        {
            return ToJson(value, new JsonSerializerSettings() { DateFormatString = dateFormatString });
        }
        /// <summary>
        /// 序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string ToJson<T>(T value, JsonSerializerSettings setting)
        {
            return JsonConvert.SerializeObject(value, setting);
        }
        /// <summary>
        /// 反序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T FromJson<T>(string value)
        {
            return FromJson<T>(value, null);
        }
        /// <summary>
        /// 反序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static T FromJson<T>(string value, JsonSerializerSettings setting)
        {
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value, setting);
        }

        #endregion

        #region XML

        /// <summary>
        /// 序列化XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToXml<T>(T value)
        {
            return ToXml<T>(value,null);
        }
        /// <summary>
        /// 序列化XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="xmlRootName"></param>
        /// <returns></returns>
        public static string ToXml<T>(T value, string xmlRootName)
        {
            if (value == null) { return string.Empty; }
            XmlSerializer xmlSerializer = string.IsNullOrEmpty(xmlRootName) ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                xmlSerializer.Serialize(sw, value);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
        /// <summary>
        /// 反序列化XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T FromXml<T>(string xml)
        {
            return FromXml<T>(xml,null);
        }
        /// <summary>
        /// 反序列化XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="xmlRootName"></param>
        /// <returns></returns>
        public static T FromXml<T>(string xml, string xmlRootName)
        {
            if (xml == null) { return default(T); }
            XmlSerializer xmlSerializer = string.IsNullOrEmpty(xmlRootName) ? new XmlSerializer(typeof(T)) : new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            using (StreamReader sr = new StreamReader(ms))
            {
                return (T)xmlSerializer.Deserialize(sr);
            }
        }


        #endregion

    }
}
