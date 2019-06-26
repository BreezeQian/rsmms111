using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class EmpController : Controller
    {
        
        /**
         * 页面跳转
         */ 
        public ActionResult EmpList()
        {  
            return View();
        }

        /**
         * 编辑员工（数据回显）
         */ 
        public ActionResult EmpEdit(String eid)
        {
            EmpService empService = new EmpService();
            Emp emp = empService.GetEmpByEid(eid);
            ViewBag.Emp = emp;
            return PartialView(ViewBag);
        }
        /**
         * 新增员工页面
         */
        public ActionResult EmpAdd()
        {
            return PartialView();
        }
        /**
         *  新增员工执行代码
         */ 
        public ActionResult doEmpAdd(String eid, String password, String ename, String age, String did, String rid)
        {
            String msg = "";
            Emp emp = new Emp();
            emp.Eid = eid;
            emp.Password = password;
            emp.Ename = ename;
            if(age != null && !age.Equals(""))
            {
                emp.Age = int.Parse(age);
            }
            else
            {
                emp.Age = 0;
            }
            if(did != null && !did.Equals(""))
            {
                emp.Did = int.Parse(did);
            }
            else
            {
                emp.Did = null;
            }
            EmpService empService = new EmpService();
            msg = empService.AddEmp(emp, rid);
            return Json(msg);
        }
        /**
         * 页面数据加载
         */
        public ActionResult getEmpList(String eid, String ename, String rid, String did)
        {
            Emp empQuery = new Emp();
            empQuery.Eid = eid;
            empQuery.Ename = ename;
            if (rid != null && !rid.Equals(""))
            {
                empQuery.Rid = int.Parse(rid);
            }
            if (did != null && !did.Equals(""))
            {
                empQuery.Did = int.Parse(did);
            }
            EmpService empService = new EmpService();
            List<Emp> list = empService.SelectEmps(empQuery);
            return Json(list);
        }

       
        /**
         *  获得所有角色（角色下拉框）
         */
        public JsonResult getRoles()
        {
            RoleService roleService = new RoleService();
            List<Role> roleList = roleService.SelectRoles();
            return Json(roleList);
        }

        /**
        *  获得所有部门（角色下拉框）
        */
        public JsonResult getDeps()
        {
            DepService depService = new DepService();
            List<Dep> depList = depService.SelectDeps();
            return Json(depList);
        }

    }
}