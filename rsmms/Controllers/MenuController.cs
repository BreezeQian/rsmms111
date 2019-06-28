using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class MenuController : BaseController
    {
        

        public ActionResult MenuList()
        {
            MenuService menuService = new MenuService();
            List<Menu> list = menuService.SelectMenus();
            //使用ViewData来传递list对象
            ViewBag.Data = list;
            return View(ViewBag);
        }
    }
}