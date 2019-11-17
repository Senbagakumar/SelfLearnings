

using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bot.Dialogs
{
    public class HelperDialog : ComponentDialog
    {
        private const string initialText = "What would you like help with: \r\n 1. Timings & Locations \r\n 2. Contacts";
        private const string RepromptText = "sorry, please enter the service you would like help with";

        public HelperDialog(string id = null)
            : base(id ?? nameof(HelperDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                InitialStepAsync,
     
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var x = (string)stepContext.Options;

            var promptMessage = MessageFactory.Text(initialText, initialText, InputHints.ExpectingInput);
            var repromptMessage = MessageFactory.Text(RepromptText, RepromptText, InputHints.ExpectingInput);


            if (x.Contains("1"))
            {
                MessageFactory.Text("Timings info");

            }
            else if (x.Contains("2"))
            {
                MessageFactory.Text("contact info");
            }

            return await stepContext.NextAsync(cancellationToken);
        }
    }
   
}
