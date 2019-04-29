using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using SelfAssessment.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SelfAssessment.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class UserController : Controller
    {
        private readonly IBusinessContract businessContract;


        public UserController(IBusinessContract businessContract)
        {
            this.businessContract = businessContract;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session[Utilities.UserId] = null;
            Session[Utilities.Usermail] = null;
            Session["AllGroupQuestions"] = null;
            Session["AllQuestions"] = null;
            Session.Clear();
            Session.Abandon();

            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (email == "Admin@gmail.com" && password == "Admin")
            {
                Session["Admin"] = "Admin";
                return Redirect(Utilities.RedirectToAdmin);
            }
            else
            {
                int UserId = 0;
                UserId = this.businessContract.LoginVerfication(email, password);
                if (UserId != 0)
                {
                    Session[Utilities.UserId] = UserId;
                    Session[Utilities.Usermail] = email;
                    return Redirect(Utilities.RedirectToUser);
                }
                else
                    return View();
            }
        }

        [HttpGet]
        public JsonResult GetSubSector(int id)
        {
            var subSector = new List<SelectListItem>();
           //var lastItem = new SelectListItem() { Text =Utilities.Others, Value = Utilities.OthersValue };
           
            using (var repository = new Repository<SubSector>())
            {
                subSector = repository.Filter(q=> q.SectorId == id).Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
            }
            //subSector.Add(lastItem);
            return Json(subSector,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCities(int id)
        {
            var cities = new List<SelectListItem>();
            //var firstItem = new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true };            

            using (var repository = new Repository<City>())
            {
                cities = repository.Filter(q => q.StateId == id).Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
            }
            //subSector.Insert(0, firstItem);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
         
            var type = new List<SelectListItem>();         

            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            type.Add(firstItem);

            type.Add(new SelectListItem() { Text = Utilities.Small, Value = "1" });
            type.Add(new SelectListItem() { Text = Utilities.Large, Value = "2" });
            type.Add(new SelectListItem() { Text = Utilities.OperatingUnit, Value = "3" });

           
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
            var cities = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();

            using (var repository = new Repository<Sector>())
            {
                sector = repository.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SectorName }).ToList();
                states = repository.AssessmentContext.states.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
                revenue = repository.AssessmentContext.revenues.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                typeOfService = repository.AssessmentContext.serviceTypes.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();                
            }

            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = "1000" };

            sector.Insert(0, firstItem);           
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            cities.Insert(0, firstItem);
            states.Insert(0, firstItem);

            sector.Add(lastItem);
            ViewBag.SubSectorList = new List<SelectListItem>();
            ViewBag.SectorList = sector;            
            ViewBag.City = new List<SelectListItem>();
            ViewBag.State = states;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;
            ViewBag.Type = type;
            return View();
        }

        public ActionResult ForGetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForGetPassword(string email)
        {
            this.businessContract.ForGetPassword(email);
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public JsonResult CreateOrganization(UIOrganization organization)
        {
            string errorMsg = string.Empty;
            try
            {

               var validation=this.businessContract.UserCreation(organization);
                if (validation.IsSuccess)
                    errorMsg = Utilities.Success;
                else
                    errorMsg = Utilities.Failiure;

                // TODO: Add insert logic here

                //return RedirectToAction("SuccesserrorMsg
            }
            catch(Exception ex)
            {
                UserException.LogException(ex);
                //return View();
            }
           return Json(errorMsg, JsonRequestBehavior.AllowGet);
        }
      
    }
}
