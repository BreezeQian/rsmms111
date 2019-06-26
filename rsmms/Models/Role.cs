using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsmms.Models
{
    public class Role
    {
        private int rid;
        private String rname;
        private String rdesc;

        public Role()
        {

        }

        public Role(int rid, string rname, string rdesc)
        {
            this.rid = rid;
            this.rname = rname;
            this.rdesc = rdesc;
        }

        public int Rid { get => rid; set => rid = value; }
        public string Rname { get => rname; set => rname = value; }
        public string Rdesc { get => rdesc; set => rdesc = value; }
    }
}