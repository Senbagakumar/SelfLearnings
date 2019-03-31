using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class ExternalLightboxForm : LightboxItem
    {
        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "dhtmlxscheduler_custom_lightbox.js",
          FileType.Local
        }
      };
        }

        public int Width
        {
            get
            {
                return this.GetInt("width");
            }
            set
            {
                this.Set("width", (object)value);
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

        public string ClassName
        {
            get
            {
                return this.Get(nameof(ClassName));
            }
            set
            {
                this.Set(nameof(ClassName), (object)value);
            }
        }

        public string View
        {
            get
            {
                return this.Get("view");
            }
            set
            {
                this.Set("view", (object)value);
            }
        }

        protected bool UseSchedulerLightboxBorder
        {
            get
            {
                return this.GetBool("using_border");
            }
            set
            {
                this.Set("using_border", (object)value);
            }
        }

        protected string _clearValues(string context)
        {
            return string.Format("\n\tif({0}.document.getElementsByTagName('form').length == 1)\n\t{{\n\t{0}.document.getElementsByTagName('form')[0].reset();\n\t}}\n\telse{{\n\tvar ev = {0}.getValues();\n\tfor(var i in ev)\n\t\tev[i] = '';\n\t{0}.setValues(ev);\n\t}}\n", (object)context);
        }

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
            return string.Format("\n\tscheduler.initCustomLightbox({{\n\t{1}\n\t}}, {0}, \"{0}\");", (object)parent, (object)string.Join(",\n\t", stringList.ToArray()));
        }

        public ExternalLightboxForm(string view, int width, int height)
          : this(view)
        {
            this.Width = width;
            this.Height = height;
        }

        public ExternalLightboxForm(string view, string classname)
          : this(view)
        {
            this.ClassName = classname;
        }

        public ExternalLightboxForm(string view)
        {
            this.View = view;
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          "using_border",
          (object) true
        }
      });
            this._hiddenProperties.Add("using_border");
        }
    }
}

