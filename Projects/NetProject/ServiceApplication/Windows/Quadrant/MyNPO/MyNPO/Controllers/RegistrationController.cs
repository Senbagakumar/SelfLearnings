using MyNPO.DataAccess;
using MyNPO.Models;
using MyNPO.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class RegistrationController : BaseController
    {
       // static string connectionString = ConfigurationManager.AppSettings["DbConnectionString"];
        EntityContext entityContext = new EntityContext();
        // GET: Registration
        //public ActionResult Index()
        //{

        //    var it = entityContext.familyInfos.ToList();
        //    it.ForEach(q => q.DependentDetails = entityContext.dependentInfos.Where(t => t.PrimaryId == q.PrimaryId).ToList());
        //    return View(it);
        //}

        // GET: Registration/Details/5
        public ActionResult Details(Guid id)
        {
            var it = entityContext.familyInfos.FirstOrDefault(q => q.PrimaryId == id);
            it.DependentDetails = entityContext.dependentInfos.Where(q => q.PrimaryId == it.PrimaryId).ToList();

            return View(it);
        }

        public ActionResult Report()
        {
            var familyInfo = new FamilyReportInfo();
            familyInfo.ReportInfo = new List<FamilyInfo>();
            return View(familyInfo);
        }


        [HttpPost]
        public ActionResult Report(FamilyReportInfo familyReportInfo)
        {
            var entityContext = new EntityContext();
            var fromDate = Convert.ToDateTime(familyReportInfo.FromDate);
            var toDate = Convert.ToDateTime(familyReportInfo.ToDate);
            var reports = new List<FamilyInfo>();
            if(fromDate != DateTime.MinValue || toDate != DateTime.MinValue)
            {
               reports = entityContext.familyInfos.Where(q => EntityFunctions.TruncateTime(q.CreateDate) >= fromDate && EntityFunctions.TruncateTime(q.CreateDate) <= toDate).ToList();
            }
            familyReportInfo.ReportInfo = reports;
            return View(familyReportInfo);
        }
            // GET: Registration/Create
        public ActionResult Create()
        {           
            //ViewBag.VolunteerInfo = "PrimaryInfo";
            return View();
        }

        [HttpPost]
        public JsonResult FirstNameSearch(string keyword)
        {
            var result = entityContext.familyInfos.Where(q => q.FirstName.ToLower().StartsWith(keyword)).Select(q => q).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Next(FamilyInfo family)
        {
            family.DependentDetails = new List<DependentInfo>();

            return View("Create", family);
        }
        // POST: Registration/Create
        [HttpPost]
        public JsonResult Create(FamilyInfo familyInfo)
        {
            string status = "";

            if (familyInfo.MarriageDate != null)
            {
                familyInfo.MaritalStatus = "Married";
            }
            StringBuilder sb = new StringBuilder();
            try
            {              
                // TODO: Add insert logic here
                Guid transactionId = Guid.NewGuid();

                if (familyInfo.MarriageDate == DateTime.MinValue)
                    familyInfo.MarriageDate = null;

                familyInfo.CreateDate = DateTime.Now;
                familyInfo.PrimaryId = transactionId;

                var famInfo = entityContext.familyInfos.FirstOrDefault(q => q.FirstName == familyInfo.FirstName && q.LastName == familyInfo.LastName && q.DateOfBirth == familyInfo.DateOfBirth);
                if (famInfo != null && !string.IsNullOrEmpty(famInfo.FirstName))
                {
                    var donorName = $"{familyInfo.FirstName} + ' ' + {familyInfo.LastName}";
                    status = $"{donorName} is already registered.";
                    if (!string.IsNullOrWhiteSpace(familyInfo.Donation))
                    {
                        var report = AddedTransactions(familyInfo);

                        if (!string.IsNullOrWhiteSpace(familyInfo.Donation) && !string.IsNullOrWhiteSpace(report.Net))
                            entityContext.reportInfo.Add(report);

                        status += $"--Thanks for the Donation of ${familyInfo.Donation}. ";
                        entityContext.SaveChanges();
                        // Generated PDF Receipt and Send email attachment.

                        if (!string.IsNullOrWhiteSpace(familyInfo.Donation) && !string.IsNullOrWhiteSpace(report.TransactionID))
                        {
                            ReceiptGenerator.GenerateDonationReceiptPdf(new Donation() { Name = report.Name, Email = report.FromEmailAddress, DonationAmount = report.Net, DonationType = "Cash", Phone = report.PhoneNo, Reason = report.Reason }, report.TransactionID);
                            status = status + $"we sent the tax recepit to your mentioned mail id";
                        }
                    }
                }
                else
                {
                    familyInfo?.DependentDetails?.ForEach(s => s.PrimaryId = transactionId);
                    entityContext.familyInfos.Add(familyInfo);

                    var report=AddedTransactions(familyInfo);

                    if(!string.IsNullOrWhiteSpace(familyInfo.Donation) && !string.IsNullOrWhiteSpace(report.Net))
                        entityContext.reportInfo.Add(report);

                    entityContext.SaveChanges();
                    status = $"Record Saved.--Thanks for the Donation of ${ familyInfo.Donation}. ";
                    // Generated PDF Receipt and Send email attachment.
                    if (!string.IsNullOrWhiteSpace(familyInfo.Donation) && !string.IsNullOrWhiteSpace(report.TransactionID))
                    {
                        ReceiptGenerator.GenerateDonationReceiptPdf(new Donation() { Name = report.Name, Email = report.FromEmailAddress, DonationAmount = report.Net, DonationType = "Cash", Phone = report.PhoneNo, Reason = report.Reason }, report.TransactionID);
                        status = status + $" We sent the tax recepit to your mentioned mail id";
                    }
                }
                ModelState.Clear();

            }
            catch (DbEntityValidationException e)
            {
               foreach (var eve in e.EntityValidationErrors)
                { 
                    foreach (var ve in eve.ValidationErrors)
                   {
                        sb.AppendFormat(ve.ErrorMessage + ",");
                    }

                }
                return Json(sb.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        private Report AddedTransactions(FamilyInfo familyInfo)
        {
            // Generate the transaction id for cash transactions
            var existingTransactionsPerDay = entityContext.reportInfo.Where(x => x.Date.Day == DateTime.Today.Day).ToList().Count;
            var prefixCount = existingTransactionsPerDay <= 0 ? 1 : existingTransactionsPerDay + 1;

            var transactionId = DateTime.Now.Date.ToString("yyyyMMdd") + "-" + prefixCount;
            var report = new Report();
            if (!string.IsNullOrEmpty(familyInfo.Donation) && !string.IsNullOrWhiteSpace(familyInfo.Donation))
            {
                report = new Report()
                {
                    Name = $"{familyInfo.FirstName} {familyInfo.LastName}",
                    FromEmailAddress = familyInfo.Email,
                    Net = $"${familyInfo.Donation}",
                    PhoneNo = familyInfo.MobileNo,
                    Date = familyInfo.CreateDate,
                    Time = familyInfo.CreateDate.ToString(Constants.HourFormat),
                    Description = $"SystemDonation", // Plan to LoginUser
                    Reason = familyInfo.DonationReason,
                    TransactionGuid = Guid.NewGuid(),
                    ReferenceTxnID = familyInfo.PrimaryId.ToString().Replace("-", ""),
                    TransactionID = transactionId,
                    UploadDateTime = familyInfo.CreateDate,
                    TypeOfReport = Constants.SystemDonation,
                    CurrencyType = "Cash"

                };
            }
            return report;
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(Guid id)
        {
            var it = entityContext.familyInfos.FirstOrDefault(q => q.PrimaryId == id);
            it.DependentDetails = entityContext.dependentInfos.Where(q => q.PrimaryId == it.PrimaryId).ToList();

            return View(it);
        }

        // POST: Registration/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FamilyInfo collection)
        {
            try
            {
                // TODO: Add update logic here

                var familInfo = entityContext.familyInfos.FirstOrDefault(q => q.PrimaryId == id);

                familInfo.DependentDetails = entityContext.dependentInfos.Where(t => t.PrimaryId == collection.PrimaryId).ToList();

                foreach(var dependent in familInfo.DependentDetails)
                {
                    var changeDependent = collection.DependentDetails.FirstOrDefault(q => q.Id == dependent.Id);
                    if(changeDependent!=null)
                    {
                        dependent.DOB = changeDependent.DOB;
                        dependent.Name = changeDependent.Name;
                        dependent.RelationShip = changeDependent.RelationShip;                      
                        
                    }
                }

                familInfo.FirstName = collection.FirstName;
                familInfo.LastName = collection.LastName;
                familInfo.Address = collection.Address;
                familInfo.City = collection.City;
                familInfo.DateOfBirth = collection.DateOfBirth;
                familInfo.Email = collection.Email;
                familInfo.MaritalStatus = collection.MaritalStatus;
                familInfo.MarriageDate = collection.MarriageDate;
                familInfo.MobileNo = collection.MobileNo;
                familInfo.NoOfDependents = collection.NoOfDependents;
                familInfo.ZipCode = collection.ZipCode;
                familInfo.IsVolunteer = collection.IsVolunteer;

                entityContext.Entry(familInfo).State = System.Data.Entity.EntityState.Modified;               
                entityContext.familyInfos.AddOrUpdate(familInfo);

                entityContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(Guid id)
        {
            var it = entityContext.familyInfos.FirstOrDefault(q => q.PrimaryId == id);
            it.DependentDetails = entityContext.dependentInfos.Where(q => q.PrimaryId == it.PrimaryId).ToList();
            return View(it);
        }

        // POST: Registration/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var it = entityContext.familyInfos.FirstOrDefault(q => q.PrimaryId == id);
                it.DependentDetails = entityContext.dependentInfos.Where(q => q.PrimaryId == it.PrimaryId).ToList();
                entityContext.dependentInfos.RemoveRange(it.DependentDetails);
                entityContext.familyInfos.Remove(it);               
                entityContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
