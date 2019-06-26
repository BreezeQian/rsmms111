using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsmms.Models
{
    public class Dep
    {
        private int did;
        private String dname;
        private String ddesc;

        public Dep()
        {

        }

        public Dep(int did, string dname, string ddesc)
        {
            this.did = did;
            this.dname = dname;
            this.ddesc = ddesc;
        }

        public int Did { get => did; set => did = value; }
        public string Dname { get => dname; set => dname = value; }
        public string Ddesc { get => ddesc; set => ddesc = value; }
    }
}