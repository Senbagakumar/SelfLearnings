using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bot
{
    public class InputModal
    {
    }
    public class Permission
    {
        public string ProjectID { get; set; }
        public string Name { get; set; }
    }

    public class Certification
    {
        public string ID { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }

    public class Owner
    {
        public string name { get; set; }
        public List<Permission> Permission { get; set; }
        public List<Certification> Certifications { get; set; }
    }

    public class BuddyBOT
    {
        public List<Owner> Owners { get; set; }
    }

    public class RootObject
    {
        public BuddyBOT BuddyBOT { get; set; }
    }
}
