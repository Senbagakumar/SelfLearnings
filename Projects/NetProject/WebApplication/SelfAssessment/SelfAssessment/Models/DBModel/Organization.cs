using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SelfAssessment.Models.DBModel
{
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int TypeOfServiceId { get; set; }
        public int TypeId { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int SectorId { get; set; }
        public int SubSectorId { get; set; }
        public int RevenueId { get; set; }
        public string TempPassword { get; set; }
        public string Password { get; set; }
        public string CurrentAssignmentType { get; set; }
        public string CurrentAssignmentStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
    public class Others
    {
        [Key]
        public string OrganizationId { get; set; }
        public string Sector { get; set; }
        public string SubSector { get; set; }
    }

    public class TempOrg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrgId { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public DateTime PromoteDate { get; set; }
    }
}