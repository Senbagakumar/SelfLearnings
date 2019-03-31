using DHTMLX.Scheduler.Settings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace DHTMLX.Scheduler.Data.Loaders
{
    public class AjaxUrl : SchedulerSettingsBase
    {
        public AjaxUrl()
        {
        }

        public AjaxUrl(string url)
          : this()
        {
            this.Action = url;
        }

        public AjaxUrl(string controller, string action)
          : this()
        {
            this.Controller = controller;
            this.Action = action;
        }

        public string Action
        {
            get
            {
                return (string)this._properties["action"];
            }
            set
            {
                this._properties["action"] = (object)value;
            }
        }

        public string Controller
        {
            get
            {
                return (string)this._properties["controller"];
            }
            set
            {
                this._properties["controller"] = (object)value;
            }
        }

        public virtual string RequestUrl
        {
            get
            {
                return this.Url + (this.Parameters == null || this.Parameters.Count == 0 ? "?" : "&");
            }
        }

        public virtual string Url
        {
            get
            {
                if (this.Controller != "undefined" && this.Controller != null)
                    return string.Format("/{0}/{1}{2}", (object)this.Controller, (object)this.Action, (object)this.ParametersString);
                return string.Format("{0}{1}", (object)this.Action, (object)this.ParametersString);
            }
        }

        public string ParametersString
        {
            get
            {
                if (this._properties.IsNullOrDefault("parameters"))
                    return "";
                string str = "";
                List<string> stringList = new List<string>();
                foreach (string key in this.Parameters.Keys)
                    stringList.Add(key + "=" + this.Parameters[key].ToString());
                if (stringList.Count > 0)
                    str = "?" + string.Join("&", stringList.ToArray());
                return str;
            }
        }

        public void AddParameters(Dictionary<string, object> param)
        {
            if (this._properties.IsNullOrDefault("parameters"))
                this.Parameters = new Dictionary<string, object>();
            foreach (string key in this.Parameters.Keys)
                this.Parameters.Add(key, this.Parameters[key]);
        }

        public void AddParameters(NameValueCollection param)
        {
            if (this._properties.IsNullOrDefault("parameters"))
                this.Parameters = new Dictionary<string, object>();
            foreach (string key in param.Keys)
                this.Parameters.Add(key, (object)param[key]);
        }

        public void AddParameter(string name, object value)
        {
            if (this._properties.IsNullOrDefault("parameters"))
                this.Parameters = new Dictionary<string, object>();
            this.Parameters.Add(name, value);
        }

        public Dictionary<string, object> Parameters
        {
            get
            {
                return this._properties["parameters"] as Dictionary<string, object>;
            }
            set
            {
                this._properties["parameters"] = (object)value;
            }
        }
    }
}
