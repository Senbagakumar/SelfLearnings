using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class SchedulerHighlighter
    {
        private bool __CreateOnClick;

        public bool Enabled { get; set; }

        internal SchedulerHighlighter()
        {
            this.Step = 60;
            this.CssClass = "highlighted_timespan";
            this.FixedStep = true;
        }

        public string Html { get; set; }

        public bool FixedStep { get; set; }

        private SchedulerHighlighter.CreateRule AddEvent { get; set; }

        public bool CreateOnClick
        {
            get
            {
                return this.__CreateOnClick;
            }
            set
            {
                this.__CreateOnClick = value;
                if (value)
                    this.AddEvent = SchedulerHighlighter.CreateRule.OnClick;
                else
                    this.AddEvent = SchedulerHighlighter.CreateRule.Default;
            }
        }

        public int Step { get; set; }

        public string CssClass { get; set; }

        public void Enable(string css, int step)
        {
            this.Enable(css);
            this.Step = step;
        }

        private string FixTime()
        {
            return "var marked = null;\r\n            var marked_date = null; \r\n            var fix_date = function(date) {\r\n            date = new Date(date);\r\n            if (date.getMinutes() > 30)\r\n            date.setMinutes(30);\r\n            else\r\n            date.setMinutes(0);\r\n            date.setSeconds(0);\r\n            return date;\r\n        };";
        }

        private string _CreateOnClick(string parent)
        {
            if (this.AddEvent == SchedulerHighlighter.CreateRule.Default)
                return "";
            string str = "";
            if (this.AddEvent == SchedulerHighlighter.CreateRule.OnDoubleClick)
                str = string.Format("{0}.attachEvent(\"onDdlClick\", function(id){{if(!id) return false;}});\r\n            ", (object)parent);
            return string.Format("{3}{0}.attachEvent(\"onEmptyClick\", function(date, e){{\r\n            {1}\r\n            {0}.unmarkTimespan(marked);\r\n            marked = null;\r\n            var fixed_date = fix_date(date);\r\n            {0}.addEventNow(fixed_date, {0}.date.add(fixed_date, event_step, \"minute\"));\r\n            {2}\r\n        }});", (object)parent, this.AddEvent == SchedulerHighlighter.CreateRule.OnDoubleClick ? (object)string.Format("if({0}._lightbox_id)\r\n            return;\r\n            if(!{0}._tmp_dblClick){{\r\n                {0}._tmp_dblClick = 1;\r\n                setTimeout(function(){{delete {0}._tmp_dblClick;}}, 400);\r\n            }}else{{\r\n                delete {0}._tmp_dblClick;", (object)parent) : (object)"", this.AddEvent == SchedulerHighlighter.CreateRule.OnDoubleClick ? (object)"}" : (object)"", (object)str);
        }

        private string MouseMove(string parent)
        {
            if (this.FixedStep)
                return "";
            return string.Format("{0}.attachEvent(\"onMouseMove\", function(event_id, native_event) {{\r\n            var date = {0}.getActionData(native_event).date;\r\n            var fixed_date = fix_date(date);\r\n            if (+fixed_date != +marked_date) {{\r\n                {0}.unmarkTimespan(marked);\r\n                marked_date = fixed_date;\r\n                marked = {0}.markTimespan({{\r\n                    start_date: fixed_date,\r\n                    end_date: {0}.date.add(fixed_date, event_step, \"minute\"),\r\n                    css: \"{1}\"\r\n                }});\r\n            }}\r\n        }});", (object)parent, (object)this.CssClass);
        }

        public void Enable(string css)
        {
            this.Enabled = true;
            this.CssClass = css;
        }

        private string RenderFixedStep(string parent)
        {
            if (this.FixedStep)
                return string.Format("\r\n        var highlight_html = \"\";\r\n        var hours = {0}.config.last_hour - {0}.config.first_hour;\r\n        var times = hours*60/event_step;\r\n        var height = {0}.config.hour_size_px*(event_step/60);\r\n        for (var i=0; i<times; i++) {{\r\n            highlight_html += \"<div class='{1}' style='height: \"+height+\"px;'>{2}</div>\"\r\n        }}\r\n        {0}.addMarkedTimespan({{\r\n            days: \"fullweek\",\r\n            zones: \"fullday\",\r\n            html: highlight_html\r\n        }});\r\n", (object)parent, (object)this.CssClass, (object)this.Html);
            return "";
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (!this.Enabled)
                return;
            string format = "\r\n    {0}.attachEvent(\"onTemplatesReady\", function() {{\r\n        {3}\r\n        var event_step = {1}; \r\n        {2}\r\n        {4}       \r\n    }});";
            if (this.FixedStep)
                builder.Append(string.Format(format, (object)parent, (object)this.Step, (object)this.RenderFixedStep(parent), this.AddEvent != SchedulerHighlighter.CreateRule.Default ? (object)this.FixTime() : (object)"", (object)this._CreateOnClick(parent)));
            else
                builder.Append(string.Format(format, (object)parent, (object)this.Step, (object)this.MouseMove(parent), (object)this.FixTime(), (object)this._CreateOnClick(parent)));
        }

        private enum CreateRule
        {
            Default,
            OnClick,
            OnDoubleClick,
        }
    }
}

