using MyNPO.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace MyNPO.Models
{

    public class Notifications
    {
        public static void NotificationToAdmins(string typeOfAction, CalendarInfo calendarInfo)
        {
            var senderID = ConfigurationManager.AppSettings["SenderEmailAddress"].ToString();
            EntityContext entityContext = new EntityContext();
            var adminList = entityContext.adminUser.ToList();
            string allMails = string.Empty;
            allMails = senderID;
            if(adminList!=null && adminList.Count > 0)
                allMails = string.Join(";", adminList.Select(q => q.Email).ToList());

            string Name = string.Empty;
            var userMailId = new string[2];
            if(typeOfAction.Contains(";"))
            {
                userMailId = typeOfAction.Split(';');
                allMails = allMails + ";" + userMailId[1];
            }
            
            if (typeOfAction.Contains("Temple"))
                Name = "Priest Services for"+ calendarInfo.Name;
            else
                Name = "Room Services Below";

            MailSender.EventSendMail(new EventInfo() { Email = allMails, Event = new Event() { Details = typeOfAction + "-Title- " + calendarInfo.Text, EndDate = calendarInfo.EndDate, StartDate = calendarInfo.StartDate, Location = calendarInfo.Address ?? "Redmond", Name = Name } });
        }
    }

    public class EventInfo
    {
        public string Email { get; set; }
        public Event Event { get; set; }
    }
    public class MailSender
    {
        #region testing code for send mail
        #endregion

        public static void EventSendMail(EventInfo eventInfo)
        {
            var senderID = ConfigurationManager.AppSettings["SenderEmailAddress"].ToString();
            var senderPassword = MyNPO.Utilities.Helper.Decrypt(ConfigurationManager.AppSettings["SenderEmailPassword"].ToString());
           var hostName = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
            string body = eventInfo.Event.Details;


            MailMessage msg = new MailMessage();
            msg.Body = body;
            msg.Subject = eventInfo.Event.Name;
            msg.From = new MailAddress(senderID, "SaiBaba");

            var listOfEmails=eventInfo.Email.Split(';');

            foreach(var email in listOfEmails)
                msg.To.Add(new MailAddress(email, email));

            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(hostName, 587);

            smtp.Credentials = new NetworkCredential(senderID,senderPassword);

            msg = GetCalanderInviteMsg3(msg,eventInfo.Event);

            //smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;

            smtp.Send(msg);


        }

        public static MailMessage GetCalanderInviteMsg3(MailMessage msg,Event eventDetails)
        {

            string TimeFormat = "yyyyMMdd\\THHmmss\\Z";

            string start = eventDetails.StartDate.ToString(TimeFormat);
            string end = eventDetails.EndDate.ToString(TimeFormat);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Google Inc//Google Calendar 70.9054//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");
            sb.AppendLine("X-WR-CALNAME:Come to the SaiBaba Redmond temple");
            sb.AppendLine("X-WR-TIMEZONE:America/Redmond");
            sb.AppendLine("X-WR-CALDESC:");
            sb.AppendLine("TZID:America/Redmond");
            sb.AppendLine("BEGIN:VEVENT");

            //sb.AppendLine("DTSTART;VALUE=DATE:"+ start);
            //sb.AppendLine("DTEND;VALUE=DATE:"+ end);

            sb.AppendLine("DTSTART;TZID=America/Redmond:" + start);
            sb.AppendLine("DTEND;TZID=America/Redmond:" + end);

            sb.AppendLine("DTSTAMP:"+ DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("UID:" + Guid.NewGuid());
            sb.AppendLine("CREATED:" + DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("DESCRIPTION:Pradishta Event");
            sb.AppendLine(string.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));
            sb.AppendLine(string.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));
            sb.AppendLine("LAST-MODIFIED:" + DateTime.UtcNow.AddDays(-1).ToString(TimeFormat));
            sb.AppendLine("LOCATION:"+eventDetails.Location);
            sb.AppendLine("SEQUENCE:0");
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:"+eventDetails.Name);
            sb.AppendLine("TRANSP:TRANSPARENT");

            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine("TRIGGER:-PT15M");
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:Reminder");
            sb.AppendLine("END:VALARM");
            sb.AppendLine("END:VEVENT");

            sb.AppendLine("END:VCALENDAR");
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType("text/calendar");
            ct.Parameters.Add("method", "REQUEST");
            AlternateView avCal = AlternateView.CreateAlternateViewFromString(sb.ToString(), ct);
            msg.AlternateViews.Add(avCal);

            return msg;
        }
    }
}