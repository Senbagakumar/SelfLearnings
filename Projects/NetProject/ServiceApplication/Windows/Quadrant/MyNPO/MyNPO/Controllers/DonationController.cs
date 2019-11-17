using MyNPO.DataAccess;
using MyNPO.Models;
using MyNPO.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class DonationController : BaseController
    {

        // GET: Donation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donation/Create
        [HttpPost]
        public ActionResult Create(MyNPO.Models.Donation donation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guid = Guid.NewGuid();
                    var dt = DateTime.Now;
                    EntityContext entityContext = new EntityContext();
                    
                    // Generate the transaction id for cash transactions
                    var existingTransactionsPerDay = entityContext.reportInfo.Where(x => x.Date.Day == DateTime.Today.Day).ToList().Count;
                    var prefixCount = existingTransactionsPerDay <= 0 ? 1 : existingTransactionsPerDay + 1;

                    var transactionId = DateTime.Now.Date.ToString("yyyyMMdd") + "-" + prefixCount;

                    var report = new Report()
                    {
                        Name = donation.Name,
                        TransactionID = transactionId,
                        FromEmailAddress = donation.Email,
                        CurrencyType = donation.DonationType,
                        Net = $"${donation.DonationAmount}",
                        PhoneNo = donation.Phone,
                        Date = dt,
                        Time = dt.ToString(Constants.HourFormat),
                        Description = $"SystemDonation",
                        Reason = donation.Reason,
                        TransactionGuid = guid,
                        ReferenceTxnID = guid.ToString().Replace("-", ""),
                        UploadDateTime = dt,
                        TypeOfReport = Constants.SystemDonation

                    };

                    entityContext.reportInfo.Add(report);
                    entityContext.SaveChanges();
                    // TODO: Add insert logic here
                    ModelState.Clear();
                    ViewBag.Status = "Successfully Saved";

                    // Generated PDF Receipt and Send email attachment.
                    ReceiptGenerator.GenerateDonationReceiptPdf(donation, report.TransactionID);

                    return View();
                }
                else
                {
                    return View(donation);
                }
            }
            catch
            {
                return View();
            }
        }

        
    }
}
