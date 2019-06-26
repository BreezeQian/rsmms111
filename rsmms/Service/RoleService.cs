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
        
        

        public List<Role> SelectRoles()
        {
            
            List<Role> roleList = new List<Role>();
            String sql = "select r.*  from Role r ";
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
        
    }
}