using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class Zone
    {
        public int Start;
        public int End;

        public Zone()
        {
        }

        public Zone(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", (object)this.Start, (object)this.End);
        }
    }
    public enum TimeUnits
    {
        Default,
        Year,
        Month,
        Week,
        Day,
        Hour,
        Minute,
    }
    public class Section
    {
        public string View { get; set; }

        public IList<string> Units { get; set; }

        public Section()
        {
            this.Units = (IList<string>)new List<string>();
        }

        public Section(string view, IList<string> units)
        {
            this.View = view;
            this.Units = units;
        }

        public override string ToString()
        {
            return string.Format("\"{0}\":[{1}]", (object)this.View, (object)string.Join(", ", this.Units.Select<string, string>((Func<string, string>)(u => "\"" + u + "\"")).ToArray<string>()));
        }
    }

    public class SchedulerTimeSpanMarker
    {
        public SchedulerTimeSpanMarker()
        {
            this.Items = new List<DHXTimeSpan>();
        }

        public List<DHXTimeSpan> Items { get; set; }

        public DHXMarkTime Mark(DateTime from, DateTime to, string css, string html)
        {
            DHXMarkTime dhxMarkTime1 = new DHXMarkTime();
            dhxMarkTime1.StartDate = from;
            dhxMarkTime1.EndDate = to;
            dhxMarkTime1.CssClass = css;
            dhxMarkTime1.HTML = html;
            DHXMarkTime dhxMarkTime2 = dhxMarkTime1;
            this.Items.Add((DHXTimeSpan)dhxMarkTime2);
            return dhxMarkTime2;
        }

        public DHXTimeSpan Mark(DateTime from, DateTime to, string css)
        {
            return (DHXTimeSpan)this.Mark(from, to, css, (string)null);
        }

        public DHXTimeSpan Mark(DateTime from, DateTime to)
        {
            return (DHXTimeSpan)this.Mark(from, to, (string)null, (string)null);
        }

        public DHXTimeSpan Add(DHXTimeSpan timespan)
        {
            this.Items.Add(timespan);
            return timespan;
        }

        public DHXMarkTime Mark(DayOfWeek day, params Zone[] zones)
        {
            DHXMarkTime dhxMarkTime1 = new DHXMarkTime();
            dhxMarkTime1.Day = day;
            DHXMarkTime dhxMarkTime2 = dhxMarkTime1;
            if (zones.Length == 0)
            {
                dhxMarkTime2.FullDay = true;
            }
            else
            {
                foreach (Zone zone in zones)
                    dhxMarkTime2.AddZone(zone);
            }
            return dhxMarkTime2;
        }

        public DHXMarkTime Mark(DateTime day)
        {
            DHXMarkTime dhxMarkTime = new DHXMarkTime();
            dhxMarkTime.StartDate = day;
            dhxMarkTime.FullDay = true;
            return dhxMarkTime;
        }

        public DHXMarkTime Mark(DateTime day, params Zone[] zones)
        {
            DHXMarkTime dhxMarkTime1 = new DHXMarkTime();
            dhxMarkTime1.StartDate = day;
            DHXMarkTime dhxMarkTime2 = dhxMarkTime1;
            if (zones.Length == 0)
            {
                dhxMarkTime2.FullDay = true;
            }
            else
            {
                foreach (Zone zone in zones)
                    dhxMarkTime2.AddZone(zone);
            }
            return dhxMarkTime2;
        }

        public DHXMarkTime Mark(DayOfWeek day)
        {
            DHXMarkTime dhxMarkTime = new DHXMarkTime();
            dhxMarkTime.Day = day;
            dhxMarkTime.FullDay = true;
            return dhxMarkTime;
        }

        public void Render(StringBuilder builder, string parent)
        {
            foreach (DHXTimeSpan dhxTimeSpan in this.Items)
                dhxTimeSpan.Render(builder, parent);
        }
    }
}

