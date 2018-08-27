using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class UserController : Controller
    {
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

            type.Add(new SelectListItem() { Text = "Small", Value = "Small" });
            type.Add(new SelectListItem() { Text = "Large", Value = "Large" });
            type.Add(new SelectListItem() { Text = "Operating Unit", Value = "Operating Unit" });

            var first = new SelectListItem() { Text = "-- Select --", Value = "-- Select --", Selected = true };
            sectorList.Add(first);


            for (int i = 1; i <= 10; i++)
            {
                var mod = new SelectListItem();
                mod.Text = "SectorName" + i;
                mod.Value = "SectorName" + i;
                sectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "SubSectorName" + i;
                mod.Value = "SubSectorName" + i;
                subSectorList.Add(mod);

                mod = new SelectListItem();
                mod.Text = "City" + i;
                mod.Value = "City" + i;
                city.Add(mod);

                mod = new SelectListItem();
                mod.Text = "State" + i;
                mod.Value = "State" + i;
                state.Add(mod);

                mod = new SelectListItem();
                mod.Text = "Revenue" + i;
                mod.Value = "Revenue" + i;
                revenue.Add(mod);

                mod = new SelectListItem();
                mod.Text = "TypesOfService" + i;
                mod.Value = "TypesOfService" + i;
                typeOfService.Add(mod);

            }

            sectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "OTHERS" });
            subSectorList.Add(new SelectListItem() { Text = "OTHERS", Value = "OTHERS" });

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
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Register(UIOrganization organization)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
    }
}
