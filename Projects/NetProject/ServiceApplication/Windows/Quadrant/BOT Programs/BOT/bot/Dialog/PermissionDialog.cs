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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Dynamic;

namespace bot.Dialogs
{
    public class PermissionDialog : ComponentDialog
    {
        private const string initialText = "Select your reporting manager from the below: \r\n 1. Shiva \r\n 2. Senba";

        public PermissionDialog(LocationDialog locationdialog)
            : base(nameof(PermissionDialog))
        {   

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(locationdialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                InitialStepAsync,
                PromptsAsync,
                EndMessageAsync,
            }));
            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //var y = (Properties)stepContext.Options;
            //if (y != null && !string.IsNullOrWhiteSpace(y.Questionset1))
            //{
            //    return await stepContext.NextAsync(y);
            //}
            //else
            //{
            //    if (stepContext.Values.Count == 0)
            //    {
            //        stepContext.Values["dialog"] = new Properties();
            //    }
                var promptMessage = MessageFactory.Text(initialText, initialText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
           // }
        }

        private async Task<DialogTurnResult> PromptsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var option = stepContext.Context.Activity.Text;

            //var y = (Properties)stepContext.Options;
            //if (y == null)
            //    y = (Properties)stepContext.Values["dialog"];

            string inputValue; //= y!=null ? y.Questionset1 : string.Empty;
            //if (string.IsNullOrWhiteSpace(inputValue))
                inputValue = option;

            string message = string.Empty;
            if (inputValue == "1" || inputValue.Contains("shiva", StringComparison.OrdinalIgnoreCase))
            {
                //y.Questionset1 = "shiva";
                //y.IsPermissionVisited = true;

                //C:\ShivaFTChatBot\Inputfile.Json
                RootObject  ro = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"C:\ShivaFTChatBot\Inputfile.Json"));
                var listOfPermission = ro.BuddyBOT.Owners[0].Permission;
                var lgeneral = new List<GeneralModal>();
                listOfPermission.ForEach(q => { lgeneral.Add(new GeneralModal() { FirstColumn = q.ProjectID, SecondColumn = q.Name });});
                //var responseMessage =
                var response = stepContext.Context.Activity.CreateReply();
                var acd = new AdaptiveCardDesign("ProjectID", "Name");
                var attachment = acd.LoopCardAdaptiveCard(lgeneral);
                response.Attachments = new List<Attachment>() { attachment };

                await stepContext.Context.SendActivityAsync(response, cancellationToken);
               // return await stepContext.BeginDialogAsync(nameof(LocationDialog), listOfPermission, cancellationToken);
            }
            else if (option == "2" || option.Contains("senba", StringComparison.OrdinalIgnoreCase))
            {
                //y.Questionset1 = "senba";
                //message = ("You can contact Vamshi Reddy at Vamshi@quadrantresource.com");
                message += ("\r\n Under Progress");
                await stepContext.Context.SendActivityAsync(message);
            }
            var m = "Would you like to continue with this options? \r\n 1. Permission \r\n 2. Certifications \r\n If you would like to exit please enter bye or no.";
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text(m) }, cancellationToken);
            //return await stepContext.BeginDialogAsync(nameof(MainDialog), y, cancellationToken);
        }

        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //var y = (Properties)stepContext.Values["dialog"];

            //string nameOfDialog = string.Empty;

            //if (stepContext.Context.Activity.Text.Equals("yes",StringComparison.OrdinalIgnoreCase) || stepContext.Context.Activity.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
            //{
            //    return await stepContext.BeginDialogAsync(nameof(CertificationDialog), y, cancellationToken);
            //}
            //else
            //{
            //    return await stepContext.BeginDialogAsync(nameof(MainDialog), y, cancellationToken);
            //}
            if (stepContext.Context.Activity.Text.Equals("Permission", StringComparison.OrdinalIgnoreCase) ||
              stepContext.Context.Activity.Text.Equals("1", StringComparison.OrdinalIgnoreCase) ||
              stepContext.Context.Activity.Text.Equals("Certifications", StringComparison.OrdinalIgnoreCase) ||
              stepContext.Context.Activity.Text.Equals("2", StringComparison.OrdinalIgnoreCase)
              )

            {
                //    return await stepContext.BeginDialogAsync(nameof(PermissionDialog),y,cancellationToken);
                //}
                //else
                //{

                return await stepContext.BeginDialogAsync(nameof(MainDialog), cancellationToken);
            }
            else
            {
                return await stepContext.EndDialogAsync();
            }

        }
    }
   
}
