using rsmms.Models;
using rsmms.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rsmms.Controllers
{
    public class RoleController : BaseController
    {
        
        /**
         * 页面跳转
         */ 
        public ActionResult RoleList()
        {
            return View();
        }

        /**
         * 页面加载数据
         */ 
        public ActionResult getRoleList(String rname)
        {
            Role roleQuery = new Role();
            roleQuery.Rname = rname;
            RoleService roleService = new RoleService();
            List<Role> list = roleService.SelectRoles(roleQuery);
            return Json(list);
        }

        /**
        * 新增角色页面
        */
        public ActionResult RoleAdd()
        {
            return PartialView();
        }

        /**
         *  新增角色执行代码
         */
        public ActionResult doRoleAdd(String rname, String rdesc)
        {
            String msg = "";
            Role role = new Role();
            role.Rname = rname;
            role.Rdesc = rdesc;
            RoleService roleService = new RoleService();
            msg = roleService.AddRole(role);
            return Json(msg);
        }

        /**
         * 编辑角色（数据回显）
         */
        public ActionResult RoleEdit(String rid)
        {
            RoleService roleService = new RoleService();
            Role role = roleService.GetRoleByRid(rid);
            ViewBag.Role = role;
            return PartialView(ViewBag);
        }

        /**
         *  修改角色执行代码
         */
        public ActionResult doRoleUpdate(String rid, String rname, String rdesc)
        {
            String msg = "";
            Role role = new Role();
            role.Rdesc = rdesc;
            role.Rname = rname;

            if (rid != null && !rid.Equals(""))
            {
                role.Rid = int.Parse(rid);
            }
            else
            {
                role.Rid = null;
            }
            RoleService roleService = new RoleService();
            msg = roleService.UpdateRole(role);
            return Json(msg);
        }

        /**
         *  删除角色执行代码
         */
        public ActionResult doRoleDelete(String rid)
        {
            String msg = "";
            Role role = new Role();
            role.Rid = int.Parse(rid);
            RoleService roleService = new RoleService();
            msg = roleService.DeleteRole(role);
            return Json(msg);
        }


        /**
         * 角色授权（数据回显）
         */
        public ActionResult RoleResource(String rid)
        {
            RoleService roleService = new RoleService();
            Role role = roleService.GetRoleMenuByRid(rid);
            ViewBag.Role = role;
            return PartialView(ViewBag);
        }
    }
}