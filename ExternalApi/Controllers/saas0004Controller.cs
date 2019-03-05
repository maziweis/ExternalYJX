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
    public class saas0004Controller : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <param name="userRes"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public object Post(long timestamp,string sign, [FromBody]UserScheme userRes)
        {
            if (ExternalResponse.signEncode(timestamp, sign))
            {
                return ExternalResponse.ErrRequest(501, "sign的值错误");
            }
            using (var db = new SunnyTeachEntities())
            {
                try
                {
                    List<long> userids = new List<long>();
                    foreach (userparm up in userRes.users)
                    {
                        userids.Add(up.userid);
                    }
                    List<Tb_UserScheme> lUserScheme = db.Tb_UserScheme.Where(w => userids.Contains(w.UserID)).ToList();
                    foreach (var item in lUserScheme)
                    {
                        item.OptType = 1;
                    }
                    if (db.SaveChanges()!=0)
                    {
                        return ExternalResponse.ExRequest3();
                    }
                    else
                    {
                        return ExternalResponse.ErrRequest(504, "删除用户失败！");
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
