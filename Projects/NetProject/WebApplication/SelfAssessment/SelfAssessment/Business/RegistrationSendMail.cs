using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SelfAssessment.Business
{
    public class RegistrationSendMail
    {
        public void Send(MailConfiguration mail)
        {

            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = mail.Port;
            client.Host = mail.Host;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(mail.NetworkUserName, mail.NetworkPassword);
            objeto_mail.From = new MailAddress(mail.FromId);
            objeto_mail.To.Add(new MailAddress(mail.ToId));
            objeto_mail.Subject = "Password Recover";
            objeto_mail.Body = "Message";
            client.Send(objeto_mail);
        }
    }
}