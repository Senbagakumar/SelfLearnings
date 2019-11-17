using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCards;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;


namespace SaiTempleAssistance
{
	public class TempleTimings : ITimings
	{
        private readonly ICard card;
        public TempleTimings()
        {
            card = new AdaptiveCardDesign("Function Name", "Timings");
        }
		private List<TempleTiming> NormalTimings
		{
			get
			{
                return new List<TempleTiming>()
                {
                    new TempleTiming() { FunctionName="Kakad Aarati", FunctionTimings="08:00 AM - 08:30 AM" },
                    new TempleTiming() { FunctionName="Open for Dharshan", FunctionTimings="08:30 AM - 10:00 AM" },
                    new TempleTiming() { FunctionName="Madhyanna Aarati", FunctionTimings="08:30 AM - 10:00 AM"},
                    new TempleTiming() { FunctionName="Temple Will be Closed", FunctionTimings="01:00 PM - 06:00 PM"},
                    new TempleTiming() { FunctionName="Dhoop Aarati", FunctionTimings="06:30 PM - 07:00 PM"},
                    new TempleTiming() { FunctionName="Saibaba Mandir Open for Dharshan", FunctionTimings="07:00 PM - 08:30 PM"},
                    new TempleTiming() { FunctionName="Shej Aarati", FunctionTimings="08:30 PM - 09:00 PM"},
                    new TempleTiming() { FunctionName="Temple Will be Closed", FunctionTimings="09:15 PM"}
                };
			}
		}
		private List<TempleTiming> SpecialTimings
		{
			get
			{
                return new List<TempleTiming>()
                {
                    new TempleTiming() { FunctionName="Sai Chalisa Chanting", FunctionTimings="12:00 PM - 12:30 PM" },
                    new TempleTiming() { FunctionName="BABA Bhajans & Satcharitra Recital", FunctionTimings="07:00 PM - 08:00 PM" },
                    new TempleTiming() { FunctionName="Panchamrutha Abhishekam to BABA", FunctionTimings="07:15 PM"},
                    new TempleTiming() { FunctionName="Pallaki Seva to BABA", FunctionTimings="08:15 PM - 08:30 PM"}
                };
			}
		}

		public List<TempleTiming> GetSpecialDayTimings()
		{
            var tt = new List<TempleTiming>();
            tt.AddRange(NormalTimings);
            tt.Add(new TempleTiming() { FunctionName="Below are special for thrusday",FunctionTimings=":):):)"});
            tt.AddRange(SpecialTimings);
            return tt;
		}

		public List<TempleTiming> GetTempleTimings()
		{
			return NormalTimings;
		}
       
        public Attachment GetTempleTimingAttachment(List<TempleTiming> timing)
        {
            if(timing.Count > 8)
                card.BoldStyleIndex = 9;
            return card.LoopCardAdaptiveCard(timing);
        }
    }
}
