
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.IdentityModel.Protocols;

namespace bot.Dialogs
{
    public class MeetingDialog : ComponentDialog
    {
        private const string Nametext = "What is your name";
        private const string Emailtext = "Where what is your email?";
        private const string Phonenumbertext = "What is your phone?";
        private const string Meetingtimetext = "What is the best time to meet?";

        public MeetingDialog()
            : base(nameof(MeetingDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NameStepAsync,
                EmailStepAsync,
                PhonenumberStepAsync,
                DateandTimeStepAsync,
                ConfirmStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["Information"] = new MeetingInformation();
            var message = "Can you provide the below informations";
            message += "\r\n" + Nametext;
            var promptMessage = MessageFactory.Text(message, Nametext, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> EmailStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var Details = (MeetingInformation)stepContext.Values["Information"];
            Details.Name = stepContext.Context.Activity.Text;
          
            var promptMessage = MessageFactory.Text(Emailtext, Emailtext, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
 
        }

        private async Task<DialogTurnResult> PhonenumberStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var Details = (MeetingInformation)stepContext.Values["Information"];
            Details.email = stepContext.Context.Activity.Text;

            var promptMessage = MessageFactory.Text(Phonenumbertext, Phonenumbertext, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);

        }
        private async Task<DialogTurnResult> DateandTimeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var Details = (MeetingInformation)stepContext.Values["Information"];
            Details.phonenumber = stepContext.Context.Activity.Text;
          
            var promptMessage = MessageFactory.Text(Meetingtimetext, Meetingtimetext, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);

        }


        private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var Details = (MeetingInformation)stepContext.Values["Information"];
            Details.dateandtime = stepContext.Context.Activity.Text;

            var messageText = $"Please confirm your details: \r\n name: {Details.Name} \r\n Email:{Details.email} \r\n phonenumber: {Details.phonenumber} \r\n Meeting date and time: {Details.dateandtime} \r\n Is this correct?";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);

            return await stepContext.PromptAsync(nameof(ConfirmPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var Details = (MeetingInformation)stepContext.Values["Information"];
            SendMail(Details);
            var message = "Your email has been sent to the hr! You will hear back from hr within 24 hours. Thank you";
            var promptMessage = MessageFactory.Text(message, message, InputHints.ExpectingInput);
            await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);

            return await stepContext.BeginDialogAsync(nameof(MainDialog));
        }

        private void SendMail(MeetingInformation info)
        {
            try
            {
                var senderID = "praneeth.1002@gmail.com";
                var senderPassword ="Praneeth12";

                StringBuilder emailBody = new StringBuilder();
                string nextLine = "<br />";
                // Email Body
                emailBody.AppendFormat("Dear {0},", "Admin");
                emailBody.AppendLine();
                emailBody.AppendFormat("This is a email to set up a meeting time.", nextLine, nextLine);
                emailBody.AppendFormat("The applicant's information is:", nextLine, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("Name: {0}", info.Name,nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("Email: {0}", info.email,nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("Phone number: {0}", info.phonenumber, nextLine);
                emailBody.AppendLine();
                emailBody.AppendFormat("Date and time to meet: {0}", info.dateandtime, nextLine);
                emailBody.AppendLine();
                emailBody.AppendLine();
                emailBody.AppendFormat("Email From", nextLine);
                emailBody.AppendFormat("Bot Template");
             

                string body = emailBody.ToString();

                MailMessage msg = new MailMessage();
                msg.Body = body;
                msg.Subject = "Scheduling meeting for client";
                msg.From = new MailAddress("Praneeth.1002@gmail.com");
                msg.To.Add(new MailAddress("spy01.cool@gmail.com"));

               
                msg.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(senderID, senderPassword);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(msg);
                }
            }
            catch (Exception ex)
            {
                var exInfo = new
                {
                    message = ex.Message,
                    innerException = ex.InnerException,
                    stackTrace = ex.StackTrace
                };
                //var filePath = Server.MapPath(@"\Data\OutPut.txt");
               // System.IO.File.AppendAllText(filePath, "\n" + exInfo.ToString() + "," + DateTime.UtcNow.ToString());
            }
        }
    }
}
