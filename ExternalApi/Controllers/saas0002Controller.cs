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
    public class saas0002Controller : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="sign"></param>
        /// <param name="busRes"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public object Post(long timestamp, string sign, [FromBody]BusRes busRes)
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
                        switch (busRes.opttype)
                        {
                            case 0://退订                               
                            case 1://暂停
                                Order.OptType = 1;
                                List<Tb_UserScheme> lTb_UserScheme = db.Tb_UserScheme.Where(w => w.ID == busRes.ecordercode).ToList();
                                foreach (Tb_UserScheme tu in lTb_UserScheme)
                                {
                                    tu.OptType = 1;
                                }
                                break;
                            case 2://恢复
                                Order.OptType = 0;
                                List<Tb_UserScheme> lTb_UserScheme2 = db.Tb_UserScheme.Where(w => w.ID == busRes.ecordercode).ToList();
                                foreach (Tb_UserScheme tu in lTb_UserScheme2)
                                {
                                    tu.OptType = 0;
                                }                               
                                break;
                            default:
                                break;
                        }
                        if (db.SaveChanges()!=0)
                        {
                            return ExternalResponse.ExRequest();
                        }
                        else
                        {
                            return ExternalResponse.ErrRequest(500, "服务器错误");
                        }
                    }
                    else
                    {
                        return ExternalResponse.ErrRequest(504, "订单不存在");
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
