using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class MiniCalendar : SchedulerControlsItem
    {
        public MiniCalendar()
        {
            this._hiddenProperties.Add("isAttached");
            this._hiddenProperties.Add("handler");
            this.Handler = "function(date,calendar){{\r\n               {0}.setCurrentView(date);\r\n               {0}.destroyCalendar()\r\n            }}";
            this.Navigation = true;
            this.IconClass = "dhx_minical_icon";
        }

        public MiniCalendar(string container)
          : this()
        {
            this.Container = container;
        }

        public override Dictionary<string, FileType> GetJS()
        {
            Dictionary<string, FileType> js = base.GetJS();
            js.Add("ext/dhtmlxscheduler_minical.js", FileType.Local);
            return js;
        }

        public void AttachToHeader(string position)
        {
            this.IsAttachedToHeader = true;
            if (string.IsNullOrEmpty(position))
                return;
            this.Position = position;
        }

        public string Handler
        {
            get
            {
                return this.Get("handler");
            }
            protected set
            {
                this.Set("handler", (object)value);
            }
        }

        public bool IsAttachedToHeader
        {
            get
            {
                return this.GetBool("isAttached");
            }
            protected set
            {
                this.Set("isAttached", (object)value);
            }
        }

        public string Position
        {
            get
            {
                return this.Get("position");
            }
            set
            {
                this.Set("position", (object)value);
            }
        }

        public string Container
        {
            get
            {
                return this.Get("container");
            }
            set
            {
                this.Set("container", (object)value);
            }
        }

        public DateTime Date
        {
            get
            {
                return (DateTime)this._properties["date"];
            }
            set
            {
                this.Set("date", (object)value);
            }
        }

        public string IconClass { get; set; }

        public bool Navigation
        {
            get
            {
                return this.GetBool("navigation");
            }
            set
            {
                this.Set("navigation", (object)value);
            }
        }
    }
}

