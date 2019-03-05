using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ExternalApi
{
    /// <summary>
    /// 
    /// </summary>
    public class ExternalResponse
    {
        /// <summary>
        /// 正常回复
        /// </summary>
        public static ExRequest ExRequest()
        {
            ExRequest exRequest = new ExRequest();
            exRequest.result = true;
            exRequest.errmsg = "";
            return exRequest;
        }

        /// <summary>
        /// 正常回复
        /// </summary>
        public static ExRequest3 ExRequest3()
        {
            ExRequest3 exRequest = new ExRequest3();
            exRequest.success = true;
            exRequest.errmsg = "";
            return exRequest;
        }

        /// <summary>
        /// 错误回复
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public static ErrRequest ErrRequest(int code, string msg)
        {
            ErrRequest exRequest = new ErrRequest();
            exRequest.code = code;
            exRequest.msg = msg;
            return exRequest;
        }
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }

        /// <summary>
        /// 加密用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="codeLength">加密位数</param>
        /// <returns>加密密码</returns>
        public static string md5(string password, int codeLength)
        {
            if (!string.IsNullOrEmpty(password))
            {
                // 16位MD5加密（取32位加密的9~25字符）  
                if (codeLength == 16)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower().Substring(8, 16);
                }

                // 32位加密
                if (codeLength == 32)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();
                }
            }
            return string.Empty;
        }
        //static string client_id = "1029";
        static string client_id = System.Configuration.ConfigurationManager.AppSettings["client_id"];
        //static string client_key = "8cc35dc9-a320-4cab-b0b2-1edec49c295a";
        static string client_key = System.Configuration.ConfigurationManager.AppSettings["client_key"];
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool signEncode(long timestamp, string sign)
        {

            string md5 = "client_id="+ client_id + "client_key="+ client_key + "timestamp=" + timestamp;
            string md5sign = MD5Encrypt32(md5);
            if (sign != md5sign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool result { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExRequest3
    {
        /// <summary>
        /// 
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ErrRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }
}