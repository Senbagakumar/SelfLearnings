using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MyNPO.DataAccess;
using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;

namespace MyNPO.Utilities
{
    /// <summary>
    /// This class generates donation receipts in pdf and send them as email attachments to the donors.
    /// </summary>
    public static class ReceiptGenerator
    {
        #region "Static Fields"
        private static string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"].ToString();
        private static string SenderEmailPassword = Helper.Decrypt(ConfigurationManager.AppSettings["SenderEmailPassword"].ToString());
        private static string OrganizationName = ConfigurationManager.AppSettings["OrganizationName"].ToString();
        private static string OrganizationAddress = ConfigurationManager.AppSettings["OrganizationAddress"].ToString();
        private static string City = ConfigurationManager.AppSettings["City"].ToString();
        private static string State = ConfigurationManager.AppSettings["State"].ToString();
        private static string Zip = ConfigurationManager.AppSettings["ZipCode"].ToString();
        private static string OrganizationPhone = ConfigurationManager.AppSettings["OrganizationPhone"].ToString();
        private static string TaxID = ConfigurationManager.AppSettings["TaxID"].ToString();
        private static string Url = ConfigurationManager.AppSettings["Url"].ToString();
        private static string SmtpHostName = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
        private static string ImageUrl = ConfigurationManager.AppSettings["ImageUrl"].ToString();
        //private static HttpResponse Response = HttpContext.Current.Response;
        #endregion

        #region "Constants"
        private const string NonProfitVerbage = @"(A Non-Profit Organization Registered in the State of Washington)";
        private const string EmailSubject = "Thank you for your donation to SaiBaba Seattle.";
        private const string Logo = "http://saibabaseattle.com/pics/saibaba-seattle-logo.png";
        #endregion

        public static void GenerateDonationReceiptPdf(Donation donation, string transactionID)
        {
            var attachmentName = "DonationReceipt_" + donation.Name + "_" + transactionID + ".pdf";
            var dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[3]
            {
                new DataColumn("TransactionID",typeof(string)),
                new DataColumn("DonationId", typeof(string)),
                new DataColumn("DonationAmount", typeof(decimal))
            });
            donation.DonationAmount = donation.DonationAmount.Replace("$", "");
            var drow = dt.NewRow(); drow[0] =transactionID; drow[1] = donation.Reason; drow[2] = $"{donation.DonationAmount}";
            dt.Rows.Add(drow);

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringBuilder sb = new StringBuilder();
                    // Generate Donation Receipt Header and Body.
                    sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                    sb.AppendFormat("<tr><td align='center' style='background-color: #18B5F0' colspan = '6'><b><font size=4>{0}</b></font></td></tr>", "Donation Receipt");
                    sb.AppendFormat("<td align = 'right' colspan = '8' ><b><font size=2>Date: {0}</b></td>", DateTime.Now);
                    sb.AppendFormat("<tr><td align='center' colspan = '6'><b><font size=4>{0}</b></font></td></tr>", OrganizationName);
                    //sb.Append("<tr><td colspan = '2'></td></tr>");
                    sb.AppendFormat("<tr><td align='center' colspan = '6'><i><font size=2>{0}</i></td></tr>", NonProfitVerbage);
                    sb.AppendFormat("<tr><td align='center' colspan = '6'><b><font size=2>{0}</b></td></tr>", TaxID);
                    sb.AppendFormat("<tr><td align='left' colspan = '2'><b><font size=2>Donation No: {0}</b></td></tr>", transactionID);
                    sb.Append("<br />");
                    sb.Append("</table>");

