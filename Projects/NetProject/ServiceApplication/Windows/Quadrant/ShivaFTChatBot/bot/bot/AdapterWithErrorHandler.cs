

using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace bot
{
    public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
    {
        private const string ErrorMsgText = "Sorry, it looks like something went wrong. Please enter valid input and try again! Say hi to restart";

        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger, ConversationState conversationState = null)
            : base(configuration, logger)
        {
            OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError($"Exception caught : {exception.Message}");

                // Send a catch-all apology to the user.
                var errorMessage = MessageFactory.Text(ErrorMsgText, ErrorMsgText, InputHints.ExpectingInput);
                await turnContext.SendActivityAsync(errorMessage);

                if (conversationState != null)
                {
                    try
                    {
                        await conversationState.DeleteAsync(turnContext);
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"Exception caught on attempting to Delete ConversationState : {e.Message}");
                    }
                }
            };
        }
    }
}
