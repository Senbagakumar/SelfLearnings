using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class LightboxCheckbox : LightboxField
    {
        public LightboxCheckbox(string name, string label = null)
          : base(name, label)
        {
            this.Type = "checkbox";
            this.CheckedValue = (object)true;
            this.UncheckedValue = (object)false;
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

        public object CheckedValue
        {
            get
            {
                return this._properties["checked_value"];
            }
            set
            {
                this.Set("checked_value", value);
            }
        }

        public object UncheckedValue
        {
            get
            {
                return this._properties["unchecked_value"];
            }
            set
            {
                this.Set("unchecked_value", value);
            }
        }
    }
}

