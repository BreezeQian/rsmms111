using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsmms.Models
{
    public class Menu
    {
        private int mid;
        private String mname;
        private String href;
        private int level;
        private String remark;
        private int parent_mid;

        private Menu parent_menu;

        public Menu()
        {

        }

        public Menu(int mid, string mname, string href, int level, string remark, int parent_mid)
        {
            this.mid = mid;
            this.mname = mname;
            this.href = href;
            this.level = level;
            this.remark = remark;
            this.parent_mid = parent_mid;
        }

        public int Mid { get => mid; set => mid = value; }
        public string Mname { get => mname; set => mname = value; }
        public string Href { get => href; set => href = value; }
        public int Level { get => level; set => level = value; }
        public string Remark { get => remark; set => remark = value; }
        public int Parent_mid { get => parent_mid; set => parent_mid = value; }
        public Menu Parent_menu { get => parent_menu; set => parent_menu = value; }
    }
}