// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using Kusto.Data;
using Kusto.Data.Common;
using Kusto.Data.Net.Client;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EchoBot
{
    public class OutPut
    {
        //IncidentId, AcknowledgeContactAlias, AcknowledgeDate
        public string IncidentId { get; set; }
        public string Title { get; set; }
    }

    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class EchoBotBot : IBot
    {
        private readonly EchoBotAccessors _accessors;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="conversationState">The managed conversation state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public EchoBotBot(ConversationState conversationState, ILoggerFactory loggerFactory)
        {
            if (conversationState == null)
            {
                throw new System.ArgumentNullException(nameof(conversationState));
            }

            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _accessors = new EchoBotAccessors(conversationState)
            {
                CounterState = conversationState.CreateProperty<CounterState>(EchoBotAccessors.CounterStateName),
            };

            _logger = loggerFactory.CreateLogger<EchoBotBot>();
            _logger.LogTrace("Turn start.");
        }

        private Attachment CreateAdaptiveCardAttachment()
        {
            string[] paths = { ".", "Welcome", "Resources", "welcomeCard.json" };
            string fullPath = Path.Combine(paths);
            var adaptiveCard = File.ReadAllText(fullPath);
            return new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCard),
            };
        }

        private Attachment LoopCardAdaptiveCard(List<OutPut> output)
        {
            var columns = new List<Column>();
            var icmIds = new List<CardElement>();
            var icmAckDates = new List<CardElement>();
            var icmAlias = new List<CardElement>();

            foreach(var oput in output)
            {
                icmIds.Add(new TextBlock() { Text = oput.IncidentId, Separation = SeparationStyle.Strong, Size=TextSize.Small });
                icmAckDates.Add(new TextBlock() { Text = oput.Title, Separation = SeparationStyle.Strong, Size = TextSize.Small });
                //icmAlias.Add(new TextBlock() { Text = oput.AcknowledgeContactAlias, Separation = SeparationStyle.Strong });
            }

            (icmIds[0] as TextBlock).Weight = TextWeight.Bolder;
            (icmAckDates[0] as TextBlock).Weight = TextWeight.Bolder;
            //(icmAlias[0] as TextBlock).Weight = TextWeight.Bolder;

            for (int i=0; i<2; i++)
            {
                var column = new Column() { Style = ContainerStyle.Normal };
                if (i == 0) { column.Items = icmIds; }
                if (i == 1) { column.Items = icmAckDates; }
                //if (i == 2) { column.Items = icmAlias; }
                columns.Add(column);
            }

            

            var container = new Container() { Items=new List<CardElement>(){ new ColumnSet() { Columns=columns }  } };
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = new AdaptiveCard() { Body = new List<CardElement>() { container } }
            };

        }

        private Attachment CreateCardAdaptiveCard()
        {
            var acard = new AdaptiveCard();
            acard.Body = new List<CardElement>()
            {
                new Container()
                {
                    Items=new List<CardElement>()
                    {
                        new ColumnSet()
                        {
                            Columns = new List<Column>()
                            {                                
                                new Column()
                                {
                                    Size=ColumnSize.Auto,
                                    Style=ContainerStyle.Emphasis,
                                    Items=new List<CardElement>()
                                    {
                                        new TextBlock()
                                        {
                                            Text="Name",
                                            Weight=TextWeight.Bolder,
                                            Separation=SeparationStyle.Strong
                                        },
                                        new TextBlock()
                                        {
                                            Text="Senba",                                            
                                            Separation=SeparationStyle.Strong
                                        },
                                        new TextBlock()
                                        {
                                            Text="Aadithi",
                                            Separation=SeparationStyle.Strong
                                        }
                                    }
                                },
                                new Column()
                                {
                                    Size=ColumnSize.Auto,
                                    Style=ContainerStyle.Emphasis,
                                    Items=new List<CardElement>()
                                    {
                                        new TextBlock()
                                        {
                                            Text="Age",
                                            Weight=TextWeight.Bolder
                                        },
                                        new TextBlock()
                                        {
                                            Text="35",
                                            Separation=SeparationStyle.Strong
                                        },
                                        new TextBlock()
                                        {
                                            Text="6",
                                            Separation=SeparationStyle.Strong
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = acard
            };            
        }

        private Microsoft.Bot.Schema.Activity CreateResponse(Microsoft.Bot.Schema.Activity activity, Attachment attachment)
        {
            var response = activity.CreateReply();
            response.Attachments = new List<Attachment>() { attachment };
            return response;
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
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
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Get the conversation state from the turn context.
                var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

                // Bump the turn count for this conversation.
                state.TurnCount++;

                // Set the property using the accessor.
                await _accessors.CounterState.SetAsync(turnContext, state);

                // Save the new turn count into the conversation state.
                await _accessors.ConversationState.SaveChangesAsync(turnContext);

                var list = new List<OutPut>();
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //Assuming that the api takes the user message as a query paramater
                        //http://localhost:15098/api/values/?severity=3&owningTeamName=SQLDBperfv-queue
                        string RequestURI = @"http://localhost:15098/Api/values/?severity=3&owningTeamName=SQLDBperfv-queue";
                        HttpResponseMessage responsemMsg = await client.GetAsync(RequestURI);
                        if (responsemMsg.IsSuccessStatusCode)
                        {
                            var apiResponse = await responsemMsg.Content.ReadAsStringAsync();
                            list = JsonConvert.DeserializeObject<List<OutPut>>(apiResponse);
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
                var response = CreateResponse(turnContext.Activity, welcomeCard);
                await turnContext.SendActivityAsync(response);

                ////try
                ////{
                ////    //https://stackoverflow.com/questions/53397728/kusto-query-from-c-sharp-- Package Name: Microsoft.Azure.Kusto.Data.NETStandard
                ////    var kcsb = new KustoConnectionStringBuilder("https://icmcluster.kusto.windows.net", "IcmDataWarehouse").WithKustoBasicAuthentication("Redmond\\v-sesiga", "Myapps@123");//WithAadUserPromptAuthentication()
                ////    using (var queryProvider = KustoClientFactory.CreateCslQueryProvider(kcsb))
                ////    {
                ////        // The query -- Note that for demonstration purposes, we send a query that asks for two different
                ////        // result sets (HowManyRecords and SampleRecords).
                ////        var query = "StormEvents | count | as HowManyRecords; StormEvents | limit 10 | project StartTime, EventType, State | as SampleRecords";

                ////        // It is strongly recommended that each request has its own unique
                ////        // request identifier. This is mandatory for some scenarios (such as cancelling queries)
                ////        // and will make troubleshooting easier in others.

                ////        var clientRequestProperties = new ClientRequestProperties() { ClientRequestId = Guid.NewGuid().ToString() };
                ////        using (var reader = queryProvider.ExecuteQuery(query, clientRequestProperties))
                ////        {

                ////        }
                ////    }

                ////    // var client = KustoClientFactory.CreateCslQueryProvider("Data Source=https://icmcluster.kusto.windows.net;Initial Catalog=IcmDataWarehouse;AAD Federated Security=True;dSTS Federated Security=False");

                ////    //  var reader = client.ExecuteQuery(kustoQuery);
                ////}
                ////catch (System.Exception ex)
                ////{

                ////    throw;
                ////}

                // Echo back to the user whatever they typed.
                //var responseMessage = $"Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
                var responseMessage = $"The above is the list of the IcM based on your selecteion criteria";
                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {
                if (turnContext.Activity.MembersAdded != null)
                {                
                    // Iterate over all new members added to the conversation.
                    //foreach (var member in turnContext.Activity.MembersAdded)
                    //{
                    //    // Greet anyone that was not the target (recipient) of this message.
                    //    // To learn more about Adaptive Cards,
                    //    // See https://aka.ms/msbot-adaptivecards for more details.
                    //    if (member.Id != turnContext.Activity.Recipient.Id)
                    //    {
                    //        //var welcomeCard = CreateAdaptiveCardAttachment();
                    //        //var welcomeCard = CreateCardAdaptiveCard();

                    //        //var lists = new List<OutPut>();
                    //        //var output = new OutPut();
                    //        //output.IncidentId = "IncidentId";
                    //        //output.AcknowledgeContactAlias = "Alias";
                    //        //output.AcknowledgeDate="AcknowledgeDate";
                    //        //lists.Add(output);

                    //        //var output1 = new OutPut();
                    //        //output1.IncidentId = "101";
                    //        //output1.AcknowledgeContactAlias = "v-sesiga";
                    //        //output1.AcknowledgeDate = DateTime.Now.ToShortDateString();
                    //        //lists.Add(output1);

                    //        //var output2 = new OutPut();
                    //        //output2.IncidentId = "102";
                    //        //output2.AcknowledgeContactAlias = "v-harpa";
                    //        //output2.AcknowledgeDate = DateTime.Now.AddDays(-1).ToShortDateString();
                    //        //lists.Add(output2);

                    //        //var welcomeCard = LoopCardAdaptiveCard(lists);
                    //        //var response = CreateResponse(turnContext.Activity, welcomeCard);
                    //        //await turnContext.SendActivityAsync(response);
                    //    }
                    //}
                }
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            }
        }
    }
}
