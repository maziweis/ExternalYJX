﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace ExternalApi
{
    public class JsonHelper
    {
        /// <summary>
        /// list 集合转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        public static List<T> JosnDeserializer<T>(string json) where T : new()
        {
            string _JsonText = json;

            List<T> _Test = new List<T>();

            DataContractJsonSerializer _Json = new DataContractJsonSerializer(_Test.GetType());
            byte[] _Using = System.Text.Encoding.UTF8.GetBytes(_JsonText);
            System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
            _MemoryStream.Position = 0;

            return _Test = (List<T>)_Json.ReadObject(_MemoryStream);
        }
        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// 对象JSON序列化接口
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        public static string EncodeJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            return serializer.Serialize(obj);
        }
        /// <summary>
        /// 对象反序列化接口
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">序列化字符串</param>
        /// <returns></returns>
        public static T DecodeJson<T>(string json) where T : new()
        {
            T obj;
            if (!String.IsNullOrEmpty(json))
            {

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = int.MaxValue;
                obj = (T)serializer.Deserialize(json, typeof(T));
            }
            else
            {
                obj = default(T);
            }
            return obj;
        }
        /// <summary>
        /// 对象JSON序列化接口
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        public static string DeepEncodeJson(object obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());

            string szJson = "";

            //序列化

            using (MemoryStream stream = new MemoryStream())
            {

                json.WriteObject(stream, obj);

                szJson = Encoding.UTF8.GetString(stream.ToArray());

            }
            return szJson;
        }

        /// <summary>
        /// 对象反序列化接口
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">序列化字符串</param>
        /// <returns></returns>
        public static T DeepDecodeJson<T>(string json) where T : new()
        {
            T obj = default(T);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                obj = (T)serializer.ReadObject(ms);

            }
            return obj;
        }
    }
}