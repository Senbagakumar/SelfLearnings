using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNPO.Models
{
    public class DonorInfo
    {
        public string Name { get; set; }
        public string DonorEmailAddress { get; set; }
        public string TransactionID { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }
        public string Net { get; set; }
    }
}