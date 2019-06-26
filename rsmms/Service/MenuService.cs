using rsmms.Models;
using rsmms.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace rsmms.Service
{
    public class MenuService
    {
        
        

        public List<Menu> SelectMenus()
        {
            
            List<Menu> menuList = new List<Menu>();
            String sql = "select m.*, m1.mname par_mname, m1.href par_href, m1.level par_level, m1.remark par_remark "+ 
            "from Menu m left join Menu m1 on m.parent_mid = m1.mid";
            SqlDataReader dr = DBUtil.ExecuteReader(sql);
            while (dr.Read())
            {

                Menu menu = new Menu();
                Menu par_menu = new Menu();

                menu.Mid = int.Parse(dr["Mid"].ToString());
                menu.Mname = dr["Mname"].ToString();
                menu.Href = dr["Href"].ToString();
                menu.Remark = dr["Remark"].ToString();
                menu.Level = int.Parse(dr["Level"].ToString());
                String parent_mid = dr["parent_mid"].ToString();
                if (parent_mid != null && !parent_mid.Equals(""))
                {
                    par_menu.Mid = int.Parse(parent_mid);
                }
                par_menu.Mname = dr["par_mname"].ToString();
                par_menu.Href = dr["par_href"].ToString();
                String par_level = dr["par_level"].ToString();
                if (par_level != null && !par_level.Equals(""))
                {
                    par_menu.Level = int.Parse(par_level);
                }
                par_menu.Remark = dr["par_remark"].ToString();

                menu.Parent_menu = par_menu;
                menuList.Add(menu);
                
            }
            return menuList;
        }
        
    }
}