using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XNManage.Common
{
    /// <summary>
    /// 登陆验证
    /// </summary>
    public class LoginValidAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CheckValid(filterContext.HttpContext) == -1)
            {
                filterContext.Result = new RedirectResult("~/User/Logon");
            }
        }

        private int CheckValid(HttpContextBase context)
        {
            int _userId;
            if (context.Session["uid"] != null)
            {
                _userId = Convert.ToInt16(context.Session["uid"].ToString());
                return _userId;
            }
            var cook = context.Request.Cookies["xnuid"];
            if (cook != null)
            {
                var uid = Convert.ToInt16(cook.Value);
                context.Session["uid"] = uid;
                context.Session.Timeout = 600;

                _userId = uid;
                return uid;
            }
            return -1;
        }
    }
}