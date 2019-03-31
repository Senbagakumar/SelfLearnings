using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public abstract class FieldRule
    {
        protected string _Default = "false";
        public abstract string Render();
    }
    public class ExternalLightboxControl : ExternalLightboxForm
    {
        public override string Render(StringBuilder beforeInit, string parent)
        {
            List<string> stringList = new List<string>();
            stringList.Add("view:'" + this.View + "'");
            if (!this._properties.IsNullOrDefault("width"))
                stringList.Add("width:" + this.Width.ToString());
            if (!this._properties.IsNullOrDefault("height"))
                stringList.Add("height:" + this.Height.ToString());
            if (!this._properties.IsNullOrDefault("ClassName"))
                stringList.Add("classname:'" + this.ClassName + "'");
            stringList.Add("type:'external'");
            return string.Format("\n\tscheduler.initCustomLightbox({{\n\t{1}\n\t}}, {0}, \"{0}\");", (object)parent, (object)string.Join(",\n\t", stringList.ToArray()));
        }

        public ExternalLightboxControl(string view, int width, int height)
          : base(view, width, height)
        {
        }

        public ExternalLightboxControl(string view, string classname)
          : base(view, classname)
        {
        }

        public ExternalLightboxControl(string view)
          : base(view)
        {
        }
    }
}

