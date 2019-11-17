using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace SaiTempleAssistance
{
    public interface IServices
    {
        Activity GetServiceInfo(Activity activity);
    }

    public interface ICard
    {
        int BoldStyleIndex { get; set; }
        Attachment LoopCardAdaptiveCard(List<TempleTiming> output);
    }
    public interface ITimings
    {
        List<TempleTiming> GetTempleTimings();
        List<TempleTiming> GetSpecialDayTimings();
        Attachment GetTempleTimingAttachment(List<TempleTiming> timing);
    }

    public interface IPriest
    {
        Attachment GetAllPriestServices();

    }

    public class TempleTiming
    {
        public string FunctionName { get; set; }
        public string FunctionTimings { get; set; }
    }

    public class PriestService
    {
        public string ServiceName { get; set; }
        public string ServicePrice { get; set; }
    }

}