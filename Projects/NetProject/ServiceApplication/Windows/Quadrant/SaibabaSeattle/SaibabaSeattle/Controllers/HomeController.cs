using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LumenWorks.Framework.IO.Csv;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace saibabaseattle.Controllers
{
    public class HomeController : Controller

    {



        #region index
        public ActionResult Index()
        {

            ViewBag.CurrentView = "Index";
            string path = Server.MapPath("~/Images/");

            IEnumerable<string> GetImage =
            Directory.GetFiles(path).Select(Path.GetFileName);

            ViewBag.Getimage = GetImage;
            ViewBag.abc = "Raja";
            return View();

        }
        #endregion

        #region Events
        public ActionResult events()
        {
            ViewBag.abc = "Raja";
            var path = Path.Combine(Server.MapPath("~/Excel/"), "events.csv");
            Stream stream = new FileStream(path, FileMode.Open);

            //Stream stream = upload.InputStream;
            DataTable csvTable = new DataTable();
            using (CsvReader csvReader =
                new CsvReader(new StreamReader(stream), true))
            {
                csvTable.Load(csvReader);
            }
            return View(csvTable);
        }
        #endregion

        #region Saiseattle views empty
        public ActionResult padukaSeva()
        {
            return View();
        }

        public ActionResult saiTemple()
        {
            return View();
        }

        public ActionResult aboutUs()
        {
            return View();
        }

        public ActionResult babaAnswers()
        {
            return View();
        }

        public ActionResult saiSaying()
        {
            return View();
        }
        public ActionResult templeAddress()
        {
            return View();
        }
        public ActionResult mainDonate()
        {
            return View();
        }



        #endregion

        #region feedback
        public ActionResult feedback()
        {
            return View();
        }
        [HttpPost]
        public ActionResult feedback(string ffname, string fmobile, string femail, string ffeedback)
        {
            //if (this.IsCaptchaValid("Validate your captcha"))
            //{
            string userBody = "Thanks For your Interest. We will contact you soon " + "<br><br>" + "Thanks<br>" + "Saibaba Seattle";
            string body = "<p style='font-size:20px'>Hi</p><br>" + "<b>" + "<p>" + ffname + "</p>" + " </b>" + " is Feedback given" + "The details are<br><br>" + "<b>Email</b>\t:\t" + femail
                          + "<br>" + "<b>Phone Number</b>\t:\t" + fmobile + "<br><b>Description</b>\t:\t" + ffeedback;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("seattlesaibaba@gmail.com");
            msg.To.Add("seattlesaibaba@gmail.com");
            msg.Subject = "Saibaba Seattle";
            msg.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential Network = new System.Net.NetworkCredential();
            Network.UserName = "seattlesaibaba@gmail.com";
            Network.Password = "Saibaba3";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            msg.IsBodyHtml = true;
            smtp.Send(msg);

            MailMessage umsg = new MailMessage();
            umsg.From = new MailAddress("seattlesaibaba@gmail.com");
            umsg.To.Add(femail);
            umsg.Subject = "Saibaba Seattle";
            umsg.Body = userBody;
            SmtpClient usmtp = new SmtpClient();
            usmtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential UNetwork = new System.Net.NetworkCredential();
            UNetwork.UserName = "seattlesaibaba@gmail.com";
            UNetwork.Password = "Saibaba3";
            usmtp.UseDefaultCredentials = true;
            usmtp.Credentials = UNetwork;
            usmtp.Port = 587;
            usmtp.EnableSsl = true;
            umsg.IsBodyHtml = true;
            usmtp.Send(umsg);
            ViewBag.message = "Feedback sent successfully..!";
            return View();
        }
        #endregion

        #region contactUs
        public ActionResult contactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contactUs(string fname, string mobile, string email, string message)
        {
            //if (this.IsCaptchaValid("Validate your captcha"))
            //{
            string userBody = "Thanks For your Interest. We will contact you soon " + "<br><br>" + "Thanks<br>" + "Saibaba Seattle";
            string body = "<p style='font-size:20px'>Hi</p><br>" + "<b>" + "<p>" + fname + "</p>" + " </b>" + " is Contacting us" + "The details are<br><br>" + "<b>Email</b>\t:\t" + email
                          + "<br>" + "<b>Phone Number</b>\t:\t" + mobile + "<br><b>Description</b>\t:\t" + message;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("seattlesaibaba@gmail.com");
            msg.To.Add("seattlesaibaba@gmail.com");
            msg.Subject = "Saibaba Seattle";
            msg.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential Network = new System.Net.NetworkCredential();
            Network.UserName = "seattlesaibaba@gmail.com";
            Network.Password = "Saibaba3";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            msg.IsBodyHtml = true;
            smtp.Send(msg);

            MailMessage umsg = new MailMessage();
            umsg.From = new MailAddress("seattlesaibaba@gmail.com");
            umsg.To.Add(email);
            umsg.Subject = "Saibaba Seattle";
            umsg.Body = userBody;
            SmtpClient usmtp = new SmtpClient();
            usmtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential UNetwork = new System.Net.NetworkCredential();
            UNetwork.UserName = "seattlesaibaba@gmail.com";
            UNetwork.Password = "Saibaba3";
            usmtp.UseDefaultCredentials = true;
            usmtp.Credentials = UNetwork;
            usmtp.Port = 587;
            usmtp.EnableSsl = true;
            umsg.IsBodyHtml = true;
            usmtp.Send(umsg);
            ViewBag.message = "Email sent successfully..!";
            return View();
        }
        #endregion

        #region VigrahaPratishta
        public ActionResult Prathistapana()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Prathistapana(string icname, string icgothram, string icemail)
        {
            //if (this.IsCaptchaValid("Validate your captcha"))
            //{
            string userBody = "Thanks For your Interest. We will contact you soon " + "<br><br>" + "Thanks<br>" + "Saibaba Seattle";
            string body = "<p>Hi</p><br>" + "<b>" + "<p>" + icname + "</p>" + " </b>" + " is Sent Gothram" + "The details are<br><br>" + "<b>Gothram</b>\t:\t" + icgothram
                          + "<br>" + "<b>Email</b>\t:\t" + icemail + "<br><b>Description</b>\t:\t";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("seattlesaibaba@gmail.com");
            msg.To.Add("seattlesaibaba@gmail.com");
            msg.Subject = "Saibaba Seattle";
            msg.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential Network = new System.Net.NetworkCredential();
            Network.UserName = "seattlesaibaba@gmail.com";
            Network.Password = "Saibaba3";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            msg.IsBodyHtml = true;
            smtp.Send(msg);

            MailMessage umsg = new MailMessage();
            umsg.From = new MailAddress("seattlesaibaba@gmail.com");
            umsg.To.Add(icemail);
            umsg.Subject = "Saibaba Seattle";
            umsg.Body = userBody;
            SmtpClient usmtp = new SmtpClient();
            usmtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential UNetwork = new System.Net.NetworkCredential();
            UNetwork.UserName = "seattlesaibaba@gmail.com";
            UNetwork.Password = "Saibaba3";
            usmtp.UseDefaultCredentials = true;
            usmtp.Credentials = UNetwork;
            usmtp.Port = 587;
            usmtp.EnableSsl = true;
            umsg.IsBodyHtml = true;
            usmtp.Send(umsg);
            ViewBag.message = "Your details sent successfully..!";
            return View();
        }


        #endregion

        #region orderded lists
        public ActionResult ganGanGanatBote()
        {
            return View();
        }
        public ActionResult saiElevenPromises()
        {
            return View();
        }
        public ActionResult templeHours()
        {
            return View();
        }
        public ActionResult books()
        {
            return View();
        }
        public ActionResult pictureGallery()
        {
            return View();
        }
        public ActionResult videoGallery()
        {
            return View();
        }
        public ActionResult samadhi()
        {
            return View();
        }
        #endregion

        #region Priest Services
        public ActionResult priestServices()
        {
            var path = Path.Combine(Server.MapPath("~/Excel/"), "priest-services.csv");
            Stream stream = new FileStream(path, FileMode.Open);


            //Stream stream = upload.InputStream;
            DataTable csvTable = new DataTable();
            using (CsvReader csvReader =
                new CsvReader(new StreamReader(stream), true))
            {
                csvTable.Load(csvReader);
            }
            return View(csvTable);
        }

        #endregion

        #region UpdatedNews
        [HttpPost]
        public JsonResult updateNews()
        {
            var path = Path.Combine(Server.MapPath("~/Excel/"), "update-news.csv");
            Stream stream = new FileStream(path, FileMode.Open);

            //Stream stream = upload.InputStream;
            DataTable csvTable = new DataTable();
            using (CsvReader csvReader =
                new CsvReader(new StreamReader(stream), true))
            {
                csvTable.Load(csvReader);
            }
            //return View(csvTable);
            ViewBag.csvTable = csvTable;

            //int? b = csvTable.Columns.Count;

            int c = csvTable.Rows.Count;

            string[] b = new string[c];

            for(int i = 0; i < c; i++)
            {
                b[i] = csvTable.Rows[i].Field<string>("Event");
            }


            return Json(b,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Admin Login

        public ActionResult AdminLogin()
        {
            Session["User"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult AdminLoginForm(string userName, string password)
        {
            if (userName == System.Configuration.ConfigurationManager.AppSettings["UserName"] && password == System.Configuration.ConfigurationManager.AppSettings["Password"])
            {
                Session["User"] = userName;
                return RedirectToAction("parentAdmin");
            }
            TempData["msg"] = "Please verify your credentials";
            return RedirectToAction("AdminLogin");

        }
        #endregion

        #region Parent Admin
        public ActionResult parentAdmin()
        {
            if (Session["User"] == null)
            return RedirectToAction("AdminLogin");
            return View();
        }
        #endregion

        #region AdminPage Sign out
        public ActionResult adminSignOut()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("AdminLogin", "Home");
        }
        #endregion

        #region Admin Console For Excel for update News

        public ActionResult adminConsoleForUpdates()
        {
            if (Session["User"] == null)
            return RedirectToAction("AdminLogin");
            return View();
        }
        [HttpPost]
        public ActionResult adminConsoleForUpdates(HttpPostedFileBase excel)
        {
            if (ModelState.IsValid)
            {

                if (excel != null && excel.ContentLength > 0)
                {

                    if (excel.FileName.EndsWith(".csv"))
                    {

                        var fileName = Path.GetFileName(excel.FileName);
                        // store the file inside ~/App_Data/uploads folder

                        var path = Path.Combine(Server.MapPath("~/Excel/"), fileName);
                        excel.SaveAs(path);

                        Stream stream = new FileStream(path, FileMode.Open);

                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        #endregion

        #region Admin Console For Excel

        public ActionResult adminConsoleForExcel()
        {
            if (Session["User"] == null)
            return RedirectToAction("AdminLogin");
            return View();
        }
        [HttpPost]
        public ActionResult adminConsoleForExcel(HttpPostedFileBase excel)
        {
            if (ModelState.IsValid)
            {

                if (excel != null && excel.ContentLength > 0)
                {

                    if (excel.FileName.EndsWith(".csv"))
                    {

                        var fileName = Path.GetFileName(excel.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine(Server.MapPath("~/Excel/"), fileName);
                        excel.SaveAs(path);

                        Stream stream = new FileStream(path, FileMode.Open);

                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
        #endregion

        #region Admin Console For Images


        public ActionResult adminConsoleForImages()
        {
            if (Session["User"] == null)
                return RedirectToAction("AdminLogin");

            string path = Server.MapPath("~/Images/");

            IEnumerable<string> GetImage =
            Directory.GetFiles(path).Select(Path.GetFileName);

            ViewBag.Getimage = GetImage;
            return View();
        }
        [HttpGet]
        public JsonResult DeleteImage(string imagepath)
        {
            string[] stldelImage = imagepath.Split('/');
            imagepath = stldelImage[stldelImage.Length - 1];

            string path = Server.MapPath("~/Images/");
            string fullpath = path + Path.GetFileName(imagepath);
            System.IO.File.Delete(fullpath);
            bool Available = true;
            return Json(Available, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult adminConsoleForImages(HttpPostedFileBase file)
        {

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var paths = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(paths);
            }
            string path = Server.MapPath("~/Images/");
            IEnumerable<string> GetImage =
            Directory.GetFiles(path).Select(Path.GetFileName);

            ViewBag.Getimage = GetImage;
            //Deleting uploaded images from the local folder for better use
            // System.IO.File.Delete(fullpath);

            return View();
        }
        #endregion

        #region Priest Services Application
        public ActionResult PriestServicesApplication()
        {
            if (Session["PSAUser"] == null)
                return RedirectToAction("PstApplicationLogin");
            return View();
        }
        [HttpPost]
        public ActionResult PriestServicesApplication(string psname, string pspnum, string psemail, float psmount, string psadress, string psdetails,  string psedate, string psetime, string pspayment, string psadscmts)
        {
            //if (this.IsCaptchaValid("Validate your captcha"))
            //{
            string userBody = "Thanks For your Interest. We will contact you soon " + "<br><br>" + "Thanks<br>" + "Saibaba Seattle";
            string body = "<p>Hi</p><br>" +
                          "<p> Name:" + psname + "</p>" + "</br>" + "<p> Phone Number:" + pspnum + "</p>" + "</br>" + "<p> Email:" + psemail + "</p>" + "</br>" +
                           "<p> Amount:" + psmount + "</p>" + "</br>" + "<p> Address:" + psadress + "</p>" + "</br>" + "<p> Event Details:" + psdetails + "</p>" +
                           "</br>" +  "</p>" + "</br>" + "<p> Event Date :" + psedate + "</p>" + "</br>" + "<p> Event Time:" + psetime + "</p>" + "</br>" + "<p>Payment Ref Number / Status of Payment:" + pspayment + "</p>" + "</br>" + "<p>Additional Comments:" + psadscmts + "</p>";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("seattlesaibaba@gmail.com");
            msg.To.Add("seattlesaibaba@gmail.com");
            msg.Subject = "Saibaba Seattle";
            msg.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential Network = new System.Net.NetworkCredential();
            Network.UserName = "seattlesaibaba@gmail.com";
            Network.Password = "Saibaba3";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = Network;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            msg.IsBodyHtml = true;
            smtp.Send(msg);

            MailMessage umsg = new MailMessage();
            umsg.From = new MailAddress("seattlesaibaba@gmail.com");
            umsg.To.Add(psemail);
            umsg.Subject = "Saibaba Seattle";
            umsg.Body = userBody;
            SmtpClient usmtp = new SmtpClient();
            usmtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential UNetwork = new System.Net.NetworkCredential();
            UNetwork.UserName = "seattlesaibaba@gmail.com";
            UNetwork.Password = "Saibaba3";
            usmtp.UseDefaultCredentials = true;
            usmtp.Credentials = UNetwork;
            usmtp.Port = 587;
            usmtp.EnableSsl = true;
            umsg.IsBodyHtml = true;
            usmtp.Send(umsg);
            ViewBag.message = "Your details sent successfully..!";
            return View();
        }
        #endregion

        #region Priest Services Application Login
        public ActionResult PstApplicationLogin()
        {
            Session["PSAUser"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult PstApplicationLoginForm(string PsaUserName, string PsaPassword)
        {
            if (PsaUserName == System.Configuration.ConfigurationManager.AppSettings["PSAUserName"] && PsaPassword == System.Configuration.ConfigurationManager.AppSettings["PSAPassword"])
            {
                Session["PSAUser"] = PsaUserName;
                return RedirectToAction("PriestServicesApplication");
            }
            TempData["msg"] = "Please verify your credentials";
            return RedirectToAction("PstApplicationLogin");
        }
        public ActionResult LogOut()
        {           
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("PstApplicationLogin", "Home");
        }
        #endregion

        #region BOT
        public PartialViewResult BOT()
        {
            DirectLineToken directLine = null;
            if (Session["botToken"] == null)
            {
                Task.Run(async () => { directLine = await GenerateToken(); }).GetAwaiter().GetResult();
                Session["botToken"] = directLine;
                ViewBag.firstTime = 1;
            }
            else
            {
                directLine = (DirectLineToken)Session["botToken"];
                ViewBag.firstTime = 0;
            }
            ViewBag.token = directLine.token;
            ViewBag.cid = directLine.conversationId;
            ViewBag.userId = $"dl_{Guid.NewGuid()}";
            return PartialView("BOT");
        }

        public class DirectLineToken
        {
            public string conversationId { get; set; }
            public string token { get; set; }
            public int expires_in { get; set; }
        }
        public async Task<DirectLineToken> GenerateToken()
        {
            var directLine = new DirectLineToken();
            try
            {
                var secret = "cL5bN3nBfXE.7TTxeTXRyTxWV4vmcs3sNqjABBe_9u-_moH8NI-8K50";
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,$"https://directline.botframework.com/v3/directline/tokens/generate");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", secret);
                var userId = $"dl_{Guid.NewGuid()}";
                request.Content = new StringContent(
                JsonConvert.SerializeObject(
                    new { User = new { Id = userId } }),
                    Encoding.UTF8,
                    "application/json");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    directLine = JsonConvert.DeserializeObject<DirectLineToken>(body);
                }
                //https://directline.botframework.com/v3/directline/tokens/refresh
            }
            catch (Exception ex)
            {
                throw;
            }
            return directLine;
        }
        #endregion
    }
}   