using rsmms.Models;
using rsmms.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsmms.Service
{
    public class DepService
    {
        
        

        public List<Dep> SelectDeps()
        {
            
            List<Dep> depList = new List<Dep>();
            String sql = "select d.*  from Dep d where 1=1 ";
            
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
        
    }
}