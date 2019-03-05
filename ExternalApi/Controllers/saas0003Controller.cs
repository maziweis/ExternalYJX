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
    /// 
    /// </summary>
    public class saas0003Controller : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <param name="userRes"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public object Post(long timestamp, string sign, [FromBody]UserScheme userRes)
        {
            if (ExternalResponse.signEncode(timestamp, sign))
            {
                return ExternalResponse.ErrRequest(501, "sign的值错误");
            }            
            using (var db = new SunnyTeachEntities())
            {
                Tb_Order tb_Order = db.Tb_Order.First(f => f.ID == userRes.ecordercode);
                if (tb_Order == null)
                {
                    return ExternalResponse.ErrRequest(504, "未查询到相应订单!订单号:" + userRes.ecordercode);
                }
                try
                {
                    Tb_UserScheme tb_UserScheme;
                    foreach (userparm up in userRes.users)
                    {
                        tb_UserScheme = db.Tb_UserScheme.Find(up.userid);
                        if (tb_UserScheme != null)
                        {
                            tb_UserScheme.OptType = up.opttype;
                        }
                        else
                        {                            
                            tb_UserScheme = new Tb_UserScheme();
                            tb_UserScheme.UserID = up.userid;
                            tb_UserScheme.ID = userRes.ecordercode;
                            tb_UserScheme.UserName = up.username;
                            tb_UserScheme.Useralias = up.useralias;
                            tb_UserScheme.Mobile = up.mobile;
                            tb_UserScheme.Email = up.email;
                            tb_UserScheme.BeginTime = up.begintime;
                            tb_UserScheme.EndTime = up.endtime;
                            tb_UserScheme.ApplyNo = userRes.ecordercode;
                            tb_UserScheme.EcorderCode = userRes.applyno;
                            tb_UserScheme.CustID = userRes.custid;
                            tb_UserScheme.ProductCode = userRes.productcode;
                            tb_UserScheme.OptType = up.opttype;
                            tb_UserScheme.IsDelete = 0;
                            tb_UserScheme.Paras = JsonHelper.JsonSerializer(up.paras);
                            tb_UserScheme.UserType = 0;// Convert.ToInt32(up.paras[0].value);
                            db.Tb_UserScheme.Add(tb_UserScheme);
                        }
                    }
                    if (db.SaveChanges() == 0)
                    {
                        return ExternalResponse.ErrRequest(504, "添加用户或更新失败!");
                    }
                    else
                        return ExternalResponse.ExRequest3();
                }
                catch (Exception e)
                {
                    return ExternalResponse.ErrRequest(500, "服务器错误:" + e.Message);
                }
            }
        }
    }
}
