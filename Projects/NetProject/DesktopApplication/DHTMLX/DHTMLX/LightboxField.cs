using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class LightboxRecurringBlock : LightboxField
    {
        public LightboxRecurringBlock(string name, string label = null)
          : base(name, label)
        {
            this.Type = "recurring";
            this.Button = "recurring";
            this.Name = this.MapTo = "rec_type";
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_recurring.js",
          FileType.Local
        }
      };
        }

        public string Button
        {
            get
            {
                return this.Get("button");
            }
            set
            {
                this.Set("button", (object)value);
            }
        }
    }
    public class LightboxRadio : LightboxSelect
    {
        public LightboxRadio(string name, string label = null)
          : base(name, label)
        {
            this.Type = "radio";
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_editors.js",
          FileType.Local
        }
      };
        }

        public bool Vertical
        {
            get
            {
                return this.GetBool("vertical");
            }
            set
            {
                this.Set("vertical", (object)value);
            }
        }
    }
    public abstract class LightboxItem : SchedulerControlsItem
    {
        public virtual string Render(StringBuilder beforeInit, string parent)
        {
            return new JavaScriptSerializer().Serialize((object)this.GetVisibleProperties());
        }

        public virtual string AfterInit()
        {
            return "";
        }
    }

    public class LightboxMultiselect : LightboxSelect
    {
        public LightboxMultiselect(string name, string label = null)
          : base(name, label)
        {
            this.Type = "multiselect";
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_multiselect.js",
          FileType.Local
        }
      };
        }

        public string ScriptUrl
        {
            get
            {
                return this.Get("script_url");
            }
            set
            {
                this.Set("script_url", (object)value);
            }
        }

        public bool Vertical
        {
            get
            {
                return this.GetBool("vertical");
            }
            set
            {
                this.Set("vertical", (object)value);
            }
        }
    }
    public class LightboxMiniCalendar : LightboxField
    {
        public LightboxMiniCalendar(string name, string label = null)
          : base(name, label)
        {
            this.Type = "calendar_time";
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_minical.js",
          FileType.Local
        }
      };
        }
    }
    public class LightboxField : LightboxItem
    {
        public LightboxField(string name, string label = null)
        {
            this.MapTo = name;
            this._hiddenProperties.Add(nameof(label));
            this.Name = name;
            if (label != null)
                this.Label = label;
            else
                this.Label = name;
        }

        public string MapTo
        {
            get
            {
                return this.Get("map_to");
            }
            set
            {
                this.Set("map_to", (object)value);
            }
        }

        public string Label
        {
            get
            {
                return this.Get("label");
            }
            set
            {
                this.Set("label", (object)value);
            }
        }

        public string Type
        {
            get
            {
                return this.Get("type");
            }
            set
            {
                this.Set("type", (object)value);
            }
        }

        public string Name
        {
            get
            {
                return this.Get("name");
            }
            set
            {
                this.Set("name", (object)value);
            }
        }

        public int Height
        {
            get
            {
                return this.GetInt("height");
            }
            set
            {
                this.Set("height", (object)value);
            }
        }

        public virtual string RenderLabel(string parent)
        {
            if (this.Label != null && this.Name != null)
                return string.Format("\n{0}.locale.labels.section_{1} = '{2}';", (object)parent, (object)this.Name, (object)this.Label);
            return "";
        }
    }
}

