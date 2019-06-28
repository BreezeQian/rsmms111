using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class LoginController : Controller
    {
       
        public ActionResult ToLogin()
        {

            return PartialView();
        }


        public ActionResult Login(String eid, String password)
        {
            EmpService empService = new EmpService();
            Emp emp = empService.SelectEmpByEidAndPassword(eid, password);
            String msg = "";
            if (emp.Eid != null && !emp.Eid.Equals(""))
            {
                msg = "success";
                Session["myEmp"] = emp;
                Session["ename"] = emp.Ename;
                Session["eid"] = emp.Eid;
            }
            else
            {
                msg = "账号或密码不正确";
            }
            return Json(msg);
        }
    }
}