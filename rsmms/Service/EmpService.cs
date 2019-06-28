using rsmms.Models;
using rsmms.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsmms.Service
{
    public class EmpService
    {
        
        

        public List<Emp> SelectEmps(Emp empQuery)
        {
            List<Emp> empList = new List<Emp>();
            String sql = "select e.*, d.dname, d.ddesc, x.rid, x.rname, x.rdesc  from Emp e "
                + "left join Dep d on e.did = d.did "
                + "left join (select re.eid, r.* from Role r, relation_role_emp re where r.rid = re.rid) x on x.eid = e.eid where 1=1 ";
            if(empQuery.Eid != null && !(empQuery.Eid.Equals("")))
            {
                sql += " and e.eid like N'%" + empQuery.Eid + "%' ";
            }if(empQuery.Ename != null && !(empQuery.Ename.Equals("")))
            {
                sql += " and e.ename like N'%" + empQuery.Ename + "%' ";
            }if((empQuery.Did+"") != null && !((empQuery.Did+"").Equals("")))
            {
                sql += " and e.did = " + empQuery.Did;
            }
            if ((empQuery.Rid + "") != null && !((empQuery.Rid + "").Equals("")))
            {
                sql += " and x.rid = " + empQuery.Rid;
            }
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {
                Emp emp = new Emp();
                Dep dep = new Dep();
                Role role = new Role();
                emp.Eid = dr["Eid"].ToString();
                emp.Ename = dr["Ename"].ToString();
                emp.Age = int.Parse(dr["Age"].ToString());
                emp.Password = dr["Password"].ToString();

                if(dr["Did"].ToString() != null && !dr["Did"].ToString().Equals(""))
                {
                    dep.Did = int.Parse(dr["Did"].ToString());
                }
                dep.Dname = dr["Dname"].ToString();
                dep.Ddesc = dr["Ddesc"].ToString();
                emp.Dep = dep;

                if(dr["Rid"].ToString() != null && !dr["Rid"].ToString().Equals(""))
                {
                    role.Rid = int.Parse(dr["Rid"].ToString());
                } 
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();
                emp.Role = role;

                empList.Add(emp);
                
            }
            return empList;
        }


        public Emp GetEmpByEid(String eid)
        {
            Emp emp = new Emp();
            String sql = "select e.*, d.dname, d.ddesc, x.rid, x.rname, x.rdesc  from Emp e "
                + "left join Dep d on e.did = e.did "
                + "left join (select re.eid, r.* from Role r, relation_role_emp re where r.rid = re.rid) x on x.eid = e.eid where 1=1 "
                + " and e.eid = '" + eid + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            { 
                Dep dep = new Dep();
                Role role = new Role();
                emp.Eid = dr["Eid"].ToString();
                emp.Ename = dr["Ename"].ToString();
                emp.Age = int.Parse(dr["Age"].ToString());
                emp.Password = dr["Password"].ToString();

                if (dr["Did"].ToString() != null && !dr["Did"].ToString().Equals(""))
                {
                    dep.Did = int.Parse(dr["Did"].ToString());
                }
                dep.Dname = dr["Dname"].ToString();
                dep.Ddesc = dr["Ddesc"].ToString();
                emp.Dep = dep;

                if (dr["Rid"].ToString() != null && !dr["Rid"].ToString().Equals(""))
                {
                    role.Rid = int.Parse(dr["Rid"].ToString());
                }
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();
                emp.Role = role;
            }
            return emp;
        } 
        /**
         * 新增员工
         */ 
        public String AddEmp(Emp emp, String rid)
        {
            String msg = "";
            Boolean isExist = false;
            //先查询有无该员工
            String sql2 = "select * from Emp e where e.eid = '" + emp.Eid + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql2);
            if (dr.HasRows)
            {
                isExist = true;
                msg = "该用户账号已经被注册";
            }
            if (!isExist)
            {
                String sql = "insert into Emp (eid, password, ename, age, did) values"
                        + " (N'" + emp.Eid + "',N'" + emp.Password + "',N'" + emp.Ename + "'," + emp.Age + "," + emp.Did + ")";
                int count = DBUtil.ExecuteNonQuery(sql);
                if (count == 1)
                {
                    if (rid != null && !rid.Equals(""))
                    {
                        String sql1 = "insert into relation_role_emp  values"
                                 + " (N'" + emp.Eid + "'," + int.Parse(rid) + ")";
                        int count1 = DBUtil.ExecuteNonQuery(sql1);
                        if (count1 == 1)
                        {
                            msg = "新增成功";
                        }
                    }
                    else
                    {
                        msg = "新增成功";
                    }
                }
            }

            return msg;
        }

        /**
         * 修改员工
         */
        public String UpdateEmp(Emp emp, String rid)
        {
            String msg = "";
            String sql = "update Emp set ename=N'"+emp.Ename+"',"
                + "age=" + emp.Age + ", did=" + emp.Did +"  where eid = N'"+emp.Eid +"'";
                int count = DBUtil.ExecuteNonQuery(sql);
                if (count == 1)
                {
                    String sql2 = "delete relation_role_emp where eid = N'" + emp.Eid + "'";
                    DBUtil.ExecuteNonQuery(sql2);
                if (rid != null && !rid.Equals(""))
                    {

                        String sql1 = "insert into relation_role_emp  values"
                                 + " (N'" + emp.Eid + "'," + int.Parse(rid) + ")";
                        int count1 = DBUtil.ExecuteNonQuery(sql1);
                        if (count1 == 1)
                        {
                            msg = "修改成功";
                        }
                    }
                    else
                    {
                        msg = "修改成功";
                    }
                    
                }
            

            return msg;
        }

        /**
         * 删除员工（先删除与之关系的角色）
         */
        public String DeleteEmp(Emp emp)
        {
            String msg = "";
            String sql = "delete relation_role_emp where eid = N'" + emp.Eid + "'";
            DBUtil.ExecuteNonQuery(sql);
            String sql1 = "delete Emp where eid = N'" + emp.Eid + "'";
            DBUtil.ExecuteNonQuery(sql1);
            msg = "删除成功";
            return msg;
        }

        /**
         * 根据eid和password来查询（登录用）
         */ 
        public Emp SelectEmpByEidAndPassword(String eid, String password)
        {
            Emp emp = new Emp();
            String sql = "select e.* from Emp e where e.eid=N'" + eid + "'" + " and e.password=N'" + password + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.Eid = dr["Eid"].ToString();
                    emp.Ename = dr["Ename"].ToString();
                    if (dr["Age"].ToString() != null && !dr["Age"].ToString().Equals(""))
                    {
                        emp.Age = int.Parse(dr["Age"].ToString());
                    }  
                    emp.Password = dr["Password"].ToString();
                    if (dr["Did"].ToString() != null && !dr["Did"].ToString().Equals(""))
                    {
                        emp.Did = int.Parse(dr["Did"].ToString());
                    }
                }
            }
            return emp;
        }


        /**
         * 修改密码（先根据eid和password查询有无数据，有说明旧密码正确，再修改新密码）
         */ 
        public String UpdateEmpPassword(String eid, String password, String newPassword)
        {
            String msg = "";
            String sql = "select e.* from Emp e where e.eid=N'" + eid + "'" + " and e.password=N'" + password + "'";
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            if (dr.HasRows)
            {
                String sql1 = "update Emp set password = N'"+ newPassword + "' where eid = N'"+ eid +"'";
                int count = DBUtil.ExecuteNonQuery(sql1);
                if(count == 1)
                {
                    msg = "success";
                }
            }
            else
            {
                msg = "原密码输入错误！";
            }

            return msg;
        }
    }
}