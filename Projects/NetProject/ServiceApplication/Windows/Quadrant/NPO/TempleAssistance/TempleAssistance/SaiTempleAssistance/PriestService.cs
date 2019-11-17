using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiTempleAssistance
{
    public class PriestServices : IPriest
    {
        private readonly ICard card;
        public PriestServices()
        {
            card = new AdaptiveCardDesign("Service Name", "Price");
        }
        private List<TempleTiming> Services
        {
            get
            {
                return new List<TempleTiming>()
                {
                    new TempleTiming() { FunctionName="Sai Archana", FunctionTimings="$11" },
                    new TempleTiming() { FunctionName="Sai Abhishekam", FunctionTimings="$101" },
                    new TempleTiming() { FunctionName="Sai Annadanam", FunctionTimings="$201"},
                    new TempleTiming() { FunctionName="Garland", FunctionTimings="$151"},
                    new TempleTiming() { FunctionName="Vahana puja", FunctionTimings="$51"},
                    new TempleTiming() { FunctionName="Gruha pravesham", FunctionTimings="$251"},
                    new TempleTiming() { FunctionName="Gruha pravesham + Homam + Vratam", FunctionTimings="$301"},
                    new TempleTiming() { FunctionName="Satya Narayana Swami Vratam (At Home)", FunctionTimings="$151"},
                    new TempleTiming() { FunctionName="Nama Karanam (At Home)", FunctionTimings="$125"},
                    new TempleTiming() { FunctionName="Annaprasana (At Home)", FunctionTimings="$125"},
                    new TempleTiming() { FunctionName="Nama Karanam (At Temple)", FunctionTimings="$51"},
                    new TempleTiming() { FunctionName="Annaprasana (At Temple)", FunctionTimings="$51"},
                    new TempleTiming() { FunctionName="Satya Narayana Swami Vratam (At Temple)", FunctionTimings="$101"},
                    new TempleTiming() { FunctionName="Samuhika Vratam", FunctionTimings="$51"},
                    new TempleTiming() { FunctionName="Kalyanam (At Temple)", FunctionTimings="$501"}

                    //new PriestService() { ServiceName="Sai Archana", ServicePrice="$11" },
                    //new PriestService() { ServiceName="Sai Abhishekam", ServicePrice="$101" },
                    //new PriestService() { ServiceName="Sai Annadanam", ServicePrice="$201"},
                    //new PriestService() { ServiceName="Garland", ServicePrice="$151"},
                    //new PriestService() { ServiceName="Vahana puja", ServicePrice="$51"},
                    //new PriestService() { ServiceName="Gruha pravesham", ServicePrice="$251"},
                    //new PriestService() { ServiceName="Gruha pravesham + Homam + Vratam", ServicePrice="$301"},
                    //new PriestService() { ServiceName="Satya Narayana Swami Vratam (At Home)", ServicePrice="$151"},
                    //new PriestService() { ServiceName="Nama Karanam (At Home)", ServicePrice="$125"},
                    //new PriestService() { ServiceName="Annaprasana (At Home)", ServicePrice="$125"},
                    //new PriestService() { ServiceName="Nama Karanam (At Temple)", ServicePrice="$51"},
                    //new PriestService() { ServiceName="Annaprasana (At Temple)", ServicePrice="$51"},
                    //new PriestService() { ServiceName="Satya Narayana Swami Vratam (At Temple)", ServicePrice="$101"},
                    //new PriestService() { ServiceName="Samuhika Vratam", ServicePrice="$51"},
                    //new PriestService() { ServiceName="Kalyanam (At Temple)", ServicePrice="$501"}
                };
            }
        }

        public Attachment GetAllPriestServices()
        {
            return card.LoopCardAdaptiveCard(Services);
        }
    }
}
