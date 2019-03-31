using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DHTMLX.Scheduler
{
    public abstract class DHXExtension
    {
        public abstract void Render(StringBuilder builder, DHXScheduler parent);
    }
    public class DHXEventTemplate
    {
        public readonly string EventVarName = "ev";

        public string Template { get; set; }

        public bool CanDrag { get; set; }

        public bool CanResize { get; set; }

        public string CssClass { get; set; }

        public DHXEventTemplate()
        {
            this.CanDrag = true;
            this.CanResize = true;
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (string.IsNullOrEmpty(this.Template))
                return;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("\n{0}.renderEvent = function(container, {1}) {{\n", (object)parent, (object)this.EventVarName));
            stringBuilder.Append("var container_width = container.style.width;\n");
            stringBuilder.Append("var html = \"");
            if (this.CanDrag)
                stringBuilder.Append(string.Format("<div class='dhx_event_move {0} dhx_custom_box_move' style='width: \" + container_width + \"'></div>\";", !string.IsNullOrEmpty(this.CssClass) ? (object)(this.CssClass + "_move") : (object)""));
            string str = Scheduler.Settings.Template.CollectParts("html", Scheduler.Settings.Template.Parse(this.Template.Replace(Environment.NewLine, ""), "\"", new MatchEvaluator(Scheduler.Settings.Template.ReplaceEvent)));
            stringBuilder.Append(str);
            if (this.CanResize)
                stringBuilder.Append(string.Format("html += \"<div class='dhx_event_resize {0} dhx_custom_box_resize' style='width: \" + container_width + \"'></div>\";\n", !string.IsNullOrEmpty(this.CssClass) ? (object)(this.CssClass + "_resize") : (object)""));
            stringBuilder.Append("container.innerHTML = html;return true;\n};");
            builder.Append(stringBuilder.ToString());
        }

        public void RenderCss(StringBuilder builder)
        {
            builder.Append("        /* event's resizing section */\r\n        .dhx_custom_box_resize {\r\n            height: 3px;\r\n            background-color:transparent;\r\n            border:none;\r\n            position: absolute;         \r\n            bottom: -1px;\r\n        }\r\n        /* event's move section */\r\n        .dhx_custom_box_move {\r\n            position: absolute;\r\n            background-color:transparent;\r\n            border:none;\r\n            top: 0;\r\n            height: 10px;\r\n            cursor: pointer;\r\n        }");
        }
    }
}
