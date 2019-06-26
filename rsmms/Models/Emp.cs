using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rsmms.Models
{
    public class Emp
    {
        //员工编号（用于员工登录）
        private String eid;
        private String ename;
        private int age;
        private String password;
        private int? did;

        private Dep dep;
        private Role role;
        private int? rid;

        public Emp()
        {

        }

        public Emp(string eid, string ename, int age, string password, int? did)
        {
            this.eid = eid;
            this.ename = ename;
            this.age = age;
            this.password = password;
            this.did = did;
        }

        public string Eid { get => eid; set => eid = value; }
        public string Ename { get => ename; set => ename = value; }
        public int Age { get => age; set => age = value; }
        public string Password { get => password; set => password = value; }
        public int? Did { get => did; set => did = value; }
        public int? Rid { get => rid; set => rid = value; }
        public Dep Dep { get => dep; set => dep = value; }
        public Role Role { get => role; set => role = value; }
    }
}