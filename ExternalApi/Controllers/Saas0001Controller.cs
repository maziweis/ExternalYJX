using ExternalApi.Common;
using FzDataBase.SunnyTeach;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExternalApi.Controllers
{
    /// <summary>
    /// Saas0001
    /// </summary>
    public class saas0001Controller : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <param name="busRes"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public object Post([FromBody]BusRes busRes,long timestamp, string sign)
        {
            if (ExternalResponse.signEncode(timestamp, sign))
            {
                return ExternalResponse.ErrRequest(501, "sign的值错误");
            }
            using (var db = new SunnyTeachEntities())
            {
                try
                {
                    Tb_Order Order = db.Tb_Order.Find(busRes.applyno);
                    if (Order != null)
                    {
                        return ExternalResponse.ErrRequest(504, "订单已存在");
                    }
                    else
                    {
                        Tb_Order tb_Order = new Tb_Order();
                        tb_Order.ID = busRes.ecordercode;
                        tb_Order.SchoolName = "移动云";//待定
                        tb_Order.SchoollID = 0;//待定
                        tb_Order.AdminAccount = busRes.username;
                        tb_Order.Phone = busRes.mobile;
                        tb_Order.SchemeID = 0;//待定 标准版 升级版
                        tb_Order.SchemeName = "标准版";//待定 标准版 升级版
                        tb_Order.OrderMon = Convert.ToInt32(busRes.services[0].serviceparas[0].value);//待定 月数
                        tb_Order.SchemeNum = Convert.ToInt32(busRes.services[0].serviceparas[1].value);//待定 人数
                        tb_Order.AccessNum = 0;//待定 已授权人数
                        tb_Order.SchemeMoney = 0;//待定 实收金额
                        tb_Order.OptType = busRes.services[0].opttype;
                        tb_Order.SchemeStateNm = "已付款";//dd
                        tb_Order.SchemeTypeID = 0;//dd
                        tb_Order.SchemeTypeName = "新订单";//dd
                        tb_Order.SchemeNo = busRes.applyno;
                        tb_Order.SchemeDate = DateTime.Now;
                        tb_Order.Ecordercode = busRes.applyno;
                        tb_Order.Trial = busRes.trial;
                        tb_Order.BossOrderID = busRes.bossorderid.ToString();
                        tb_Order.CustID = busRes.custid;
                        tb_Order.CustCode = busRes.custcode;
                        tb_Order.CustType = busRes.custtype;
                        tb_Order.RegisterSource = busRes.registersource;
                        tb_Order.Email = busRes.email;
                        tb_Order.ProductCode = busRes.services[0].code;
                        tb_Order.BeginTime = busRes.services[0].begintime;
                        tb_Order.EndTime = busRes.services[0].endtime;
                        tb_Order.CustName = busRes.custname;
                        tb_Order.UserID = busRes.userid;
                        tb_Order.UserName = busRes.username;
                        tb_Order.UserLias = busRes.useralias;
                        tb_Order.Produectparas = JsonHelper.JsonSerializer(busRes.services);
                        db.Tb_Order.Add(tb_Order);
                        long scUserid = Convert.ToInt64(busRes.userid);                
                        Tb_UserScheme tb_UserScheme = db.Tb_UserScheme.Find(scUserid);
                        if (tb_UserScheme == null)
                        {
                            tb_UserScheme = new Tb_UserScheme();
                            tb_UserScheme.UserID =Convert.ToInt64(busRes.userid);
                            tb_UserScheme.UserName = busRes.username;
                            tb_UserScheme.EcorderCode = busRes.applyno;
                            tb_UserScheme.Useralias = busRes.useralias;
                            tb_UserScheme.UserType = 1;
                            tb_UserScheme.OptType = 1;
                            tb_UserScheme.Mobile = busRes.mobile;
                            tb_UserScheme.ProductCode = busRes.services[0].code;
                            tb_UserScheme.ID = busRes.ecordercode;
                            tb_UserScheme.ApplyNo = busRes.ecordercode;
                            tb_UserScheme.CustID = busRes.custid;
                            tb_UserScheme.Email = busRes.email;
                            tb_UserScheme.BeginTime = busRes.services[0].begintime;
                            tb_UserScheme.EndTime = busRes.services[0].endtime;
                            tb_UserScheme.IsDelete = 0;
                            db.Tb_UserScheme.Add(tb_UserScheme);
                        }
                        db.SaveChanges();
                        return ExternalResponse.ExRequest();
                    }
                }
                catch (Exception e)
                {
                    return ExternalResponse.ErrRequest(500, "服务器错误:" + e.Message);
                }
            }
        }
    }
}
