// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace SaiTempleAssistance
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service. Transient lifetime services are created
    /// each time they're requested. Objects that are expensive to construct, or have a lifetime
    /// beyond a single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public partial class SaiTempleAssistance : IBot
    {
        private readonly BotAccessor _botAccessor;
        private readonly DialogSet _dialogSet;
        private readonly IServices _services;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SaiTempleAssistance(BotAccessor botAccessor)
        {
            _botAccessor = botAccessor ?? throw new ArgumentNullException(nameof(botAccessor));
            _dialogSet = new DialogSet(_botAccessor.DialogStateAccessor);
            _services = new Orchestra();
        }

        /// <summary>
        /// Every conversation turn calls this method.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {

            if (turnContext == null)
                throw new ArgumentException(nameof(turnContext));

            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                DialogContext dialogContext = await _dialogSet.CreateContextAsync(turnContext, cancellationToken);
                DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);
                switch (results.Status)
                {
                    case DialogTurnStatus.Cancelled:
                    case DialogTurnStatus.Empty:
                        // If there is no active dialog, we should clear the user info and start a new dialog.
                        //await _botAccessor.UserProfileAccessor.SetAsync(turnContext, new IcMProfile(), cancellationToken);
                        await _botAccessor.UserState.SaveChangesAsync(turnContext, false, cancellationToken);

                        var gm = GreetingMessages(turnContext.Activity.Text);
                        if (!string.IsNullOrWhiteSpace(gm))
                        {
                            await turnContext.SendActivityAsync(Constants.GreetingReplyMsg + "\n"+ Constants.ContinueMsg);
                        }
                        else
                        {
                            var responseMessage = _services.GetServiceInfo(turnContext.Activity);
                            if (responseMessage != null)
                                await turnContext.SendActivityAsync(responseMessage, cancellationToken);
                            else
                                await turnContext.SendActivityAsync(Constants.VerifyMsg);

                            await turnContext.SendActivityAsync(Constants.EndMsg);
                            await turnContext.SendActivityAsync(Constants.ContinueMsg, cancellationToken: cancellationToken);
                        }
                        break;
                    case DialogTurnStatus.Complete:
                        await turnContext.SendActivityAsync(Constants.EndMsg);
                        break;
                    case DialogTurnStatus.Waiting:
                        await turnContext.SendActivityAsync(Constants.VerifyMsg);
                        // If there is an active dialog, we don't need to do anything here.
                        break;
                }

                await _botAccessor.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);

                // Echo back to the user whatever they typed. Please verify your request services
                //await turnContext.SendActivityAsync("Hello World", cancellationToken: cancellationToken);
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                IConversationUpdateActivity update = turnContext.Activity;
                if (update.MembersAdded != null && update.MembersAdded.Any())
                {
                    foreach (var newMember in update.MembersAdded)
                    {
                        if (newMember.Id != update.Recipient.Id)
                        {
                            await turnContext.SendActivityAsync(Constants.GreetingMsg + "\n" + Constants.ContinueMsg, cancellationToken: cancellationToken);
                        }
                    }
                }
            }
            //else //if (turnContext.Activity.Type == ActivityTypes.ContactRelationUpdate)
            //{
            //    //if (turnContext.Activity.Action == "add")
            //    //{
            //        await turnContext.SendActivityAsync(Constants.WelcomeMsg, cancellationToken: cancellationToken);
            //    //}
            //}
        }

        private string GreetingMessages(string requestMsg)
        {
            requestMsg = requestMsg.Trim().ToLower();
            if (requestMsg.Contains("hi") ||
               requestMsg.Contains("hellow") ||
               requestMsg.Contains("hey") ||
               requestMsg.Contains("how") ||
               requestMsg.Contains("good"))
                return Constants.GreetingMsg;
            else
                return string.Empty;

        }

    }
}
