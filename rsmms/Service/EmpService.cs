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
                + "left join Dep d on e.did = e.did "
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
    }
}