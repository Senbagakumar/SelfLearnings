using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class RevenueController : Controller
    {
        // GET: Revenue
        public ActionResult Index()
        {
            var lmodel = new List<Revenue>();

            using (var repository = new Repository<Revenue>())
            {
                lmodel = repository.All().ToList();
            }
            return View(lmodel);
        }

        // POST: Revenue/Create
        [HttpPost]
        public ActionResult Create(Revenue revenue)
        {
            try
            {
                using (var repository = new Repository<Revenue>())
                {
                    if (revenue.Id != 0)
                    {
                        var updateRevenue = repository.Filter(q => q.Id == revenue.Id).FirstOrDefault();
                        if (updateRevenue != null && !string.IsNullOrEmpty(updateRevenue.Name))
                        {
                            updateRevenue.Name = revenue.Name;
                            updateRevenue.UpdateDate = DateTime.Now;
                            repository.Update(updateRevenue);
                        }
                    }
                    else
                    {
                        int? intIdt = repository.All().Max(u => (int?)u.Id);
                        if (intIdt == null || intIdt == 0)
                            intIdt = 1;
                        else
                            intIdt = ++intIdt;

                        repository.Create(new Revenue() { Id=intIdt.Value, Name = revenue.Name, CreateDate = DateTime.Now });
                    }
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch
            {
                return Json("Failiure", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

       // GET: Revenue/Delete/5
        public JsonResult Delete(int id)
        {

            using (var repository = new Repository<Revenue>())
            {
                var deleteRevenue = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteRevenue != null && !string.IsNullOrEmpty(deleteRevenue.Name))
                {
                    repository.Delete(deleteRevenue);
                }
                repository.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
