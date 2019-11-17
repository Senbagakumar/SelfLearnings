//make a quadrant about page
//make an email sending evite page
using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bot.Dialogs
{
    public class LocationDialog : ComponentDialog
    {

        public LocationDialog()
            : base(nameof(LocationDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                InitialAsync,
                LocationsAsync,
                EndMessageAsync,
            }));
            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["dialog"] = new Properties();
            var message = ("What branch would you like to know the timings and location? \r\n 1. Washington – USA \r\n 2. Texas – USA \r\n 3. Hyderabad – India \r\n 4. Bengaluru - India");
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(message) }, cancellationToken);
        }
        private async Task<DialogTurnResult> LocationsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var x = stepContext.Context.Activity.Text;
            var y = (Properties)stepContext.Values["dialog"];
            y.Questionset2 = x;


            string message = string.Empty;
            if (x == "3" || x == "Hyderabad")
            {
                message = "The address is: SoftSol Building, 4th Floor,Tower 2, Inorbit Mall Road,Near Mindspace Flyover Junction,Hitech City, Madhapur,Hyderabad, Telanagana – 500081";
                message += "\r\n The timings for this location are unknown";

                var activity = stepContext.Context.Activity;

                Attachment attachment = new Attachment();
                attachment.ContentType = "ContentType";
                attachment.ContentUrl = "https://www.google.com/maps/search/SoftSol+Building,+4th+Floor,Tower+2,+Inorbit+Mall+Road,Near+Mindspace+Flyover+Junction,Hitech+City,+Madhapur,Hyderabad,+Telanagana+%E2%80%93+500081/@17.4365186,78.3780742,16z/data=!3m1!4b1";
                attachment.Name = "Hyderabad Office Map";

                var reply = activity.CreateReply();
                reply.Attachments = new List<Attachment>() { attachment };

                await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            }
            else if (x == "2" || x == "Texas")
            {
                message = "The address is: 3333 Lee ParkwaySuite 600 Dallas, TX – 75219";
                message += "\r\n The timings for this location are monday to friday, 8am - 6pm";

                var activity = stepContext.Context.Activity;

                Attachment attachment = new Attachment();
                attachment.ContentType = "ContentType";
                attachment.ContentUrl = "https://www.google.com/maps/place/3333+Lee+Pkwy+%23600,+Dallas,+TX+75219/@32.8093709,-96.8074897,17z/data=!3m1!4b1!4m5!3m4!1s0x864e9ecdd3fb0d9f:0x3717cad9f99f2707!8m2!3d32.8093709!4d-96.805301";
                attachment.Name = "Dallas Office Map";

                var reply = activity.CreateReply();
                reply.Attachments = new List<Attachment>() { attachment };

                await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            }
            else if (x == "1" || x == "Washington")
            {
                message = "The address is: 4034 148th Ave NE, Suite K1C1, Redmond, WA 98052";
                message += "\r\n The timings for this location are monday to friday, 8am - 6pm";

                var activity = stepContext.Context.Activity;

                Attachment attachment = new Attachment();
                attachment.ContentType = "ContentType";
                attachment.ContentUrl = "https://www.google.com/maps/place/Quadrant+Resource/@47.6471046,-122.1444964,17z/data=!3m1!4b1!4m5!3m4!1s0x54906d6b0ef47a37:0xa7d57d915d73bed5!8m2!3d47.6471046!4d-122.1423077";
                attachment.Name = "Redmond Office Map";

                var reply=activity.CreateReply();
                reply.Attachments = new List<Attachment>() { attachment };

                await stepContext.Context.SendActivityAsync(reply,cancellationToken);
                
            }
            else if (x == "4" || x == "Bangaluru")
            {
                message = "The address is: 936, SCK complex, Second Floor, Channasandra, Kengeri main road, Rajarajeshwari Nagar, Bengaluru, Karnataka.";
                message += "\r\n The timings for this location are unknown";

                var activity = stepContext.Context.Activity;

                Attachment attachment = new Attachment();
                attachment.ContentType = "ContentType";
                attachment.ContentUrl = "https://www.google.com/maps/place/Pixselo+Global+Solutions/@12.9030372,77.5195796,17z/data=!3m1!4b1!4m5!3m4!1s0x3bae15f0c36ddd57:0xa3aaf3342a1f635b!8m2!3d12.9030372!4d77.5217683";
                attachment.Name = "Bengaluru Office Map";

                var reply = activity.CreateReply();
                reply.Attachments = new List<Attachment>() { attachment };

                await stepContext.Context.SendActivityAsync(reply, cancellationToken);
            }
            message += "\r\n Do you want to continue? Yes or No";
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(message) }, cancellationToken);


        }
        
        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var y = (Properties)stepContext.Values["dialog"];

            if (stepContext.Context.Activity.Text == "yes" || stepContext.Context.Activity.Text == "Yes")
            {
                y.Questionset2 = string.Empty;
                return await stepContext.BeginDialogAsync(nameof(HelperDialog));
            }
            else
            {
                y.Questionset1 = string.Empty;
                y.Questionset2 = string.Empty;
                return await stepContext.BeginDialogAsync(nameof(MainDialog));
            }

        }
        //private async Task<DialogTurnResult> MapLinkAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        //{

        //}
    }
}