                    sb.AppendLine("<b>Organization Address:</b>");
                    sb.AppendFormat("<font size=2><p>{0}<br>{1}<br>{2},{3}</p>", OrganizationAddress, City, State, Zip);
                    sb.AppendFormat("<font size=2><p><b>Phone: {0}</b></p>", OrganizationPhone);
                    sb.AppendFormat("<font size=2><p><b>Email: {0}</b></p>", SenderEmailAddress);
                    sb.Append("<br />");
                    sb.AppendFormat("<p>Thank you for your generous contribution to {0}.</p>", OrganizationName);
                    sb.AppendFormat("<p>{0} provided no goods or services in return for this donation.</p>", OrganizationName);
                    sb.AppendFormat("<p>For Federal Income tax purposes the donation amount shown below is deductible to the extent allowed by law. Please retain the attached receipt to substantiate your charitable donation.</p>");
                    sb.Append("<br />");

                    // Generate Donation Receipt Items Grid.
                    sb.Append("<table border = '1'>");
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<th>");
                        sb.Append(column.ColumnName);
                        sb.Append("</th>");
                    }
                    sb.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            sb.Append("<td>");
                            if (column.ColumnName == "DonationAmount")
                                sb.Append($"${row[column]}");
                            else
                                sb.Append(row[column]);
                            sb.Append("</td>");
                        }
                        sb.Append("</tr>");
                    }

                    sb.Append("<tr><td align = 'center' colspan = '");
                    sb.Append(dt.Columns.Count - 1);
                    sb.Append("'>TotalAmount</td>");
                    sb.Append("<td>");
                    sb.Append("$" + dt.Compute("sum(DonationAmount)", ""));
                    sb.Append("</td>");
                    sb.Append("</tr></table>");
                    sb.AppendLine("<br/>");
                    sb.Append("<p>Thank You, <br> Saibaba Seattle</p>");

                    //Export HTML String as PDF.
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                    Image gif = Image.GetInstance(Logo);
                    gif.ScaleToFit(200f, 200f);

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    MemoryStream memStream = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memStream);

                    // Build PDF
                    pdfDoc.Open();
                    pdfDoc.Add(gif);
                    htmlparser.Parse(sr);
                    writer.CloseStream = false;

                    if (string.IsNullOrWhiteSpace(donation.Email))
                    {
                        var value = string.Format("attachment;filename={0}", attachmentName);
                        // ToDo : Add logic to download pdf to local box in case of no email mentioned during donation.

                        //Response.ContentType = "application/pdf";
                        ////Response.AddHeader("content-disposition", value);
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.Write(pdfDoc);
                        //Response.End();
                        //writer.Flush();
                        //Response.Close();
                    }

                    else
                    {
                        pdfDoc.Close();
                        memStream.Position = 0;

                        StringBuilder emailBody = new StringBuilder();

                        // Email Body
                        emailBody.AppendFormat("<tr><td>Dear {0},</td></tr>", donation.Name);
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendFormat("<tr><td>Thank you for your generous contribution to {0}.</td></tr>", OrganizationName);
                        emailBody.AppendLine();
                        emailBody.AppendFormat("<tr><td>Please retain the attached receipt to substantiate your charitable donation.</td></tr>");
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendLine("<tr><td>Thank You,</td></tr>");
                        emailBody.AppendFormat("<tr><td>{0}</td></tr>", OrganizationName);

                        // Build email
                        MailMessage msg = new MailMessage();
                        msg.Body = emailBody.ToString();
                        msg.Subject = EmailSubject;
                        msg.From = new MailAddress(SenderEmailAddress, "SaiBaba Seattle");
                        msg.To.Add(new MailAddress(donation.Email, donation.Email));
                        Attachment attachment = new Attachment(memStream, attachmentName);
                        msg.Attachments.Add(attachment);
                        msg.IsBodyHtml = true;

                        //EntityContext entityContext = new EntityContext();
                        EntityContext entityContext = new EntityContext();
                        var adminList = entityContext.adminUser.ToList();
                        if (adminList != null && adminList.Count > 0)
                        {
                            var list = adminList.Select(q => q.Email).ToList();
                            foreach (var email in list)
                            {
                                msg.Bcc.Add(new MailAddress(email, email));
                            }
                        }

                        // Send email
                        SmtpClient smtp = new SmtpClient(SmtpHostName, 587);

                        smtp.Credentials = new NetworkCredential(SenderEmailAddress, SenderEmailPassword);

                        smtp.EnableSsl = true;

                        smtp.Send(msg);
                    }
                }
            }
        }

        public static void GenerateYearEndDonationReceiptPdf()
        {
            var entityContext = new EntityContext();

            var lastYearFirstDay = new DateTime(DateTime.Now.Year - 1, 1, 1);
            var lastYearLastDay = new DateTime(DateTime.Now.Year - 1, 12, 31);

            // Collect donation data for the last year.
            var donationData = (from rep in entityContext.reportInfo
                                where (rep.FromEmailAddress != null && rep.FromEmailAddress !=string.Empty)
                                && (rep.Date >= lastYearFirstDay && rep.Date <= lastYearLastDay)
                                select new DonorInfo
                                {
                                    Name = rep.Name,
                                    DonorEmailAddress = rep.FromEmailAddress,
                                    TransactionID = rep.TransactionID,
                                    Date = rep.Date,
                                    Reason = rep.Reason,
                                    Net = rep.Net
                                }).GroupBy(x => x.DonorEmailAddress).ToDictionary(x => x.Key, v => v.ToList());

            foreach (var donorEmailAddress in donationData.Keys)
            {
                var donorName = donationData[donorEmailAddress].FirstOrDefault().Name;
                var donorTrxId = donationData[donorEmailAddress].FirstOrDefault().TransactionID;

                var attachmentName = "DonationReceipt_" + donorName + "_" + DateTime.Today.AddYears(-1).Year.ToString() + ".pdf";
                var donationDataTable = ConvertListToDataTable(donationData[donorEmailAddress]);

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        StringBuilder sb = new StringBuilder();
                        // Generate Donation Receipt Header and Body.
                        sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");
                        sb.AppendFormat("<tr><td align='center' style='background-color: #18B5F0' colspan = '6'><b><font size=4>{0}</b></font></td></tr>", "Donation Receipt");
                        sb.AppendFormat("<td align = 'right' colspan = '8' ><b><font size=2>Date: {0}</b></td>", DateTime.Now);
                        sb.AppendFormat("<tr><td align='center' colspan = '6'><b><font size=4>{0}</b></font></td></tr>", OrganizationName);
                        //sb.Append("<tr><td colspan = '2'></td></tr>");
                        sb.AppendFormat("<tr><td align='center' colspan = '6'><i><font size=2>{0}</i></td></tr>", NonProfitVerbage);
                        sb.AppendFormat("<tr><td align='center' colspan = '6'><b><font size=2>{0}</b></td></tr>", TaxID);
                        sb.AppendFormat("<tr><td align='left' colspan = '2'><b><font size=2>Donation No: {0}</b></td></tr>", donorTrxId);
                        sb.Append("<br />");
                        sb.Append("</table>");

                        sb.AppendLine("<b>Organization Address:</b>");
                        sb.AppendFormat("<font size=2><p>{0}<br>{1}<br>{2},{3}</p>", OrganizationAddress, City, State, Zip);
                        sb.AppendFormat("<font size=2><p><b>Phone: {0}</b></p>", OrganizationPhone);
                        sb.AppendFormat("<font size=2><p><b>Email: {0}</b></p>", SenderEmailAddress);
                        sb.Append("<br />");
                        sb.AppendFormat("<p>Thank you for your generous contribution to {0}.</p>", OrganizationName);
                        sb.AppendFormat("<p>{0} provided no goods or services in return for this donation.</p>", OrganizationName);
                        sb.AppendFormat("<p>For Federal Income tax purposes the donation amount shown below is deductible to the extent allowed by law. Please retain the attached receipt to substantiate your charitable donation.</p>");
                        sb.Append("<br />");

                        // Generate Donation Receipt Items Grid.
                        sb.Append("<table border = '1'>");
                        sb.Append("<tr>");
                        foreach (DataColumn column in donationDataTable.Columns)
                        {
                            sb.Append("<th>");
                            sb.Append(column.ColumnName);
                            sb.Append("</th>");
                        }
                        sb.Append("</tr>");
                        foreach (DataRow row in donationDataTable.Rows)
                        {
                            sb.Append("<tr>");
                            foreach (DataColumn column in donationDataTable.Columns)
                            {
                                sb.Append("<td>");
                                sb.Append(row[column]);
                                sb.Append("</td>");
                            }
                            sb.Append("</tr>");
                        }

                        sb.Append("<tr><td align = 'center' colspan = '");
                        sb.Append(donationDataTable.Columns.Count - 1);
                        sb.Append("'>TotalAmount</td>");
                        sb.Append("<td>");
                        sb.Append("$" + donationDataTable.Compute("sum(DonationAmount)", ""));
                        sb.Append("</td>");
                        sb.Append("</tr></table>");
                        sb.AppendLine("<br/>");
                        sb.Append("<p>Thank You, <br> Saibaba Seattle</p>");

                        //Export HTML String as PDF.
                        StringReader sr = new StringReader(sb.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                        Image gif = Image.GetInstance(Logo);
                        gif.ScaleToFit(200f, 200f);

                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        MemoryStream memStream = new MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memStream);

                        // Build PDF
                        pdfDoc.Open();
                        pdfDoc.Add(gif);
                        htmlparser.Parse(sr);
                        writer.CloseStream = false;

                        pdfDoc.Close();
                        memStream.Position = 0;

                        StringBuilder emailBody = new StringBuilder();

                        // Email Body
                        emailBody.AppendFormat("<tr><td>Dear {0},</td></tr>", donorName);
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendFormat("<tr><td>Thank you for your generous contribution to {0}.</td></tr>", OrganizationName);
                        emailBody.AppendLine();
                        emailBody.AppendFormat("<tr><td>Please retain the attached receipt to substantiate your charitable donation.</td></tr>");
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendLine();
                        emailBody.AppendLine("<tr><td>Thank You,</td></tr>");
                        emailBody.AppendFormat("<tr><td>{0}</td></tr>", OrganizationName);

                        // Build email
                        MailMessage msg = new MailMessage();
                        msg.Body = emailBody.ToString();
                        msg.Subject = EmailSubject;
                        msg.From = new MailAddress(SenderEmailAddress, "SaiBaba Seattle");
                        msg.To.Add(new MailAddress(donorEmailAddress, donorEmailAddress));
                        Attachment attachment = new Attachment(memStream, attachmentName);
                        msg.Attachments.Add(attachment);
                        msg.IsBodyHtml = true;

                        // Send email
                        SmtpClient smtp = new SmtpClient(SmtpHostName, 587);

                        smtp.Credentials = new NetworkCredential(SenderEmailAddress, SenderEmailPassword);

                        smtp.EnableSsl = true;

                        smtp.Send(msg);
                    }
                }
            }
        }


        public static DataTable ConvertListToDataTable(List<DonorInfo> donorInfoList)
        {
            // New table.
            DataTable table = new DataTable();

            // Add columns.
            table.Columns.AddRange(new DataColumn[3]
            {
                    new DataColumn("DonationId", typeof(string)),
                    new DataColumn("Date", typeof(string)),
                    new DataColumn("DonationAmount", typeof(decimal))
            });

            // Add rows.
            foreach (var row in donorInfoList)
            {
                var net = row.Net.Split('$');
                var donationAmount = Decimal.Parse(net[1].Substring(0, net[1].Length-1));
                table.Rows.Add(row.TransactionID??"0", row.Date.ToString("yyyy-MM-dd"), donationAmount);
            }

            return table;
        }

    }
}