using SelfAssessment.Business;
using SelfAssessment.DataAccess;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfAssessment.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailTemplate()
        {
            var listTemplate = new List<Template>();
            //listTemplate.Add(new Template() { Id= 1, Name= "Tiger Nixon", Description= "System Architect" });
            //listTemplate.Add(new Template() { Id = 2, Name = "Garrett Winters", Description = "Accountant" });

            using (var repository = new Repository<Template>())
            {
                listTemplate = repository.All().ToList();
            }

            var uilistTemplate = new List<UITemplate>();
            int i = 1;
            foreach(var template in listTemplate)
            {
                var uiTemplate = new UITemplate();
                uiTemplate.UiId = i;
                uiTemplate.Id = template.Id;
                uiTemplate.Name = template.Name;
                uiTemplate.Description = template.Description.ToString();
                uiTemplate.Description = uiTemplate.Description.Replace("\r\n", " ").Replace("\t"," ");
                uilistTemplate.Add(uiTemplate);
                i++;
            }

            return View(uilistTemplate);
        }
        [HttpPost]
        public JsonResult TemplateSave(UITemplate template)
        {
            try
            {
                using (var repository = new Repository<Template>())
                {
                    if (template.Id != 0)
                    {
                        var uiTemp = repository.Filter(q => q.Id == template.Id).FirstOrDefault();
                        if (uiTemp != null && !string.IsNullOrEmpty(uiTemp.Name))
                        {
                            uiTemp.Name = template.Name;
                            uiTemp.Description = template.Description;
                            uiTemp.Id = template.Id;
                            uiTemp.UpdateDate = DateTime.Now;
                            repository.Update(uiTemp);
                        }
                    }
                    //else
                    //{
                    //    var temp = new Template()
                    //    {
                    //        Name = template.Name,
                    //        CreateDate = DateTime.Now,
                    //        Description = template.Description,
                    //        UpdateDate = DateTime.Now
                    //    };
                    //    repository.Create(temp);
                    //}
                    repository.SaveChanges();
                }

                // TODO: Add insert logic here

                // return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                UserException.LogException(ex);
                return Json("Failiure", JsonRequestBehavior.AllowGet);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        public JsonResult TemplateDelete(int id)
        {
            using (var repository = new Repository<Template>())
            {
                var deleteTemplate = repository.Filter(q => q.Id == id).FirstOrDefault();
                if (deleteTemplate != null && !string.IsNullOrEmpty(deleteTemplate.Name))
                {
                    repository.Delete(deleteTemplate);
                }
                repository.SaveChanges();
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
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
                        Sector = selectOrg.SectorId == 1000 ? Utilities.Others : org.AssessmentContext.sectors.Where(q => q.Id == selectOrg.SubSectorId).FirstOrDefault().SectorName,
                        SubSector = selectOrg.SubSectorId == 1000 ? Utilities.Others :  org.AssessmentContext.subSectors.Where(q => q.Id == selectOrg.SubSectorId).FirstOrDefault().SubSectorName,
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

        public FileResult PdfExport(int userId, string level = null)
        {
            string companyName = string.Empty, address = string.Empty, contactName = string.Empty;
            using (var org = new Repository<Organization>())
            {
                var user = org.Filter(q => q.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    companyName = user.Name;
                    address = user.Address;
                    contactName = user.ContactName;
                }
            }
            Utilities.DeleteOldFiles(Server.MapPath("~/Downloads"));
            var dt = Utilities.GetReport(userId, level);
            var dynamicName = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var fileName = Server.MapPath($"~/Downloads/{dynamicName}.pdf");
            Utilities.CreatePdf(fileName, dt,companyName,address,contactName);
            return File(fileName, "application/pdf", $"{dynamicName}.pdf");

        }

        public FileResult CsvExport(int userId,string level = null)
        {
            string companyName = string.Empty, address = string.Empty, contactName = string.Empty;
            using (var org = new Repository<Organization>())
            {
                var user = org.Filter(q => q.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    companyName = user.Name;
                    address = user.Address;
                    contactName = user.ContactName;
                }
            }

            Utilities.DeleteOldFiles(Server.MapPath("~/Downloads"));
            var dt = Utilities.GetReport(userId, level);
            var dynamicName = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var fileName = Server.MapPath($"~/Downloads/{dynamicName}.csv");
            var s = Utilities.CreateCsv(dt,companyName,address,contactName);
            System.IO.File.AppendAllText(fileName, s);
            return File(fileName, "application/text", $"{dynamicName}.csv");

        }

        [HttpPost]
        public ActionResult CustomerAssessmentReport(QuestionOnePost question)
        {
            //PDF-1-Level 1

            if (question.hdnaction != string.Empty && (question.hdnaction.Contains("PDF") || question.hdnaction.Contains("CSV")))
            {
                var values = question.hdnaction.Split('-');
                if (question.hdnaction.Contains("PDF"))
                {
                    return PdfExport(int.Parse(values[1]),values[2]);
                }
                else
                {
                    return CsvExport(int.Parse(values[1]), values[2]);
                }
            }
            else
            {
                var groupQuizManager = new GroupQuizManager(question.UserId);
                var listOfGroup = groupQuizManager.GetAllQuestions(question.Level);
                groupQuizManager.AllQuestions = listOfGroup;

                GroupQuiz groupQuiz = new GroupQuiz();
                int groupId = int.Parse(question.QInfo);
                if (question.hdnaction == "Previous")
                {
                    groupId = groupId - 1;
                    if (groupId <= 0)
                        groupId = 1;

                }
                else
                {
                    if (listOfGroup.Count > groupId)
                        groupId = groupId + 1;
                }

                if (groupQuizManager.MoveToNextGroup(groupId))
                {
                    groupQuiz = groupQuizManager.LoadQuiz(groupId);
                }
                groupQuiz.UserId = question.UserId;
                return PartialView("~/Views/ManageUser/QuizGroupPartial.cshtml", groupQuiz);
            }
        }
    }
}
