using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!this.checkLogin())// 判断是否登录
            {
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Login", Action = "ToLogin" });
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 判断是否登录
        /// </summary>
        protected bool checkLogin()
        {
            if (this.Session["myEmp"] == null)
            {
                return false;
            }
            return true;
        }
    }
}