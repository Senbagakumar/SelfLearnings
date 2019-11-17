using MyNPO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace WebJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ReceiptGenerator.GenerateYearEndDonationReceiptPdf();
        }
    }
}
