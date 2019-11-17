using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyNPO.Models
{

    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Guid TransactionGuid { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string TimeZone { get; set; }
        public string Description { get; set; }
        public string CurrencyType { get; set; }
        public string Gross { get; set; }
        public string Fee { get; set; }
        public string Net { get; set; }
        public string TransactionID { get; set; }
        public string FromEmailAddress { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string ShippingAmount { get; set; }
        public string SalesTax { get; set; }
        public string InvoiceID { get; set; }
        public string ReferenceTxnID { get; set; }
        public DateTime UploadDateTime { get; set; }
        public string PhoneNo { get; set; }
        public string Reason { get; set; }
        public string TypeOfReport { get; set; }
        public string PriestServicesList { get; set; }

    }

    public class PriestServices
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Wrong mobile number")]
        public string Phone { get; set; }
        public string DonationAmount { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public string PriestServicesList { get; set; }
        public string Gothram { get; set; }
        public DateTime EventsDate { get; set; }
        public string EventTime { get; set; }
        public string Address { get; set; }
        public string PaymentMode { get; set; }
        public DHXScheduler Scheduler { get; set; }
    }
}