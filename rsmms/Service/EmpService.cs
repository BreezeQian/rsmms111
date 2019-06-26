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

                dep.Did = int.Parse(dr["Did"].ToString());
                dep.Dname = dr["Dname"].ToString();
                dep.Ddesc = dr["Ddesc"].ToString();
                emp.Dep = dep;

                role.Rid = int.Parse(dr["Rid"].ToString());
                role.Rname = dr["Rname"].ToString();
                role.Rdesc = dr["Rdesc"].ToString();
                emp.Role = role;

                empList.Add(emp);
                
            }
            return empList;
        }
        
    }
}