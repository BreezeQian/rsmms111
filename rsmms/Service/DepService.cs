using rsmms.Models;
using rsmms.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace rsmms.Service
{
    public class DepService
    {



        public List<Dep> SelectDeps(Dep depQuery)
        {

            List<Dep> depList = new List<Dep>();
            String sql = "select d.*  from Dep d where 1=1";
            if (depQuery.Dname != null && !(depQuery.Dname.Equals("")))
            {
                sql += " and d.dname like N'%" + depQuery.Dname + "%' ";
            }
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {

                Dep dep = new Dep();

                dep.Did = int.Parse(dr["Did"].ToString());
                dep.Dname = dr["Dname"].ToString();
                dep.Ddesc = dr["Ddesc"].ToString();

                depList.Add(dep);

            }
            return depList;
        }

        /**
         * 编辑弹出层数据回显用
         */ 
        public Dep GetDepByDid(String did)
        {
            Dep dep = new Dep();
            String sql = "select d.did, d.dname, d.ddesc from Dep d where 1=1"
                + " and d.did = " + did;
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {
                dep.Did = int.Parse(dr["Did"].ToString());
                dep.Dname = dr["Dname"].ToString();
                dep.Ddesc = dr["Ddesc"].ToString();
            }
            return dep;
        }

        //新增
        public String AddDep(Dep dep)
        {
            String msg = "";
            Boolean isExist = false;
            //先查询有无该部门
            String sql2 = "select * from Dep d where d.dname = N'" + dep.Dname + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql2);
            if (dr.HasRows)
            {
                isExist = true;
                msg = "该部门已存在";
            }
            if (!isExist)
            {
                String sql = "insert into Dep (dname, ddesc) values"
                        + " (N'" + dep.Dname + "',N'" + dep.Ddesc + "')";
                int count = DBUtil.ExecuteNonQuery(sql);
                msg = "新增成功";
            }

            return msg;
        }


        /**
         * 删除部门（先把与之关系的员工表中的did置为NULL）
         */
        public String Deletedep(Dep dep)
        {
            String msg = "";
            String sql1 = "update Emp  set did = NULL where did = " + dep.Did;
            DBUtil.ExecuteNonQuery(sql1);
            String sql = "delete Dep where did = " + dep.Did;
            DBUtil.ExecuteNonQuery(sql);
            msg = "删除成功";
            return msg;
        }

        /**
            * 修改部门
            */
        public String UpdateDep(Dep dep)
        {
            String msg = "";
            String sql = "update Dep set dname=N'" + dep.Dname + "',"
                + "ddesc=N'" + dep.Ddesc + "'  where did =" + dep.Did;
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
    }
}







