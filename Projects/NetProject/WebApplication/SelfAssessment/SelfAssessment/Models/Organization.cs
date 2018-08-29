using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{  

    public class UIOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string TypeOfService { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
        public string Revenue { get; set; }
        public string Type { get; set; }

    }

    public class UILogin
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
    
    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserId { get; set; }
    }
}