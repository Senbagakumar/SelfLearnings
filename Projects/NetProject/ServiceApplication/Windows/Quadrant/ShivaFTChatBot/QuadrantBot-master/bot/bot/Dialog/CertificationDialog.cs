

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using Newtonsoft.Json;

namespace bot.Dialogs
{
    public class CertificationDialog : ComponentDialog
    {
        private const string InitialText = "Select your reporting manager from the below: \r\n 1. Shiva \r\n 2. Senba";

        public CertificationDialog()
            : base(nameof(CertificationDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                InitialStepAsync,
                EndMessageAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //var y = (Properties)stepContext.Values["dialog"];
            //var value=y.Questionset1;


            //var y = (Properties)stepContext.Options;
            //if (y != null && !string.IsNullOrWhiteSpace(y.Questionset1))
            //{
            //    return await stepContext.NextAsync(y);
            //}
            //else
            //{
            //    if(stepContext.Values.Count == 0)
            //    {
            //        stepContext.Values["dialog"] = new Properties();
            //    }
                var promptMessage = MessageFactory.Text(InitialText, InitialText, InputHints.ExpectingInput);
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
            //}
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            
            var option = stepContext.Context.Activity.Text;

            //var y = (Properties)stepContext.Options;
            //if (y == null)
            //    y = (Properties)stepContext.Values["dialog"];

            string inputValue;// = y != null ? y.Questionset1 : string.Empty;
            //if (string.IsNullOrWhiteSpace(inputValue))
                inputValue = option;

            string message = string.Empty;
            if (inputValue == "1" || inputValue.Contains("shiva", StringComparison.OrdinalIgnoreCase))
            {
              //  y.IsCertificationVisited = true;
              //  y.Questionset1 = "shiva";
                //C:\ShivaFTChatBot\Inputfile.Json
                RootObject ro = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(@"C:\ShivaFTChatBot\Inputfile.Json"));
                var listOfCertification = ro.BuddyBOT.Owners[0].Certifications;

                var lgeneral = new List<GeneralModal>();
                listOfCertification.ForEach(q => { lgeneral.Add(new GeneralModal() { FirstColumn = q.ID, SecondColumn = q.Url }); });

                var response = stepContext.Context.Activity.CreateReply();
                var acd = new AdaptiveCardDesign("ID", "Url");
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

        }

        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //var y = (Properties)stepContext.Options;
            //if(y == null)
            //     y = (Properties)stepContext.Values["dialog"];
            //if (stepContext.Context.Activity.Text.Equals("yes",StringComparison.OrdinalIgnoreCase) || stepContext.Context.Activity.Text.Equals("1", StringComparison.OrdinalIgnoreCase))
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

                return await stepContext.BeginDialogAsync(nameof(MainDialog), stepContext.Context.Activity.Text, cancellationToken);
            }
            else
            {
                return await stepContext.EndDialogAsync();
            }

        }
    }
}
