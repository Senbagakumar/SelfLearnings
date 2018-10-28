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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            var lmodel = new List<UICity>();
            var city = new List<City>();
            var state = new List<State>();

            using (var repository = new Repository<City>())
            {
                city = repository.All().ToList();
                state = repository.AssessmentContext.states.ToList();
            }

            var list = city.Join(state, r => r.StateId, t => t.Id, (r, t) => new UICity() { Id = r.Id, StateId = t.Id, CityName = r.CityName, StateName = t.StateName });

            int i = 1;
            foreach(var cy in list)
            {
                var cty = new UICity() { Id=cy.Id, CityName=cy.CityName, StateId=cy.StateId, StateName=cy.StateName, OrderNo=i};
                i = i + 1;
                lmodel.Add(cty);
            }

            var lmod = new List<SelectListItem>();            
            lmod = state.Select(t => new SelectListItem() { Value = t.Id.ToString(), Text = t.StateName }).ToList();
            lmod.Insert(0, new SelectListItem() { Text = "-- Select --", Value = "0", Selected = true });

            ViewBag.StateList = lmod;

            return View(lmodel);
        }

        
        // POST: City/Create
        [HttpPost]
        public JsonResult Create(UICity uICity)
        {
            try
            {
                using (var repository = new Repository<City>())
                {
                    if (uICity.Id != 0)
                    {
                        var uiCity = repository.Filter(q => q.Id == uICity.Id).FirstOrDefault();
                        if (uiCity != null && !string.IsNullOrEmpty(uiCity.CityName))
                        {
                            uiCity.CityName = uICity.CityName;
                            uiCity.Id = uICity.Id;
                            uiCity.UpdateDate = DateTime.Now;
                            repository.Update(uiCity);
                        }
                    }
                    else
                    {
                        var cId = repository.All().Count();
                        var city = new City()
                        {
                            Id = ++cId,
                            CityName = uICity.CityName,
                            CreateDate = DateTime.Now,
                            StateId = uICity.StateId,
                            States = repository.AssessmentContext.states.FirstOrDefault(q => q.Id == uICity.StateId)
                        };
                        repository.Create(city);
                    }
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return Json("Failiure", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        
        // GET: City/Delete/5
        public JsonResult Delete(int id)
        {
            using (var repository = new Repository<City>())
            {
                var deleteCity = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteCity != null && !string.IsNullOrEmpty(deleteCity.CityName))
                {
                    repository.Delete(deleteCity);
                }
                repository.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

       
    }
}
