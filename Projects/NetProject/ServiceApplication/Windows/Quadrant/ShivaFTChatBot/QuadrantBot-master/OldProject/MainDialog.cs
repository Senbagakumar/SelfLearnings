

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
        private readonly Recognizer _luisRecognizer;
        protected readonly ILogger Logger;
        public MainDialog(Recognizer luisRecognizer, ILogger<MainDialog> logger)
            : base(nameof(MainDialog))
        {
            _luisRecognizer = luisRecognizer;
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroAsync,
                PromptsAsync,
                EndMessageAsync,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (!_luisRecognizer.IsConfigured)
            {
                return await stepContext.NextAsync(null, cancellationToken);
            }

            var messageText = stepContext.Options?.ToString() ?? "What can I help you with today? \r\n 1. About \r\n 2. Application";
            var promptMessage = MessageFactory.Text(messageText, messageText, InputHints.ExpectingInput);
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = promptMessage }, cancellationToken);
        }

        private async Task<DialogTurnResult> PromptsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var x = (string)stepContext.Options;

            if (x.Contains("1"))
            {
                return await stepContext.BeginDialogAsync(nameof(HelperDialog), cancellationToken);
            } else if (x.Contains("2"))
            {
                return await stepContext.BeginDialogAsync(nameof(ApplicationDialog), cancellationToken);
            }
           
            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> EndMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            // Restarts the main dialog with the initial message
            var promptMessage = "What else can I help you with today? \r\n 1. About \r\n 2. Application";
            return await stepContext.ReplaceDialogAsync(InitialDialogId, promptMessage, cancellationToken);
        }
    }
}
