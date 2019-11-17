using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyNPO.Models
{
    public class DependentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[ForeignKey("FamilyInfo")]
        public Guid PrimaryId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public string DOB { get; set; }
        public string RelationShip { get; set; }
    }
}