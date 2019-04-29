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
    public class StateController : AdminBaseController
    {
        // GET: State
        public ActionResult Index()
        {
            List<State> lstate = new List<State>();
            var uilstate = new List<UIState>();
            using (Repository<State> repository = new Repository<State>())
            {
                lstate = repository.All().ToList();
            }
            int i = 1;
            foreach(var uis in lstate)
            {
                uilstate.Add(new UIState() { Id = uis.Id, StateName = uis.StateName, CreateDate = uis.CreateDate, OrderId = i });
                i = i + 1;
            }
            return View(uilstate);
        }

        // POST: State/Create
        [HttpPost]
        public JsonResult Create(State state)
        {
            try
            {
                using (Repository<State> repository = new Repository<State>())
                {
                    if (state.Id != 0)
                    {
                        var updateState = repository.Filter(q => q.Id == state.Id).FirstOrDefault();
                        if (updateState != null && !string.IsNullOrEmpty(updateState.StateName))
                        {
                            updateState.StateName = state.StateName;
                            updateState.UpdateDate = DateTime.Now;
                            repository.Update(updateState);
                        }
                    }
                    else
                    {
                        var stateCount = repository.AssessmentContext.states.Count();
                        repository.Create(new State() { StateName = state.StateName, Id=++stateCount, CreateDate = DateTime.Now });
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

        // GET: State/Delete/5
        public JsonResult Delete(int id)
        {
            try
            {
                using (Repository<State> repository = new Repository<State>())
                {
                    var deleteState = repository.Filter(q => q.Id == id).FirstOrDefault();
                    if (deleteState != null && !string.IsNullOrEmpty(deleteState.StateName))
                    {
                        repository.Delete(deleteState);
                    }
                    repository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return Json("Success", JsonRequestBehavior.AllowGet);

        }
    }
}
