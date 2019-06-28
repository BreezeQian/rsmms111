using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class DepController : BaseController
    {


        public ActionResult DepList()
        {
            return View();
        }
        /**
         * 编辑部门（数据回显）
         */
        public ActionResult DepEdit(String did)
        {
            DepService depService = new DepService();
            Dep dep = depService.GetDepByDid(did);
            ViewBag.Dep = dep;
            return PartialView(ViewBag);
        }
        /**
         *  修改部门执行代码
         */
        public ActionResult doDepUpdate(String did, String dname, String ddesc)
        {
            String msg = "";
            Dep dep = new Dep();
            dep.Ddesc = ddesc;
            dep.Dname = dname;
            
            if (did != null && !did.Equals(""))
            {
                dep.Did = int.Parse(did);
            }
            else
            {
                dep.Did = null;
            }
            DepService depService = new DepService();
            msg = depService.UpdateDep(dep);
            return Json(msg);
        }


        /**
         * 新增部门页面
         */
        public ActionResult DepAdd()
        {
            return PartialView();
        }
        /**
         *  新增部门执行代码
         */
        public ActionResult doDepAdd(String dname, String ddesc)
        {
            String msg = "";
            Dep dep = new Dep();
            dep.Dname = dname;
            dep.Ddesc = ddesc;
            DepService depService = new DepService();
            msg = depService.AddDep(dep);
            return Json(msg);
        }

        /**
         *  删除部门执行代码
         */
        public ActionResult doDepDelete(String did)
        {
            String msg = "";
            Dep dep = new Dep();
            dep.Did = int.Parse(did);
            DepService depService = new DepService();
            msg = depService.Deletedep(dep);
            return Json(msg);
        }
        /**
 * 页面数据加载
 */
        public ActionResult getDepList(String dname)
        {
            Dep depQuery = new Dep();
            depQuery.Dname = dname;
            DepService DepService = new DepService();
            List<Dep> list = DepService.SelectDeps(depQuery);
            return Json(list);
        }
    }
}