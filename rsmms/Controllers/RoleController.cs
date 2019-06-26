using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class RoleController : Controller
    {
        

        public ActionResult RoleList()
        {
            RoleService roleService = new RoleService();
            List<Role> list = roleService.SelectRoles();
            //使用ViewData来传递list对象
            ViewBag.Data = list;
            return View(ViewBag);
        }
    }
}