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
            }
            else if (x == "2" || x == "Texas")
            {
                message = "The address is: 3333 Lee ParkwaySuite 600 Dallas, TX – 75219";
                message += "\r\n The timings for this location are monday to friday, 8am - 6pm";
            }
            else if (x == "1" || x == "Washington")
            {
                message = "The address is: 4034 148th Ave NE, Suite K1C1, Redmond, WA 98052";
                message += "\r\n The timings for this location are monday to friday, 8am - 6pm";
            }
            else if (x == "4" || x == "Bangaluru")
            {
                message = "The address is: 936, SCK complex, Second Floor,Channasandra, Kengeri main road,Rajarajeshwari Nagar,Bengaluru,Karnataka.";
                message += "\r\n The timings for this location are unknown";
            }
            message += "\r\n Do you want to continue? 1. Yes or 2. no";
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(message) }, cancellationToken);


        }

        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var y = (Properties)stepContext.Values["dialog"];

            if (stepContext.Context.Activity.Text == "yes" && !string.IsNullOrWhiteSpace(y.Questionset2))
            {
                y.Questionset2 = string.Empty;
                return await stepContext.BeginDialogAsync(nameof(PermissionDialog));
            }
            else
            {
                y.Questionset1 = string.Empty;
                y.Questionset2 = string.Empty;
                return await stepContext.BeginDialogAsync(nameof(MainDialog));
            }

        }
    }

}
