// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace OSEngBot
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
	public partial class OSEngBot : IBot
	{
		private readonly BotAccessor _botAccessor;
		private readonly DialogSet _dialogSet;

		private const string IcMNumberPrompt = "prompt-IcMNumber";
		private const string IcMTsgId = "prompt-IcMTsgId";
		private const string TopLevelDialog = "prompt-topLevel";

		private const string IcMInfo = "IcMInfo";
		private const string IcMcontinuePrompt = "IcMContinuePrompt";

	   private readonly string[] _ContinueProcess = new string[]
	   {
			"Yes", "No"
	   };

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>                        
		public OSEngBot(BotAccessor botAccessor)
		{
			_botAccessor = botAccessor ?? throw new ArgumentNullException(nameof(botAccessor));
			_dialogSet = new DialogSet(_botAccessor.DialogStateAccessor);


			// Add the prompts we need to the dialog set.
			_dialogSet
				.Add(new TextPrompt(IcMNumberPrompt))
				.Add(new TextPrompt(IcMTsgId))
				.Add(new ChoicePrompt(IcMcontinuePrompt));


			_dialogSet.Add(new WaterfallDialog(TopLevelDialog)
				.AddStep(IcMNumberPromptStepAsync)
				.AddStep(IcMTsgIdPromptStepAsync)
				.AddStep(AcknowledgementStepAsync));
				//.AddStep(ContinueStepAsync));
		}

		private async Task<DialogTurnResult> ContinueStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
		{
			var IsContinue = stepContext.Context.Activity.Text;
			if (IsContinue == "Yes")
				return await stepContext.ReplaceDialogAsync("IcMNumberPromptStepAsync",null,cancellationToken);
			else
				return await stepContext.EndDialogAsync((IcMProfile)stepContext.Values[IcMInfo]);
		}

		private async Task<DialogTurnResult> AcknowledgementStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
		{
			var icmProfile = (IcMProfile)stepContext.Values[IcMInfo];

			icmProfile.IcMTsgId = stepContext.Context.Activity.Text;

			await stepContext.Context.SendActivityAsync("Valid TSG, We are working on that");

			var response = string.Empty;
			response = "Self-Healed";
			//try
			//{
			//    using (HttpClient client = new HttpClient())
			//    {
			//        //Assuming that the api takes the user message as a query paramater
			//        //http://localhost:15098/api/values/?severity=3&owningTeamName=SQLDBperfv-queue
			//        string RequestURI = $@"http://localhost:15098/Api/values/?IcMId={icmProfile.IcMNumber}&IcMTsgId={icmProfile.IcMTsgId}";
			//        HttpResponseMessage responsemMsg = await client.GetAsync(RequestURI);
			//        //await stepContext.Context.SendActivityAsync("Please Wait, it takes some time to get the result");
			//        if (responsemMsg.IsSuccessStatusCode)
			//        {
			//            response = await responsemMsg.Content.ReadAsStringAsync();
						await stepContext.Context.SendActivityAsync(response);
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    await stepContext.Context.SendActivityAsync("Sorry, it looks like something went wrong. Please contact the Help team");
			//}
			return await stepContext.EndDialogAsync((IcMProfile)stepContext.Values[IcMInfo]);
			//return await stepContext.PromptAsync(
			//    IcMcontinuePrompt,
			//    new PromptOptions
			//    {
			//        Prompt = MessageFactory.Text("Do you want to Continue?"),
			//        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
			//        Choices = ChoiceFactory.ToChoices(_ContinueProcess.ToList()),
			//    },
			//    cancellationToken);
		}

		private async Task<DialogTurnResult> IcMTsgIdPromptStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
		{
			var icmProfile = (IcMProfile)stepContext.Values[IcMInfo];
			icmProfile.IcMNumber = stepContext.Context.Activity.Text;

			return await stepContext.PromptAsync(
				IcMNumberPrompt,
				new PromptOptions
				{
					Prompt = MessageFactory.Text("Please Enter the \"TSG\" for this IcM."),
					RetryPrompt = MessageFactory.Text("Please Enter the TSG for this IcM.")
				},
				cancellationToken);
		}

		private async Task<DialogTurnResult> IcMNumberPromptStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
		{
			// Create an object in which to collect the user's information within the dialog.
			stepContext.Values[IcMInfo] = new IcMProfile();
			return await stepContext.PromptAsync(
				IcMNumberPrompt,
				new PromptOptions
				{
					Prompt = MessageFactory.Text("Please Enter the IcM number"),
					RetryPrompt = MessageFactory.Text("Please Enter the IcM number.")
				},
				cancellationToken);
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
						await _botAccessor.UserProfileAccessor.SetAsync(turnContext, new IcMProfile(), cancellationToken);
						await _botAccessor.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
						await dialogContext.BeginDialogAsync(TopLevelDialog, null, cancellationToken);
						break;
					case DialogTurnStatus.Complete:
						// If we just finished the dialog, capture and display the results.
						IcMProfile icMInfo = results.Result as IcMProfile;
						string status = "Thanks for the chat";
						await turnContext.SendActivityAsync(status);
						await _botAccessor.UserProfileAccessor.SetAsync(turnContext, icMInfo, cancellationToken);
						await _botAccessor.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
						break;
					case DialogTurnStatus.Waiting:
						// If there is an active dialog, we don't need to do anything here.
						break;
				}

				await _botAccessor.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);

				// Echo back to the user whatever they typed.             
				//await turnContext.SendActivityAsync("Hello World", cancellationToken: cancellationToken);
			}
			if(turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
			{
				await turnContext.SendActivityAsync("Welcome to Online Support Engineer Bots", cancellationToken: cancellationToken);
			}
		}
	}
}
