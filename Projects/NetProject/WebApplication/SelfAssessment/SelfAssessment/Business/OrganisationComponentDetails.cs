using SelfAssessment.DataAccess;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Business
{
    public class OrganisationComponentDetails : IOrganizationComponentDetails
    {
        Repository<Assessment> repository;
        public OrganisationComponentDetails()
        {
            repository = new Repository<Assessment>();
        }

        public List<SelectListItem> GetAssessment()
        {
            return repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
        }

        public List<SelectListItem> GetCity()
        {
            return repository.AssessmentContext.cities.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
        }

        public List<SelectListItem> GetRevenue()
        {
            return repository.AssessmentContext.revenues.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
        }

        public List<SelectListItem> GetSector()
        {
            return repository.AssessmentContext.sectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
        }

        public List<SelectListItem> GetServiceType()
        {
            return repository.AssessmentContext.serviceTypes.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
        }

        public List<SelectListItem> GetState()
        {
            return repository.AssessmentContext.states.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
        }

        public List<SelectListItem> GetSubSector()
        {
            return repository.AssessmentContext.subSectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
        }
    }
}