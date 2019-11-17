using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace MyNPO.Models
{
    public class FamilyInfo
    {
        [Key]
        public Guid PrimaryId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:MM/dd/yyyy}")]

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Wrong mobile number")]
        public string MobileNo { get; set; }

        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Required]
        public string Email { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

        [Required]
        public string Address { get; set; }
        public string MaritalStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? MarriageDate { get; set; }
        public int NoOfDependents { get; set; }
        public bool IsVolunteer { get; set; }
        public List<DependentInfo> DependentDetails { get; set; }
        public DateTime CreateDate { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage ="Invalid donation amount")]
        public string Donation { get; set; }
        public string DonationReason { get; set; }
    }
}