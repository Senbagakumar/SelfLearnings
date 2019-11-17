using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyNPO.Models
{
    public class Donation
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Wrong Phone number")]
        public string Phone { get; set; }

        [Required]
        public string DonationType { get; set; }

        [Required]
        public string DonationAmount { get; set; }
        public string Reason { get; set; }
    }


    public class Login
    {
        [Required]
        [RegularExpression("[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ReportUserInfo
    {
        [Required]
        [DataType(DataType.Date)]
        public string FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ToDate { get; set; }
        [Required]
        public int TypeOfReport { get; set; }
        public List<Report> ReportInfo { get; set; }
    }

    public class FamilyReportInfo
    {
        [Required]
        [DataType(DataType.Date)]
        public string FromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ToDate { get; set; }
        [Required]
        public int TypeOfReport { get; set; }
        public List<FamilyInfo> ReportInfo { get; set; }
        
    }

    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Details { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string UploadFileName { get; set; }
    }

    public class AdminUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Wrong Phone number")]
        public string PhoneNumber { get; set; }
    }
}