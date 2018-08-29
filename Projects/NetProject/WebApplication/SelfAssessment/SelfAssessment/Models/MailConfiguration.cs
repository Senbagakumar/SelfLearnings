using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models
{
    public class MailConfiguration
    {
        public string Host { get; set; }
        public string NetworkUserName { get; set; }
        public string NetworkPassword { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int Port { get; set; }
    }
}