using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace _5KProject.Controllers
{
    public class Model
    {
        public string Fname { get; set; }
        public string Email { get; set; }
        public string PayPal { get; set; }
        public string Mobile { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentFee { get; set; }
        public string PaymentGross { get; set; }
        public string PaymentStatus { get; set; }
        public string Quantity { get; set; }
        public string ItemName { get; set; }
        public string ItemNo { get; set; }

    }

    public class Response
    {
        public string first_name { get; set; }
        public string item_name { get; set; }
        public string last_name { get; set; }
        public string payer_email { get; set; }
        public string payer_status { get; set; }

        public string receiver_id { get; set; }
        public string txn_id { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetPrices(string promoCode)
        {
            var prices = new List<SelectListItem>();
            if (promoCode.Equals("COMMUNITY10",StringComparison.OrdinalIgnoreCase))
            {
                prices.Add(new SelectListItem() { Value = "1 Adult", Text = "1 Adult $18.00 USD" });
                prices.Add(new SelectListItem() { Value = "1 Kid", Text = "1 Kid $10.80 USD" });
                prices.Add(new SelectListItem() { Value = "1 Adult + 1 Kid", Text = "1 Adult + 1 Kid $28.80 USD" });
                prices.Add(new SelectListItem() { Value = "2 Adults + 1 Kid", Text = "2 Adults + 1 Kid $46.80 USD" });
                prices.Add(new SelectListItem() { Value = "2 Adults + 2 Kids", Text = "2 Adults + 2 Kids $57.60 USD" });
                prices.Add(new SelectListItem() { Value = "2 Adults", Text = "2 Adults $36.00 USD" });
                prices.Add(new SelectListItem() { Value = "3 Adults", Text = "3 Adults $54.00 USD" });
            }
            return Json(prices, JsonRequestBehavior.AllowGet);
        }

        private string  GetSqlString()
        {
            string sqlConnectionString = ConfigurationManager.AppSettings["DbConnectionString"];
            string[] sqlComponents = sqlConnectionString.Split(';');
            string passwordSection = sqlComponents[3];
            string passwords = passwordSection.Trim().Replace("Password=", " ");
            string decrypt = Decrypt(passwords.Trim());
            return $"{sqlComponents[0]};{sqlComponents[1]};{sqlComponents[2]};Password={decrypt}";
        }

        private void SaveRecord(Model person)
        {
            try
            {
                string insertQuery = "Insert into [5kRun] (pname,pemail,pinvoiceno,pitemno,pitemname,ppaymentstatus,pquantity,ppaymentdate,ppaymentfee,ppaymentgross) values (@0,@1,@2,@3,@4,@5,@6,@7,@8,@9)";
                using (SqlConnection sc = new SqlConnection(GetSqlString()))
                {
                    SqlCommand sd = new SqlCommand(insertQuery, sc);
                    sd.Parameters.AddWithValue("@0", person.Fname);
                    sd.Parameters.AddWithValue("@1", person.Email);
                    sd.Parameters.AddWithValue("@2", person.InvoiceNo);
                    sd.Parameters.AddWithValue("@3", person.ItemNo);
                    sd.Parameters.AddWithValue("@4", person.ItemName);
                    sd.Parameters.AddWithValue("@5", person.PaymentStatus);
                    sd.Parameters.AddWithValue("@6", person.Quantity);
                    sd.Parameters.AddWithValue("@7", person.PaymentDate);
                    sd.Parameters.AddWithValue("@8", person.PaymentFee);
                    sd.Parameters.AddWithValue("@9", person.PaymentGross);
                    sc.Open();
                    sd.ExecuteNonQuery();
                    sc.Close();
                }
            }
            catch (Exception ex)
            {
                var exInfo = new
                {
                    userinfo = $"{person.Fname},{person.Email},{person.InvoiceNo}",
                    message = ex.Message,
                    innerException = ex.InnerException,
                    stackTrace = ex.StackTrace
                };
                var filePath = Server.MapPath(@"\Data\OutPut.txt");
                System.IO.File.AppendAllText(filePath, "\n" + exInfo.ToString() +","+ DateTime.UtcNow.ToString());
            }
        }

        [HttpPost]
        public ActionResult Success()
        {
            var model = new Model();
            try
            {
                Stream req = Request.InputStream;
                req.Seek(0, SeekOrigin.Begin);
                string json = new StreamReader(req).ReadToEnd();
                json = Uri.UnescapeDataString(json);
                var values = HttpUtility.ParseQueryString(json);

                model.Fname = values["first_name"] + " " + values["last_name"];
                model.InvoiceNo = values["txn_id"];
                model.Email = values["payer_email"];
                model.ItemNo = values["item_number"];
                model.ItemName = values["item_name"];
                model.PaymentStatus = values["payment_status"];
                model.Quantity = values["quantity"];
                model.PaymentDate = values["payment_date"];
                model.PaymentFee = values["payment_fee"];
                model.PaymentGross = values["payment_gross"];

                if (string.IsNullOrEmpty(model.ItemNo))
                    model.ItemNo = "01";
            }
            catch (Exception ex)
            {
                var exInfo = new
                {
                    message = ex.Message,
                    innerException = ex.InnerException,
                    stackTrace = ex.StackTrace
                };
                var filePath = Server.MapPath(@"\Data\OutPut.txt");
                System.IO.File.AppendAllText(filePath, "\n" + DateTime.UtcNow.ToString() + "," + exInfo.ToString());
            }

            SaveRecord(model);
            SendMail(model.Email, model.Fname, model.InvoiceNo);

            ViewBag.Name = model.Fname;
            ViewBag.InvocieNo = model.InvoiceNo;

            if (model.ItemName.Trim().ToLower().Contains("5kregistration"))
                return View();
            else
                return Redirect("http://SaibabaSeattle.com/");

            //if (model.ItemName != "5KRegistration")
            //    return Redirect("http://SaibabaSeattle.com/");
            //else
            //    return View();
        }

        private void SendMail(string userEmail,string userName,string userInvoiceNo)
        {
            try
            {
                var senderID = ConfigurationManager.AppSettings["SenderEmailAddress"].ToString();
                var senderPassword = Decrypt(ConfigurationManager.AppSettings["SenderEmailPassword"].ToString());
                var hostName = ConfigurationManager.AppSettings["SmtpHostName"].ToString();

                var faceBookImage = Server.MapPath(@"\img\facebookshare.jpg");
                StringBuilder emailBody = new StringBuilder();
                string nextLine = "<br />";
                // Email Body
                emailBody.AppendFormat("Dear {0},", userName);
                emailBody.AppendLine();
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} Congratulations! You are now registered for the 1st Annual 5k Saibaba Seattle Run.", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} Please check our <a href='http://www.saibaba5krun.org/'>Event's website</a> for updates", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} Refer you friends", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} <a href='https://www.facebook.com/events/206694916880368/'><img src='https://www.mail-signatures.com/articles/wp-content/themes/emailsignatures/images/facebook-35x35.gif'></a>", nextLine, nextLine);
                emailBody.AppendFormat("{0}{1} SaiBabaSeattle", nextLine, nextLine);
                emailBody.AppendFormat("{0} Email : {1}", nextLine, "saibabaseattle@hotmail.com");
                emailBody.AppendFormat("{0} Contact No : {1}", nextLine, "(425)-999-6350");

                emailBody.AppendFormat("{0}{1} Registration Details:", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} InvoiceNo: {1}", nextLine, userInvoiceNo);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} Participant name: {1}", nextLine, userName);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} Date: Saturday, June 22nd, 2019 ", nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} Time: 8AM to 2PM", nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} Location: Marymoor Park 6046 West Lake Sammamish Pkwy, NE Redmond, WA 98082", nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0} <a href='https://www.google.com/maps/place/6046+West+Lake+Sammamish+Pkwy+NE,+Redmond,+WA+98052/@47.6603812,-122.1217066,17z/data=!3m1!4b1!4m5!3m4!1s0x549072a3f34cae2f:0x3e7d94db622d9769!8m2!3d47.6603776!4d-122.1195179'>Google Map</a>", nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} Thank You !", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("{0}{1} Saibaba Seattle", nextLine, nextLine);

                string body = emailBody.ToString();

                MailMessage msg = new MailMessage();
                msg.Body = body;
                msg.Subject = "Registration confirmation for Saibaba Seattle 1st Annual 5K Run";
                msg.From = new MailAddress("saibabaseattle@hotmail.com", "SaiBaba Seattle");
                msg.To.Add(new MailAddress(userEmail, userName));
                msg.Bcc.Add(new MailAddress("saibabaseattle@hotmail.com", "5kRunAdmin"));

                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(hostName, 587);
                smtp.Credentials = new NetworkCredential(senderID, senderPassword);
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                var exInfo = new
                {
                    userinfo = $"{userName},{userEmail},{userInvoiceNo}",
                    message = ex.Message,
                    innerException = ex.InnerException,
                    stackTrace = ex.StackTrace
                };
                var filePath = Server.MapPath(@"\Data\OutPut.txt");
                System.IO.File.AppendAllText(filePath, "\n" + exInfo.ToString() + "," + DateTime.UtcNow.ToString());
            }
        }

        private string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "Quadrant2019Sai";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}