using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalApi.Common
{
    public class ExternalBase
    {
    }
    #region 类
    public class BusRes
    {
        public string applyno { get; set; }
        public string ecordercode { get; set; }
        public int opttype { get; set; }
        public int trial { get; set; }
        public long bossorderid { get; set; }
        public long custid { get; set; }
        public string custcode { get; set; }
        public int custtype { get; set; }
        public int registersource { get; set; }
        public string custname { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
        public string useralias { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string productcode { get; set; }
        public long begintime { get; set; }
        public long endtime { get; set; }
        public List<param> productparas { get; set; }
        //public List<param> productparas { get; set; }
        public List<ServiceE> services { get; set; }
    }
    public class ServiceE
    {
        public int opttype { get; set; }
        public string code { get; set; }
        public long begintime { get; set; }
        public long endtime { get; set; }
        public List<param> serviceparas { get; set; }
    }
    public class UserScheme
    {
        public string applyno { get; set; }
        public string ecordercode { get; set; }
        public long custid { get; set; }
        public string productcode { get; set; }
        public long begintime { get; set; }
        public long endtime { get; set; }
        public List<userparm> users { get; set; }
    }
    public class userparm
    {
        public int opttype { get; set; }
        public long userid { get; set; }
        public string username { get; set; }
        public string useralias { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public long begintime { get; set; }
        public long endtime { get; set; }
        public List<param> paras { get; set; }
    }
    public class param
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class ExRequest
    {
        public string errmsg { get; set; }
        public bool success { get; set; }
    }
    public class ErrRequest
    {
        public string msg { get; set; }
        public int code { get; set; }
    }
    #endregion
}