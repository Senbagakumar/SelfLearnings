using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot
{
    public class IcMProfile
    {
        public string IcMType { get; set; }
        public string IcMQueue { get; set; }
        public string IcMSeverity { get; set; }

        //The list of companies the user wants to review.
        public List<string> IcMDetails { get; set; } = new List<string>();
    }

    public class ComplexDialogBotOtpions
    {
        public ComplexDialogBotOtpions(ConversationState conversationState, UserState userState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
            UserState = userState ?? throw new ArgumentNullException(nameof(userState));
        }

        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
        public IStatePropertyAccessor<IcMProfile> UserProfileAccessor { get; set; }

        public ConversationState ConversationState { get; }
        public UserState UserState { get; }
    }

    
}
