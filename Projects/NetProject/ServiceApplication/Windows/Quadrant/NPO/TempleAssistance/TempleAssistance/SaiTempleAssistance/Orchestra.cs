using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiTempleAssistance
{
    public class Orchestra : IServices
    {
        //private readonly IPriest;
        private readonly ITimings timings;
        private readonly IPriest priest;
        public Orchestra()
        {
            timings = new TempleTimings();
            priest = new PriestServices();
        }

        public Activity GetServiceInfo(Activity activity)
        {
            return PrepareMessage(activity);
        }
        private Activity PrepareMessage(Activity activity)
        {
            var requestMsg= activity.Text.Trim().ToLower();
            if (IsTempleMessage(requestMsg))
            {
                var timing = DateTime.UtcNow.DayOfWeek == DayOfWeek.Thursday ? timings.GetSpecialDayTimings() : timings.GetTempleTimings();
                var timingCard = timings.GetTempleTimingAttachment(timing);
                var response = CreateResponse(activity, timingCard);
                return response;
            }
            else if (IsPriestService(requestMsg))
            {
                var serviceCard = priest.GetAllPriestServices();
                var response = CreateResponse(activity, serviceCard);
                return response;
            }
            else
                return null;
        }

        private bool IsTempleMessage(string requestMessage)
        {
            
            if (requestMessage.Trim().Contains("1") ||
               requestMessage.Trim().Contains("timings") ||
               requestMessage.Trim().Contains("timing") ||
               requestMessage.Trim().Contains("temple"))
                    return true;
            else
                return false;
        }

        private bool IsPriestService(string requestMessage)
        {
            if (requestMessage.Trim().Contains("2") ||
              requestMessage.Trim().Contains("priest") ||
               requestMessage.Trim().Contains("services") ||
              requestMessage.Trim().Contains("service"))
                return true;
            else
                return false;
        }


        private Microsoft.Bot.Schema.Activity CreateResponse(Microsoft.Bot.Schema.Activity activity, Attachment attachment)
        {
            var response = activity.CreateReply();
            response.Attachments = new List<Attachment>() { attachment };
            return response;
        }

    }
}
