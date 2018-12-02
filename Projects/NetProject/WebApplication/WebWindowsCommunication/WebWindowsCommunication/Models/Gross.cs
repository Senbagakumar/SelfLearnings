using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebWindowsCommunication.Models
{ 


    public class Settings
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        public string ConstWt { get; set; }
        public string Factor { get; set; }
        public string Type { get; set; }
    }

    public class Wagon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Slno { get; set; }
        public string WagonWeight { get; set; }
        public string Direction { get; set; }
        public string ErrorValue { get; set; }
    }

    public class Utility
    {
        public static Dictionary<string,string> GetTypeValues()
        {
            var typeKey = new Dictionary<string, string>();
            typeKey.Add("1", "GrossMinimum");
            typeKey.Add("2", "GrossMaximum");
            typeKey.Add("3", "BiosInMinimum");
            typeKey.Add("4", "BiosInMaximum");
            typeKey.Add("5", "BiosOutMinimum");
            typeKey.Add("6", "BiosOutMaximum");
            typeKey.Add("7", "BiosOut1");
            typeKey.Add("8", "BiosOut2");
            return typeKey;
        }

        public static string GetValue(string key)
        {
            return GetTypeValues()[key];
        }
    }
}