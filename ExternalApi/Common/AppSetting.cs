using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExternalApi.Common
{
    public class AppSetting
    {
        private static string _connectionString = string.Empty;
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KingsunConnectionStr"].ConnectionString;
                }
                return _connectionString;
            }
        }

        public static string GetValue(string key)
        {
            string result = "";
            if (string.IsNullOrEmpty(key))
            {
                return result;
            }
            result = ConfigurationManager.AppSettings[key];
            return result;
        }
    }
}