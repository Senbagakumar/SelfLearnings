using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class ManageOrgController : Controller
    {
        private readonly IOrganizationComponentDetails organizationComponentDetails;

        public ManageOrgController()//IOrganizationComponentDetails organizationComponentDetails
        {
            this.organizationComponentDetails = new OrganisationComponentDetails();
        }
        // GET: ManageOrg
        public ActionResult Index()
        {
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = Utilities.OthersValue };

            var subSector = new List<SelectListItem>();
            var txtsubSector = new List<SelectListItem>();
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
            var cities = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();
            var assessMent = new List<SelectListItem>();

            txtsubSector = this.organizationComponentDetails.GetSubSector();
            sector = this.organizationComponentDetails.GetSector();
            states = this.organizationComponentDetails.GetState();
            cities = this.organizationComponentDetails.GetCity();
            typeOfService = this.organizationComponentDetails.GetServiceType();
            revenue = this.organizationComponentDetails.GetRevenue();
            assessMent = this.organizationComponentDetails.GetAssessment();

            sector.Insert(0, firstItem);
            subSector.Insert(0, firstItem);
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            cities.Insert(0, firstItem);
            states.Insert(0, firstItem);
            assessMent.Insert(0, firstItem);
            txtsubSector.Insert(0, firstItem);

            sector.Add(lastItem);
            subSector.Add(lastItem);
            txtsubSector.Add(lastItem);

            ViewBag.AssessMent = assessMent;
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = subSector;
            ViewBag.City = cities;
            ViewBag.State = states;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;
            ViewBag.TxtSubSector = txtsubSector;

            return View();
        }

        [HttpGet]
        public JsonResult DeleteOrganization(int id)
        {
            var repo = new Repository<Organization>();
            var org = repo.Filter(q => q.Id == id).FirstOrDefault();
            repo.Delete(org);
            repo.SaveChanges();
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateOrganization(UIOrganization organization)
        {
            var repo = new Repository<Organization>();
            var org = repo.Filter(q => q.Id == organization.Id).FirstOrDefault();

            org.MobileNo = organization.MobileNo;
            org.Name = organization.Name;
            org.Address = organization.Address;
            org.CityId = Convert.ToInt16(organization.City);
            org.ContactName = organization.ContactName;
            org.Designation = organization.Designation;
            org.Email = organization.Email;
            org.Password = organization.Password;
            org.RevenueId = Convert.ToInt16(organization.Revenue);
            org.SectorId = Convert.ToInt16(organization.Sector);
            org.StateId = Convert.ToInt16(organization.State);
            org.SubSectorId = Convert.ToInt16(organization.SubSector);
            org.TypeOfServiceId = Convert.ToInt16(organization.TypeOfService);

            org.Cities = repo.AssessmentContext.cities.FirstOrDefault(q => q.Id == org.CityId);
            org.States = repo.AssessmentContext.states.FirstOrDefault(q => q.Id == org.StateId);
            org.Revenues = repo.AssessmentContext.revenues.FirstOrDefault(q => q.Id == org.RevenueId);
            org.TypesOfService = repo.AssessmentContext.serviceTypes.FirstOrDefault(q => q.Id == org.Id);
            org.Sectors = repo.AssessmentContext.sectors.FirstOrDefault(q => q.Id == org.SectorId);
            org.SubSectors = repo.AssessmentContext.subSectors.FirstOrDefault(q => q.Id == org.SubSectorId);
            


            repo.Update(org);
            repo.SaveChanges();
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ViewOrganization(int id, string type)
        {
            var model = new UIOrganization();
            var repo = new Repository<Organization>();
            var org = repo.Filter(q => q.Id == id).FirstOrDefault();

            model.Id = org.Id;
            model.Name = org.Name;
            model.Address = org.Address;
            model.ContactName = org.ContactName;
            model.Designation = org.Designation;
            model.Email = org.Email;
            model.MobileNo = org.MobileNo;
            

            if (type == "View")
            {
                model.City = repo.AssessmentContext.cities.FirstOrDefault(c => c.Id == org.CityId).CityName;
                model.Revenue = repo.AssessmentContext.revenues.FirstOrDefault(r => r.Id == org.RevenueId).Name;
                model.Sector = (org.SectorId == 0 || org.SectorId == Convert.ToInt16(Utilities.OthersValue)) ? Utilities.Others : repo.AssessmentContext.sectors.FirstOrDefault(r => r.Id == org.SectorId).SectorName;
                model.SubSector = (org.SectorId == 0 || org.SubSectorId == Convert.ToInt16(Utilities.OthersValue)) ? Utilities.Others : repo.AssessmentContext.subSectors.FirstOrDefault(r => r.Id == org.SubSectorId).SubSectorName;
                model.State = org.StateId > 0 ? repo.AssessmentContext.states.FirstOrDefault(s => s.Id == org.StateId).StateName : "";
                model.TypeOfService = org.TypeOfServiceId > 0 ? repo.AssessmentContext.serviceTypes.FirstOrDefault(ty => ty.Id == org.TypeOfServiceId).Name : "";
              
            }
            else
            {               
                model.City = org.CityId.ToString();
                model.Revenue = org.RevenueId.ToString();
                model.Sector = org.SectorId.ToString();
                model.SubSector = org.SubSectorId.ToString();
                model.State = org.StateId.ToString();
                model.TypeOfService = org.TypeOfServiceId.ToString();
            }

                return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AssignOrganizationByFilter(Organization org)
        {
            var lmodel = new List<UIOrganization>();
            var listOrganization = new List<Organization>();
            using (var repo = new Repository<Organization>())
            {
                listOrganization = repo.AssessmentContext.UserInfo.ToList();
            }

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
                    model.Name = q.Name;
                    model.City = city.Filter(c => c.Id == q.CityId).FirstOrDefault().CityName;
                    model.Revenue = city.AssessmentContext.revenues.FirstOrDefault(r => r.Id == q.RevenueId).Name;
                    model.Sector = (q.SectorId == 0 || q.SectorId == Convert.ToInt16(Utilities.OthersValue)) ? Utilities.Others : city.AssessmentContext.sectors.FirstOrDefault(r => r.Id == q.SectorId).SectorName;
                    model.SubSector = (q.SectorId == 0 || q.SubSectorId == Convert.ToInt16(Utilities.OthersValue)) ? Utilities.Others : city.AssessmentContext.subSectors.FirstOrDefault(r => r.Id == q.SubSectorId).SubSectorName;
                    model.State = q.StateId > 0 ? city.AssessmentContext.states.FirstOrDefault(s => s.Id == q.StateId).StateName : "";
                    model.TypeOfService = q.TypeOfServiceId > 0 ? city.AssessmentContext.serviceTypes.FirstOrDefault(ty => ty.Id == q.TypeOfServiceId).Name : "";
                    
                    lmodel.Add(model);
                });
            }
            catch (Exception ex)
            {
                //throw;
            }

            return Json(lmodel, JsonRequestBehavior.AllowGet);
        }
    }
}