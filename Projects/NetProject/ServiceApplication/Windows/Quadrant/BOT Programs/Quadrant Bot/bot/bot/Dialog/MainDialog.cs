

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;


namespace bot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        
        protected readonly ILogger Logger;
        public MainDialog(ILogger<MainDialog> logger, ApplicationDialog applicationdialog, HelperDialog helperdialog, MeetingDialog meetingdialog)
            : base(nameof(MainDialog))
        {
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(helperdialog);
            AddDialog(applicationdialog);
            AddDialog(meetingdialog);
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroAsync,
                PromptsAsync,
            }));
            

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            var messageText = stepContext.Options?.ToString() ?? "What can I help you with today? \r\n 1. About Quadrant \r\n 2. Job Vacancy \r\n 3. Schedule a meeting \r\n If you would like to exit please enter bye or no.";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> PromptsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var x = stepContext.Context.Activity.Text;

            if (x == "1" || x.Contains("About", StringComparison.OrdinalIgnoreCase))
            {
                return await stepContext.BeginDialogAsync(nameof(HelperDialog), cancellationToken);
            }
            else if (x == "2" || x.Contains("Job", StringComparison.OrdinalIgnoreCase))
            {
                return await stepContext.BeginDialogAsync(nameof(ApplicationDialog), cancellationToken);
            }
            else if (x == "3" || x.Contains("Schedule Meeting", StringComparison.OrdinalIgnoreCase))
            {
                return await stepContext.BeginDialogAsync(nameof(MeetingDialog), cancellationToken);
            }
            else if (x.Contains("bye", StringComparison.OrdinalIgnoreCase) || x.Contains("no", StringComparison.OrdinalIgnoreCase))
            {
                var messageText = stepContext.Options?.ToString() ?? "Thank you have a nice day! \r\n The chat with the bot has ended.";
                return await stepContext.EndDialogAsync(messageText, cancellationToken);

            }
            return null;
        }

        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            // Restarts the main dialog with the initial message
            var promptMessage = "Please type a valid input: \r\n 1. About \r\n 2. Application";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
        }
    }
}
