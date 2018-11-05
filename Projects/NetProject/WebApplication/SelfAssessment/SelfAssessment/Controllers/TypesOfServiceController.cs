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
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class TypesOfServiceController : AdminBaseController
    {
        // GET: TypesOfService
        public ActionResult Index()
        {
            var lmodel = new List<ServiceType>();
            var uiModel = new List<UIServiceType>();

            using (var repository = new Repository<ServiceType>())
            {
                lmodel = repository.All().ToList();
            }

            int i = 1;
            foreach(var uim in lmodel)
            {
                uiModel.Add(new UIServiceType() { Id=uim.Id, CreateDate=uim.CreateDate, Name=uim.Name, OrderId=i  });
                i = i + 1;
            }
            return View(uiModel);
        }

        // POST: TypesOfService/Create
        [HttpPost]
        public JsonResult Create(ServiceType serviceType)
        {
            try
            {
                using (var repository = new Repository<ServiceType>())
                {
                    if (serviceType.Id != 0)
                    {
                        var updateServiceType = repository.Filter(q => q.Id == serviceType.Id).FirstOrDefault();
                        if (updateServiceType != null && !string.IsNullOrEmpty(updateServiceType.Name))
                        {
                            updateServiceType.Name = serviceType.Name;
                            updateServiceType.UpdateDate = DateTime.Now;
                            repository.Update(updateServiceType);
                        }
                    }
                    else
                    {
                        var id = repository.All().Count();
                        repository.Create(new ServiceType() {Id=++id, Name = serviceType.Name, CreateDate = DateTime.Now });
                    }
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch
            {
                return Json(Utilities.Failiure, JsonRequestBehavior.AllowGet);
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }
        // GET: TypesOfService/Delete/5
        public JsonResult Delete(int id)
        {
            using (var repository = new Repository<ServiceType>())
            {
                var deleteServiceType = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteServiceType != null && !string.IsNullOrEmpty(deleteServiceType.Name))
                {
                    repository.Delete(deleteServiceType);
                }
                repository.SaveChanges();
            }
            return Json(Utilities.Success, JsonRequestBehavior.AllowGet);
        }
    }
}
