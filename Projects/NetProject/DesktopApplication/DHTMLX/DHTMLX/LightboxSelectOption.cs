using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class LightboxTime : LightboxField
    {
        public LightboxTime(string name, string label = null)
          : base(name, label)
        {
            this.Type = "time";
            this.MapTo = "auto";
            this.Name = "time";
        }
    }
    public class LightboxText : LightboxField
    {
        public LightboxText(string name, string label = null)
          : base(name, label)
        {
            this.Type = "textarea";
        }

        public bool Focus
        {
            get
            {
                return this.GetBool("focus");
            }
            set
            {
                this.Set("focus", (object)value);
            }
        }
    }
    public class LightboxSelectOption : LightboxItem
    {
        public LightboxSelectOption(object key, object label)
        {
            this.Set(nameof(key), key);
            this.Set(nameof(label), label);
        }

        public object Key
        {
            get
            {
                return (object)this.Get("key");
            }
        }

        public object Label
        {
            get
            {
                return (object)this.Get("label");
            }
        }
    }
}
