using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;


    namespace DHTMLX.Scheduler
    {
        public class DHXMarkTime : DHXTimeSpan
        {
            private string _template = "\n\t{0}.addMarkedTimespan({{{1}}});";

            public DHXMarkTime()
            {
                this._Template = this._template;
            }

            public DHXTimeSpan.Type SpanType { get; set; }

            public string CssClass { get; set; }

            protected override List<string> _GetSettings()
            {
                List<string> settings = base._GetSettings();
                if (!string.IsNullOrEmpty(this.CssClass))
                    settings.Add(string.Format("css:\"{0}\"", (object)this.CssClass.Replace("\"", "\\\"")));
                if (this.SpanType != DHXTimeSpan.Type.Default)
                {
                    string str = this.SpanType != DHXTimeSpan.Type.BlockEvents ? "default" : "dhx_time_block";
                    settings.Add(string.Format("type:\"{0}\"", (object)str));
                }
                return settings;
            }
        }
    }

