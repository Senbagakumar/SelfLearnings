using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    //For 1st and 2nd Json Format
    public class TimeAndCapacity
    {
        public string time { get; set; }
        public int minInstanceCount { get; set; }
        public int maxInstanceCount { get; set; }
    }

    public class Schedule
    {
        public List<string> days { get; set; }
        public TimeAndCapacity timeAndCapacity { get; set; }
    }

    public class Recurrence
    {
        public string timeZone { get; set; }
        public List<Schedule> schedule { get; set; }
    }

    public class Autoscale
    {
        public Recurrence recurrence { get; set; }
    }

    public class RootObject
    {
        public Autoscale autoscale { get; set; }
    }

    // for 3 and 4 JsonFormat
    public class Capacity
    {
        public int minInstanceCount { get; set; }
        public int maxInstanceCount { get; set; }
    }

    public class CapacityAutoscale
    {
        public Capacity capacity { get; set; }
    }

    public class CapactityRootObject
    {
        public CapacityAutoscale autoscale { get; set; }
    }


    public class JsonConversion
    {
        public static T JsonToObject<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string ReplaceJson(string input)
        {
            input = input.Replace("Auto scale is enabled, AutoScale Configuration:", "");
            input = input.Replace("Autoscale configuration request:", "");
            input = input.Trim();
            return input;
        }
        public static Recurrence JsonCasesRecurrence(string input)
        {
            Recurrence rec = null;
            input = ReplaceJson(input);
            if(input.Contains("autoscale") && input.Contains("recurrence"))
            {
                RootObject root = JsonToObject<RootObject>(input);
                rec = root.autoscale.recurrence;

            }
            else if(input.Contains("recurrence"))
            {
                Autoscale autoScale = JsonToObject<Autoscale>(input);
                rec = autoScale.recurrence;
            }
            return rec;
        }

        public static Capacity JsonCasesAutoScale(string input)
        {
            Capacity capacity = null;
            input = ReplaceJson(input);
            if (input.Contains("autoscale") && input.Contains("capacity"))
            {
                CapactityRootObject cr = JsonToObject<CapactityRootObject>(input);
                return cr.autoscale.capacity;
            }
            else if (input.Contains("capacity"))
            {
                CapacityAutoscale ca = JsonToObject<CapacityAutoscale>(input);
                return ca.capacity;
            }
            return capacity;
        }



        public static string JsonTest(string input1)
        {
            string result = string.Empty;
            Recurrence rec = JsonCasesRecurrence(input1);
            string timeZone = rec.timeZone;
            if (rec != null)
            {
                string Template = "(ScheduleTime= {0}, ScheduleWeekDays= {1}, MinInstanceCount={2}, MaxInstanceCount={3})";
                foreach (Schedule schedule in rec.schedule)
                {
                    string days = string.Join(",", schedule.days.ToArray());
                    int mincount = schedule.timeAndCapacity.minInstanceCount;
                    int maxcount = schedule.timeAndCapacity.maxInstanceCount;
                    string stime = schedule.timeAndCapacity.time;
                    if (!string.IsNullOrWhiteSpace(result))
                        result = result + ";" + string.Format(Template, stime, days, mincount, maxcount);
                    else
                        result = string.Format(Template, stime, days, mincount, maxcount);
                }
            }
            else
            {
                Capacity capacityAutoscale = JsonCasesAutoScale(input1);
                string template = "(minInstanceCount={0},maxInstanceCount = {1})";
                if (capacityAutoscale != null)
                {
                    result = string.Format(template,capacityAutoscale.minInstanceCount,capacityAutoscale.maxInstanceCount);
                }
                else
                    result = string.Empty;
            }
            return result;
        }

        public void JsonTestCases()
        {
            string input1 = @"Auto scale is enabled, AutoScale Configuration: {""recurrence"":{""timeZone"":""UTC"",""schedule"":[{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""1:0"",""minInstanceCount"":4,""maxInstanceCount"":4}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""2:0"",""minInstanceCount"":5,""maxInstanceCount"":5}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""3:0"",""minInstanceCount"":6,""maxInstanceCount"":6}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""4:0"",""minInstanceCount"":7,""maxInstanceCount"":7}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""5:0"",""minInstanceCount"":4,""maxInstanceCount"":4}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""6:0"",""minInstanceCount"":5,""maxInstanceCount"":5}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday"",""Friday"",""Saturday"",""Sunday""],""timeAndCapacity"":{""time"":""8:0"",""minInstanceCount"":6,""maxInstanceCount"":6}}]}}";
            string input2 = @"Autoscale configuration request: {""autoscale"":{""recurrence"":{""timeZone"":""Pacific Standard Time"",""schedule"":[{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday""],""timeAndCapacity"":{""time"":""06:00"",""minInstanceCount"":44,""maxInstanceCount"":44}},{""days"":[""Monday"",""Tuesday"",""Wednesday"",""Thursday""],""timeAndCapacity"":{""time"":""18:00"",""minInstanceCount"":16,""maxInstanceCount"":16}},{""days"":[""Friday""],""timeAndCapacity"":{""time"":""06:00"",""minInstanceCount"":44,""maxInstanceCount"":44}},{""days"":[""Friday""],""timeAndCapacity"":{""time"":""18:00"",""minInstanceCount"":4,""maxInstanceCount"":4}}]}}}";
            string input3 = @"Auto scale is enabled, AutoScale Configuration: {""capacity"":{""minInstanceCount"":1,""maxInstanceCount"":2}}";
            string input4 = @"Autoscale configuration request: {""autoscale"":{""capacity"":{""minInstanceCount"":3,""maxInstanceCount"":8}}}";
            string input5 = @"Autoscale configuration request: {}";
            var test1 = JsonTest(input1);
            var test2 = JsonTest(input2);
            var test3 = JsonTest(input3);
            var test4 = JsonTest(input4);
            var test5 = JsonTest(input5);
        }
    }
}
