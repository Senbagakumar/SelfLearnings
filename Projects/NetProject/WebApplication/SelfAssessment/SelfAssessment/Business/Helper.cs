using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Business
{
    public class Helper
    {
        public static List<UIOrganization> AssignOrganizationByFilter(Organization org)
        {
            var lmodel = new List<UIOrganization>();
            var listOrganization = new List<Organization>();
            var orgHistory = new List<OrganizationLevelHistory>();

            using (var repo = new Repository<Organization>())
            {
                listOrganization = repo.All().ToList();
                orgHistory = repo.AssessmentContext.organizationLevelHistories.ToList();
            }

            orgHistory.ForEach(t => 
            {
                listOrganization.Add(new Organization() { AssessmentId=t.AssessmentId, CurrentAssignmentType=t.Level, CityId=t.CityId, StateId=t.StateId,
                SectorId=t.SectorId, SubSectorId=t.SubSectorId, RevenueId=t.RevenueId, TypeOfServiceId=t.TypeOfServiceId, Id=t.OrgId});
            });

            //if (org.AssessmentId > 0)
            //    listOrganization = listOrganization.Where(q => q.AssessmentId == org.AssessmentId).ToList();

            if (!string.IsNullOrEmpty(org.CurrentAssignmentType))
                listOrganization = listOrganization.Where(q => q.CurrentAssignmentType == org.CurrentAssignmentType).ToList();

            if (org.CityId > 0)
                listOrganization = listOrganization.Where(q => q.CityId == org.CityId).ToList();
            if (org.StateId > 0)
                listOrganization = listOrganization.Where(q => q.StateId == org.StateId).ToList();
            if (org.SectorId > 0 && org.SectorId != 1000)
                listOrganization = listOrganization.Where(q => q.SectorId == org.SectorId).ToList();
            if (org.RevenueId > 0)
                listOrganization = listOrganization.Where(q => q.RevenueId == org.RevenueId).ToList();
            if (org.SubSectorId > 0 && org.SubSectorId != 1000)
                listOrganization = listOrganization.Where(q => q.SubSectorId == org.SubSectorId).ToList();
            if (org.TypeOfServiceId > 0)
                listOrganization = listOrganization.Where(q => q.TypeOfServiceId == org.TypeOfServiceId).ToList();

            var city = new Repository<City>();

            try
            {
                listOrganization.ForEach(q =>
                {
                    UIOrganization model = new UIOrganization();
                    model.Id = q.Id;
                    model.Name = !string.IsNullOrEmpty(q.Name) ? q.Name : city.AssessmentContext.UserInfo.FirstOrDefault(t => t.Id == q.Id).Name;
                    model.City = city.Filter(c => c.Id == q.CityId).FirstOrDefault().CityName;
                    model.Revenue = city.AssessmentContext.revenues.FirstOrDefault(r => r.Id == q.RevenueId).Name;
                    model.Sector = (q.SectorId == 0 || q.SectorId == 1000) ? Utilities.Others : city.AssessmentContext.sectors.FirstOrDefault(r => r.Id == q.SectorId).SectorName;
                    model.SubSector = (q.SectorId == 0 || q.SubSectorId == 1000) ? Utilities.Others : city.AssessmentContext.subSectors.FirstOrDefault(r => r.Id == q.SubSectorId).SubSectorName;
                    model.State = q.StateId > 0 ? city.AssessmentContext.states.FirstOrDefault(s => s.Id == q.StateId).StateName : "";
                    model.TypeOfService = q.TypeOfServiceId > 0 ? city.AssessmentContext.serviceTypes.FirstOrDefault(ty => ty.Id == q.TypeOfServiceId).Name : "";
                    model.Type = org.CurrentAssignmentType;
                    lmodel.Add(model);
                });
            }
            catch (Exception ex)
            {
                //throw;
            }

            return lmodel;
        }
    }
}