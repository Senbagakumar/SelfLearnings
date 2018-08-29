using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;

namespace SelfAssessment.Mapper
{
    public class OrganizationMapper : IOrganizationMapper
    {
        public Organization Registration(UIOrganization uiOrganization)
        {
            return new Organization()
            {
                Address = uiOrganization.Address,
                ContactName = uiOrganization.ContactName,
                Designation = uiOrganization.Designation,
                Email = uiOrganization.Email,
                MobileNo = uiOrganization.MobileNo,
                Name = uiOrganization.Name,
                CityId = Convert.ToInt16(uiOrganization.City),
                RevenueId = Convert.ToInt16(uiOrganization.Revenue),
                SectorId = Convert.ToInt16(uiOrganization.Sector),
                SubSectorId = Convert.ToInt16(uiOrganization.SubSector),
                TypeId = Convert.ToInt16(uiOrganization.Type),
                TypeOfServiceId = Convert.ToInt16(uiOrganization.TypeOfService),
                UserId = uiOrganization.Name.Substring(0, 6),
                StateId =Convert.ToInt16(uiOrganization.State)            
                
            };
        }
    }
}