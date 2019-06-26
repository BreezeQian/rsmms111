using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class DepController : Controller
    {
        

        public ActionResult DepList()
        {
            DepService depService = new DepService();
            List<Dep> list = depService.SelectDeps();
            //使用ViewData来传递list对象
            ViewBag.Data = list;
            return View(ViewBag);
        }
    }
}