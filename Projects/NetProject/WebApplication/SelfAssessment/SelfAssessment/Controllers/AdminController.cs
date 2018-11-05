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
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerWiseReport()
        {
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };
            var subSector = new List<SelectListItem>();
            var sector = new List<SelectListItem>();
            var states = new List<SelectListItem>();
            var cities = new List<SelectListItem>();
            var typeOfService = new List<SelectListItem>();
            var revenue = new List<SelectListItem>();

            var assessMent = new List<SelectListItem>();

            using (var repo = new Repository<Assessment>())
            {
                assessMent = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                //subSector = repo.AssessmentContext.subSectors.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.SubSectorName }).ToList();
                sector = BaseHelper.GetSectorValues();
                states = repo.AssessmentContext.states.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.StateName }).ToList();
                revenue = repo.AssessmentContext.revenues.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                typeOfService = repo.AssessmentContext.serviceTypes.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                cities = repo.AssessmentContext.cities.Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.CityName }).ToList();
            }

            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = Utilities.OthersValue };

            //sector.Insert(0, firstItem);
            //subSector.Insert(0, firstItem);
            revenue.Insert(0, firstItem);
            typeOfService.Insert(0, firstItem);
            cities.Insert(0, firstItem);
            states.Insert(0, firstItem);
            assessMent.Insert(0, firstItem);

            //sector.Add(lastItem);
            //subSector.Add(lastItem);

            ViewBag.AssessMent = assessMent;
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = subSector;
            ViewBag.City = cities;
            ViewBag.State = states;
            ViewBag.Revenue = revenue;
            ViewBag.TypeOfService = typeOfService;

            return View();
        }

        public ActionResult SectorWiseReport()
        {
            var sector = new List<SelectListItem>();
            var assessMent = new List<SelectListItem>();
            var subSector = new List<SelectListItem>();

            var lastItem = new SelectListItem() { Text = Utilities.Others, Value = Utilities.OthersValue };
            var firstItem = new SelectListItem() { Text = Utilities.DefaultSelectionValue, Value = "0", Selected = true };

            using (var repo = new Repository<Assessment>())
            {
                assessMent = repo.All().Select(q => new SelectListItem() { Value = q.Id.ToString(), Text = q.Name }).ToList();
                sector = BaseHelper.GetSectorValues();
            }

            //sector.Insert(0, firstItem);
            subSector.Insert(0, firstItem);
            assessMent.Insert(0, firstItem);

            sector.Add(lastItem);
            subSector.Add(lastItem);

            ViewBag.AssessMent = assessMent;
            ViewBag.SectorList = sector;
            ViewBag.SubSectorList = subSector;

            return View();
        }
        public ActionResult CustomerDetails(int id,string level)
        {
            var uiOrganization = new UIOrganization();

            using (var org = new Repository<Organization>())
            {
                var selectOrg = org.Filter(q => q.Id == id).FirstOrDefault();

                uiOrganization = org.AssessmentContext.UserInfo.Where(q => q.Id == id).ToList().Join(org.AssessmentContext.cities, og => og.CityId, cy => cy.Id, (og, cy) => new { og, cy }).
                    Join(org.AssessmentContext.states, s => s.og.StateId, st => st.Id, (s, st) => new { s, st, }).Join(org.AssessmentContext.serviceTypes,
                    sty => sty.s.og.TypeOfServiceId, stype => stype.Id, (sty, stype) => new UIOrganization()
                    {
                        Id= id,
                        SelectLevel=level,
                        Name = sty.s.og.Name,
                        Address = sty.s.og.Address,
                        City = sty.s.cy.CityName,
                        Email = sty.s.og.Email,
                        MobileNo = sty.s.og.MobileNo,
                        State = sty.st.StateName,
                        Designation = sty.s.og.Designation,
                        TypeOfService = stype.Name,
                        Sector = org.AssessmentContext.sectors.Where(q => q.Id == selectOrg.SubSectorId).FirstOrDefault().SectorName,
                        SubSector = org.AssessmentContext.subSectors.Where(q => q.Id == selectOrg.SubSectorId).FirstOrDefault().SubSectorName,
                        Revenue = org.AssessmentContext.revenues.Where(q=>q.Id == selectOrg.RevenueId).FirstOrDefault().Name,
                        Type = selectOrg.TypeId == 1 ? Utilities.Small : selectOrg.TypeId == 2 ? Utilities.Large : Utilities.OperatingUnit

                    }).FirstOrDefault();
            }
            return View(uiOrganization);
        }
        public ActionResult GetFirstGroup(int id,string level)
        {
            return PartialView("~/Views/ManageUser/QuizGroupPartial.cshtml", GetGroupQuiz(id,level));
        }
        private GroupQuiz GetGroupQuiz(int id,string level)
        {
            var groupQuizManager = new GroupQuizManager(id);
            var listOfGroup = groupQuizManager.GetAllQuestions(level);
            groupQuizManager.AllQuestions = listOfGroup;
            var groupQuiz= groupQuizManager.LoadQuiz(1);
            groupQuiz.UserId = id;
            return groupQuiz;
        }

        [HttpPost]
        public ActionResult CustomerAssessmentReport(QuestionOnePost question)
        {
            var groupQuizManager = new GroupQuizManager(question.UserId);
            var listOfGroup = groupQuizManager.GetAllQuestions(question.Level);
            groupQuizManager.AllQuestions = listOfGroup;

            GroupQuiz groupQuiz = null;
            int groupId = int.Parse(question.QInfo);
            if (question.hdnaction == "Previous")
            {
                groupId = groupId - 1;
                if (groupId <= 0)
                    groupId = 1;
               
            }
            else
                groupId = groupId + 1;

            if (groupQuizManager.MoveToNextGroup(groupId))
            {
                groupQuiz = groupQuizManager.LoadQuiz(groupId);
            }           
            groupQuiz.UserId = question.UserId;
            return PartialView("~/Views/ManageUser/QuizGroupPartial.cshtml", groupQuiz);
        }
    }
}
