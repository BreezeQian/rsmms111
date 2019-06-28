using rsmms.Models;
using rsmms.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsmms.Service
{
    public class RoleService
    {
        
        

        public List<Role> SelectRoles(Role roleQuery)
        {
            
            List<Role> roleList = new List<Role>();
            String sql = "select r.*  from Role r where 1=1 ";
            if(roleQuery.Rname != null && !roleQuery.Rname.Equals(""))
            {
                sql += " and r.rname like N'%" + roleQuery.Rname + "%'";
            }
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {

                Role role = new Role();

                role.Rid = int.Parse(dr["Rid"].ToString());
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();

                roleList.Add(role);
                
            }
            return roleList;
        }

        //新增
        public String AddRole(Role role)
        {
            String msg = "";
            Boolean isExist = false;
            //先查询有无该角色
            String sql2 = "select * from Role r where r.rname = N'" + role.Rname + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql2);
            if (dr.HasRows)
            {
                isExist = true;
                msg = "该角色已存在";
            }
            if (!isExist)
            {
                String sql = "insert into Role (rname, rdesc) values"
                        + " (N'" + role.Rname + "',N'" + role.Rdesc + "')";
                int count = DBUtil.ExecuteNonQuery(sql);
                msg = "新增成功";
            }

            return msg;
        }


        /**
         * 编辑弹出层数据回显用
         */ 
        public Role GetRoleByRid(String rid)
        {
            Role role = new Role();
            String sql = "select r.rid, r.rname, r.rdesc from Role r where 1=1 "
                + " and r.rid = " + rid;
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {
                role.Rid = int.Parse(dr["Rid"].ToString());
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();
            }
            return role;
        }


        /**
            * 修改角色
            */
        public String UpdateRole(Role role)
        {
            String msg = "";
            String sql = "update Role set rname=N'" + role.Rname + "',"
                + "rdesc=N'" + role.Rdesc + "'  where rid =" + role.Rid;
            int count = DBUtil.ExecuteNonQuery(sql);
            if (count == 1)
            {
                msg = "修改成功";
            }

            else
            {
                msg = "修改成功";
            }
            return msg;
        }

        /**
         * 删除角色（先把与之关系的员工角色表和角色菜单表中的相关数据删掉）
         */
        public String DeleteRole(Role role)
        {
            String msg = "";
            String sql1 = "delete relation_role_emp where rid = " + role.Rid;
            DBUtil.ExecuteNonQuery(sql1);
            String sql2 = "delete relation_role_menu where rid = " + role.Rid;
            DBUtil.ExecuteNonQuery(sql2);
            String sql = "delete Role where rid = " + role.Rid;
            DBUtil.ExecuteNonQuery(sql);
            msg = "删除成功";
            return msg;
        }

        /**
         * 角色授权弹出层数据回显用
         */
        public Role GetRoleMenuByRid(String rid)
        {
            Role role = new Role();
            List<Menu> menus = new List<Menu>();
            String sql = "select r.*, m.* from Role r, relation_role_menu rm, Menu m "
                       + " where r.rid = rm.rid "
                       + " and rm.mid = m.mid "
                       + " and r.rid = " + int.Parse(rid);
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {
                role.Rid = int.Parse(dr["Rid"].ToString());
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();

                Menu menu = new Menu();
                menu.Mid = int.Parse(dr["Mid"].ToString());
                menu.Mname = dr["Mname"].ToString();
                menu.Href = dr["Href"].ToString();
                menu.Remark = dr["Remark"].ToString();
                if(dr["Level"].ToString() != null && !dr["Level"].ToString().Equals(""))
                {
                    menu.Level = int.Parse(dr["Level"].ToString());
                }
                if (dr["Parent_mid"].ToString() != null && !dr["Parent_mid"].ToString().Equals(""))
                {
                    menu.Parent_mid = int.Parse(dr["Parent_mid"].ToString());
                }
                menus.Add(menu);
            }
            role.Menus = menus;
            return role;
        }
    }
}