// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace ChatBot
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
    /// 
    public class ChatBotBot : IBot
    {

        private const string WelcomeText =
            "Welcome to IcMBot. This bot provides a Active IcMList. " +
            "Type anything to get started.";

        // Define the dialog and prompt names for the bot.
        private const string TopLevelDialog = "dialog-topLevel";
        private const string ReviewSelectionDialog = "dialog-reviewSeleciton";
        private const string IcMTypePrompt = "prompt-icmtype";
        private const string IcMSeverityPrompt = "prompt-severitytype";
        private const string IcMQueuePrompt = "prompt-icmQueueType";

        // Define a "done" response for the company selection prompt.
        private const string DoneOption = "done";

        // Define value names for values tracked inside the dialogs.
        private const string IcMInfo = "value-userInfo";
        private const string IcMType = "value-IcMTypeSelected";
        private const string IcMSeverity = "value-IcMSeveritySelected";
        private const string IcMQueue = "value-IcMQueueSelected";

        // Define the company choices for the company selection prompt.
        private readonly string[] _icmTypeOptions = new string[]
        {
            "ACTIVE", "MITIGATED"
        };

        private readonly string[] _icmSeverityOptions = new string[]
        {
            "1", "2", "3","4"
        };

        private readonly string[] _icmQueueOptions = new string[]
        {
            "SQLDBperfv-queue", "GeoDR", "Backup/Restore"
        };

        private readonly ChatDialogBotAccessors _accessors;

        /// <summary>
        /// The <see cref="DialogSet"/> that contains all the Dialogs that can be used at runtime.
        /// </summary>
        private readonly DialogSet _dialogs;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexDialogBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        public ChatBotBot(ChatDialogBotAccessors accessors)
        {
            _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

            // Create a dialog set for the bot. It requires a DialogState accessor, with which
            // to retrieve the dialog state from the turn context.
            _dialogs = new DialogSet(accessors.DialogStateAccessor);

            // Add the prompts we need to the dialog set.
            _dialogs
                .Add(new ChoicePrompt(IcMTypePrompt))
                .Add(new ChoicePrompt(IcMSeverityPrompt))
                .Add(new ChoicePrompt(IcMQueuePrompt));

            // Add the dialogs we need to the dialog set.
            _dialogs.Add(new WaterfallDialog(TopLevelDialog)
                .AddStep(IcMTypeStepAsync)
                .AddStep(IcMSeverityStepAsync)
                .AddStep(IcMQueueTypeAsync)
                .AddStep(AcknowledgementStepAsync));
        }

        /// <summary>
        /// Every conversation turn for our EchoBot will call this method.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Run the DialogSet - let the framework identify the current state of the dialog from
                // the dialog stack and figure out what (if any) is the active dialog.
                DialogContext dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
                DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);
                switch (results.Status)
                {
                    case DialogTurnStatus.Cancelled:
                    case DialogTurnStatus.Empty:
                        // If there is no active dialog, we should clear the user info and start a new dialog.
                        await _accessors.UserProfileAccessor.SetAsync(turnContext, new IcMProfile(), cancellationToken);
                        await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
                        await dialogContext.BeginDialogAsync(TopLevelDialog, null, cancellationToken);
                        break;
                    case DialogTurnStatus.Complete:
                        // If we just finished the dialog, capture and display the results.
                        IcMProfile icMInfo = results.Result as IcMProfile;
                        string status = "The above IcM list is displayed based on the selecteion criteria: if you want to continue, please restart the conversation ";
                        await turnContext.SendActivityAsync(status);
                        await _accessors.UserProfileAccessor.SetAsync(turnContext, icMInfo, cancellationToken);
                        await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
                        break;
                    case DialogTurnStatus.Waiting:
                        // If there is an active dialog, we don't need to do anything here.
                        break;
                }

                await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            }

            // Processes ConversationUpdate Activities to welcome the user.
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded != null)
                {
                    await SendWelcomeMessageAsync(turnContext, cancellationToken);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected", cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Sends a welcome message to the user.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            foreach (ChannelAccount member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    Activity reply = turnContext.Activity.CreateReply();
                    reply.Text = WelcomeText;
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                }
            }
        }

        /// <summary>The first step of the top-level dialog.</summary>
        /// <param name="stepContext">The waterfall step context for the current turn.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        /// <remarks>If the task is successful, the result contains a <see cref="DialogTurnResult"/> to
        /// communicate some flow control back to the containing WaterfallDialog.</remarks>
        private async Task<DialogTurnResult> IcMTypeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Create an object in which to collect the user's information within the dialog.
            stepContext.Values[IcMInfo] = new IcMProfile();
            return await stepContext.PromptAsync(
                IcMTypePrompt,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose the IcMType"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_icmTypeOptions.ToList()),
                },
                cancellationToken);
        }

        /// <summary>The second step of the top-level dialog.</summary>
        /// <param name="stepContext">The waterfall step context for the current turn.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        /// <remarks>If the task is successful, the result contains a <see cref="DialogTurnResult"/> to
        /// communicate some flow control back to the containing WaterfallDialog.</remarks>
        private async Task<DialogTurnResult> IcMSeverityStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            // Set the IcMSeverity to what they entered in response to the IcMSeverity selection.
            ((IcMProfile)stepContext.Values[IcMInfo]).IcMType = stepContext.Context.Activity.Text;

            // Ask the user to enter their age.
            return await stepContext.PromptAsync(
                IcMSeverityPrompt,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose the IcMSeverity"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_icmSeverityOptions.ToList()),
                },
                cancellationToken);
        }

        /// <summary>The third step of the top-level dialog.</summary>
        /// <param name="stepContext">The waterfall step context for the current turn.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        /// <remarks>If the task is successful, the result contains a <see cref="DialogTurnResult"/> to
        /// communicate some flow control back to the containing WaterfallDialog.</remarks>
        private async Task<DialogTurnResult> IcMQueueTypeAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            ((IcMProfile)stepContext.Values[IcMInfo]).IcMSeverity = stepContext.Context.Activity.Text;
            return await stepContext.PromptAsync(
                IcMQueuePrompt,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose the IcMQueueType"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_icmQueueOptions.ToList()),
                },
                cancellationToken);
        }

        private Attachment LoopCardAdaptiveCard(List<OutPut> output)
        {
            var columns = new List<Column>();
            var icmIds = new List<CardElement>();
            var icmAckDates = new List<CardElement>();
            var icmLink = new List<CardElement>();

            foreach (var oput in output)
            {
                //icmAction.Add(new CardAction() { Text=oput.IncidentId, Value="https://google.com", Type=OpenUrlAction.TYPE, Title=oput.IncidentId });
                icmIds.Add(new TextBlock() { Text = oput.IncidentId, Separation = SeparationStyle.Strong, Size = TextSize.Small, });//oput.IncidentId
                icmAckDates.Add(new TextBlock() { Text = oput.Title, Separation = SeparationStyle.Strong, Size = TextSize.Small });
                //string url = string.Empty;
                //if (oput.IncidentId == "IncidentID")
                //    url = "IcMLink";
                //else
                //    url = $"https://icm.ad.msft.net/imp/v3/incidents/details/{oput.IncidentId}/home";

                //icmLink.Add(new TextBlock() { Text = url, Separation = SeparationStyle.Strong });
            }

           (icmIds[0] as TextBlock).Weight = TextWeight.Bolder;
            (icmAckDates[0] as TextBlock).Weight = TextWeight.Bolder;
            //(icmAlias[0] as TextBlock).Weight = TextWeight.Bolder;

            for (int i = 0; i < 2; i++)
            {
                var column = new Column() { Style = ContainerStyle.Normal };
                if (i == 0) { column.Items = icmIds; }
                //if (i == 1) { column.Items = icmLink; }
                if (i == 1) { column.Items = icmAckDates; }
                columns.Add(column);
            }



            var container = new Container() { Items = new List<CardElement>() { new ColumnSet() { Columns = columns } } };
            return new Attachment()
            {               
                ContentType = AdaptiveCard.ContentType,
                Content = new AdaptiveCard() { Body = new List<CardElement>() { container } }
            };

        }
        /// <summary>The final step of the top-level dialog.</summary>
        /// <param name="stepContext">The waterfall step context for the current turn.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A task that represents the work queued to execute.</returns>
        /// <remarks>If the task is successful, the result contains a <see cref="DialogTurnResult"/> to
        /// communicate some flow control back to the containing WaterfallDialog.</remarks>
        private async Task<DialogTurnResult> AcknowledgementStepAsync(
            WaterfallStepContext stepContext,
            CancellationToken cancellationToken)
        {
            // Set the user's company selection to what they entered in the review-selection dialog.
            ((IcMProfile)stepContext.Values[IcMInfo]).IcMQueue = stepContext.Context.Activity.Text;

            var profileInfo = ((IcMProfile)stepContext.Values[IcMInfo]);
            var list = new List<OutPut>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //Assuming that the api takes the user message as a query paramater
                    //http://localhost:15098/api/values/?severity=3&owningTeamName=SQLDBperfv-queue
                    string RequestURI = $@"http://localhost:15098/Api/values/?severity={profileInfo.IcMSeverity}&owningTeamName={profileInfo.IcMQueue}&status={profileInfo.IcMType}";
                    HttpResponseMessage responsemMsg = await client.GetAsync(RequestURI);
                    if (responsemMsg.IsSuccessStatusCode)
                    {
                        var apiResponse = await responsemMsg.Content.ReadAsStringAsync();
                        list = JsonConvert.DeserializeObject<List<OutPut>>(apiResponse);
                        list  = list.Distinct(new IcMComparer()).ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            //Add the Title in the list  , AcknowledgeContactAlias = "Alias"
            list.Insert(0, new OutPut() { IncidentId = "IncidentID", Title = "Title" });


            var welcomeCard = LoopCardAdaptiveCard(list);
            var response = CreateResponse(stepContext.Context.Activity, welcomeCard);
            await stepContext.Context.SendActivityAsync(response, cancellationToken);

            // Exit the dialog, returning the collected user information.
            return await stepContext.EndDialogAsync(stepContext.Values[IcMInfo], cancellationToken);
        }
        private Microsoft.Bot.Schema.Activity CreateResponse(Microsoft.Bot.Schema.Activity activity, Attachment attachment)
        {
            var response = activity.CreateReply();
            response.Attachments = new List<Attachment>() { attachment };
            return response;
        }
    }

    public class OutPut
    {
        //IncidentId, AcknowledgeContactAlias, AcknowledgeDate
        public string IncidentId { get; set; }
        public string Title { get; set; }
    }

    class IcMComparer : IEqualityComparer<OutPut>
    {
        public bool Equals(OutPut x, OutPut y)
        {
            return x.IncidentId.Equals(y.IncidentId);
        }

        public int GetHashCode(OutPut obj)
        {
            return obj.IncidentId.GetHashCode();
        }
    }
}
