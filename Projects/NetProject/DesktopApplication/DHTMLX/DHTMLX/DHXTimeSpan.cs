using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHTMLX.Scheduler
{
    public abstract class DHXTimeSpan
    {
        private DayOfWeek _Day;
        protected string _Template;

        protected bool DaySet { get; set; }

        public DHXTimeSpan()
        {
            this.Zones = new List<Zone>();
            this.Sections = new List<Section>();
        }

        public DateTime StartDate { get; set; }

        public DayOfWeek Day
        {
            get
            {
                return this._Day;
            }
            set
            {
                this._Day = value;
                this.DaySet = true;
            }
        }

        public void AddZone(int start, int end)
        {
            this.Zones.Add(new Zone() { Start = start, End = end });
        }

        public void AddZone(Zone zone)
        {
            this.Zones.Add(zone);
        }

        public void AddSection(string view, IList<string> units)
        {
            this.Sections.Add(new Section()
            {
                View = view,
                Units = units
            });
        }

        public void AddSection(Section section)
        {
            this.Sections.Add(section);
        }

        public List<Zone> Zones { get; set; }

        public List<Section> Sections { get; set; }

        public bool FullDay { get; set; }

        public bool FullWeek { get; set; }

        public DateTime EndDate { get; set; }

        public string HTML { get; set; }

        protected virtual List<string> _GetSettings()
        {
            List<string> stringList = new List<string>();
            if (!this.DaySet && !this.FullWeek)
            {
                stringList.Add("start_date:" + DateFormatHelper.DateToJS(this.StartDate, true));
                if (this.EndDate != new DateTime())
                    stringList.Add("end_date:" + DateFormatHelper.DateToJS(this.EndDate, true));
            }
            if (!string.IsNullOrEmpty(this.HTML))
                stringList.Add(string.Format("html:\"{0}\"", (object)this.HTML.Replace("\"", "\\\"")));
            if (this.FullWeek)
                stringList.Add("days:\"fullweek\"");
            else if (this.DaySet)
                stringList.Add(string.Format("days:{0}", (object)this.Day));
            if (this.Sections.Count > 0)
                stringList.Add(string.Format("sections:{{{0}}}", (object)string.Join(",", this.Sections.Select<Section, string>((Func<Section, string>)(s => s.ToString())).ToArray<string>())));
            if (this.Zones.Count > 0 && !this.FullDay)
                stringList.Add(string.Format("zones:[{0}]", (object)string.Join(",", this.Zones.Select<Zone, string>((Func<Zone, string>)(z => z.ToString())).ToArray<string>())));
            else if (this.EndDate == new DateTime())
                stringList.Add("zones:\"fullday\"");
            return stringList;
        }

        public void Render(StringBuilder builder, string parent)
        {
            string str = string.Join(",", this._GetSettings().ToArray());
            builder.Append(string.Format(this._Template, (object)parent, (object)str));
        }

        public enum Type
        {
            Default,
            BlockEvents,
        }

        public enum ExportColorScheme
        {
            Color,
            FullColor,
            Gray,
            BlackWhite,
        }
    }
}

