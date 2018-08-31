using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SelfAssessment.Controllers
{
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

        [HttpPost]
        public ActionResult Login(UILogin login)
        {

            if (this.businessContract.LoginVerfication(login.UserName, login.UserPwd))
                return RedirectToAction("Index", "ManageUser");
            else
                return View();
        }
        public ActionResult Register()
        {
            var sectorList = new List<SelectListItem>();
            var subSectorList = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();
            var type = new List<SelectListItem>();
            var city = new List<SelectListItem>();
            var state = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();

            var first = new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true };
            type.Add(first);
            type.Add(new SelectListItem() { Text = "Small", Value = "1" });
            type.Add(new SelectListItem() { Text = "Large", Value = "2" });
            type.Add(new SelectListItem() { Text = "Operating Unit", Value = "3" });

            var subSector = new List<SubSector>();
            var sector = new List<Sector>();

            using (var repository = new Repository<SubSector>())
            {
                subSector = repository.All().ToList();
            }

            using (var repository = new Repository<Sector>())
            {
                sector = repository.All().ToList();
            }



            sectorList.Add(first);
            subSectorList.Add(first);
            revenue.Add(first);
           
            city.Add(first);
            state.Add(first);
            typeOfService.Add(first);



            for (int i = 1; i <= 10; i++)
            {
                var mod = new SelectListItem();
                mod.Text = "SectorName" + i;
                mod.Value =  i.ToString();
                sectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "SubSectorName" + i;
                mod.Value = i.ToString();
                subSectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "City" + i;
                mod.Value =  i.ToString();
                city.Add(mod);

                mod = new SelectListItem();
                mod.Text = "State" + i;
                mod.Value = i.ToString();
                state.Add(mod);

                mod = new SelectListItem();
                mod.Text = "Revenue" + i;
                mod.Value = i.ToString();
                revenue.Add(mod);

                mod = new SelectListItem();
                mod.Text = "TypesOfService" + i;
                mod.Value = i.ToString();
                typeOfService.Add(mod);

            }

            sectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "1000" });
            subSectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "1000" });

            ViewBag.SectorList = sectorList;
            ViewBag.SubSectorList = subSectorList;
            ViewBag.City = city;
            ViewBag.State = state;
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
            try
            {

                this.businessContract.UserCreation(organization);
                

                // TODO: Add insert logic here

                //return RedirectToAction("Success");
            }
            catch
            {
                //return View();
            }
           return Json("Success", JsonRequestBehavior.AllowGet);
        }
      
    }
}
