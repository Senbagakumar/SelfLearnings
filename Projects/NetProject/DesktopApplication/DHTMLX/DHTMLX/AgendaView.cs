using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DHTMLX.Scheduler.Controls
{
    public class ExpressionRule : FieldRule
    {
        public string Expression { get; set; }

        public ExpressionRule()
        {
        }

        public ExpressionRule(string expression)
        {
            this.Expression = expression;
        }

        public override string Render()
        {
            if (string.IsNullOrEmpty(this.Expression))
                return this._Default;
            return string.Format("({0})", (object)Regex.Replace(this.Expression, "{([a-zA-Z_0-9]*)}", "event.$1"));
        }
    }
    public class DayView : SchedulerView
    {
        public DayView()
        {
            this.ViewType = "day";
            this.Set("name", (object)"day");
            this.TabPosition = 204;
        }
    }
    public class AgendaView : YearView
    {
        public AgendaView()
        {
            this.ViewType = "agenda";
            this.Set("name", (object)"agenda");
            this.Label = "Agenda";
            this.TabPosition = 280;
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_agenda_view.js",
          FileType.Local
        }
      };
        }

        public TimeUnits TimeFrameUnits { get; set; }

        public int TimeFrameCount { get; set; }

        public void SetTimeFrame(TimeUnits unit, int number)
        {
            this.TimeFrameCount = number;
            this.TimeFrameUnits = unit;
        }

        public void SetTimeFrame(TimeUnits unit)
        {
            this.SetTimeFrame(unit, 1);
        }

        public override void Render(StringBuilder builder, string parent)
        {
            base.Render(builder, parent);
            if (this.TimeFrameUnits == TimeUnits.Default)
                return;
            builder.Append(string.Format("\r\n            {0}.attachEvent(\"onBeforeViewChange\", function(old_mode, old_date, new_mode, new_date) {{\r\n                {0}.config.{1}_start = new Date((new_date||old_date).valueOf());\r\n                {0}.config.{1}_end = {0}.date.add({0}.config.{1}_start, {2}, \"{3}\");\r\n                return true;\r\n            }});", (object)parent, (object)this.Name, (object)this.TimeFrameCount, (object)this.TimeFrameUnits.ToString("g").ToLower()));
        }
    }
}

