using DHTMLX.Common;
using DHTMLX.Scheduler.Data.Loaders;
using DHTMLX.Scheduler.GoogleCalendar;
using DHTMLX.Scheduler.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Web.Mvc;

namespace DHTMLX.Scheduler.Data
{
    public sealed class SchedulerFormResponseScript
    {
        private DataAction action;

        private SchedulerFormResponseScript()
        {
            //TimeCheck.Check();
        }

        public object Event { get; set; }

        public SchedulerFormResponseScript(DataAction action, object ev)
          : this()
        {
            this.action = action;
            this.Event = ev;
        }

        public SchedulerFormResponseScript(bool result)
          : this()
        {
            this.action = new DataAction(DataActionTypes.Update, 1, 1);
        }

        private string serializeEvent(object ev)
        {
            if (ev == null)
                return "{}";
            DHXSchedulerDataStore schedulerDataStore = new DHXSchedulerDataStore();
            schedulerDataStore.Add(ev);
            string str = schedulerDataStore.Render();
            if (str.Length >= 2)
                str = str.Substring(1, str.Length - 2);
            return str;
        }

        public string Render(string parent)
        {
            return string.Format("<html><body style='display:none'>{{\"action\":\"{0}\", sid:\"{1}\", \"data\":{2}}}</body></html>", (object)this.action.Type.ToString("g").ToLower(), (object)this.action.TargetId, (object)this.serializeEvent(this.Event));
        }

        public static implicit operator string(SchedulerFormResponseScript data)
        {
            return data.ToString("scheduler");
        }

        public static implicit operator ContentResult(SchedulerFormResponseScript data)
        {
            return data.ToContentResult("scheduler");
        }
    }
    public class SchedulerDataprocessor : SchedulerAjaxCaller
    {
        protected string convertMode(SchedulerDataprocessor.TransactionModes mode)
        {
            return mode == SchedulerDataprocessor.TransactionModes.GET ? "GET" : "POST";
        }

        public bool SendMode { get; set; }

        protected override string _Template()
        {
            return "\n\tdp = new dataProcessor(\"{0}\");\n\tdp.init({3});\n\tdp.setTransactionMode(\"{1}\", {4});{2}";
        }

        public SchedulerDataprocessor(string controller)
          : this(controller, "Save")
        {
        }

        public SchedulerDataprocessor(string controller, string action)
          : base(controller)
        {
            this.Action = action;
            this.TransactionalMode = SchedulerDataprocessor.TransactionModes.POST;
            this.UpdateFieldsAfterSave = false;
        }

        public SchedulerDataprocessor.TransactionModes TransactionalMode { get; set; }

        public void Enable()
        {
            this.Enabled = true;
        }

        public void Enable(NameValueCollection parameters)
        {
            this.Enabled = true;
            this.AddParameters(parameters);
        }

        public bool UpdateFieldsAfterSave { get; set; }

        protected string updateSaveWatcher(string parent)
        {
            if (this.UpdateFieldsAfterSave)
                return string.Format("\r\n    dp.attachEvent('onAfterUpdate', function (s_id, action, t_id, node) {{\r\n        if(action == 'inserted' || action == 'updated'){{\r\n            var ev = {0}.getEvent(t_id);\r\n            for(var i = 0; i < node.attributes.length; i++){{\r\n                var attr_name = node.attributes[i].name;\r\n                if(attr_name.indexOf('dhx_') != 0)\r\n                    continue;               \r\n                var property_name = attr_name.replace('dhx_', '');\r\n                \r\n                if(ev[property_name] instanceof Date || property_name == 'start_date' || property_name == 'end_date'){{\r\n                    ev[property_name] = {0}.templates.xml_date(node.attributes[i].value);\r\n                }}else{{\r\n                    ev[property_name] = node.attributes[i].value;\r\n                }}\r\n            }}\r\n            {0}.updateEvent(t_id);\r\n        }} \r\n        \r\n    }});", (object)parent);
            return "";
        }

        public override void Render(StringBuilder builder, string parent)
        {
            if (!this.Enabled)
                return;
            builder.Append(string.Format(this._Template(), (object)this.Url, (object)this.convertMode(this.TransactionalMode), (object)this.updateSaveWatcher(parent), (object)parent, this.SendMode ? (object)"true" : (object)"false"));
        }

        public enum TransactionModes
        {
            GET,
            POST,
        }
    }
    public class SchedulerDataManager
    {
        public Action<StringBuilder, object> EventRenderer { get; set; }

        public DHXSchedulerDataStore Pull { get; protected set; }

        public SchedulerDataLoader Loader { get; protected set; }

        public SchedulerDataprocessor DataProcessor { get; protected set; }

        public SchedulerDataManager()
        {
            this.Pull = new DHXSchedulerDataStore();
            this.Loader = new SchedulerDataLoader("undefined");
            this.DataProcessor = new SchedulerDataprocessor("undefined");
        }

        public void EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode mode)
        {
            this.Loader.EnableDynamicLoading(mode);
        }

        internal Dictionary<string, FileType> GetJS()
        {
            return this.Loader.GetJS();
        }

        public void Parse(IEnumerable data)
        {
            this.Pull.Parse(data);
        }

        public string DateFormat
        {
            get
            {
                return this.Pull.DateFormat;
            }
            set
            {
                this.Pull.DateFormat = value;
            }
        }

        public void DropOffset()
        {
            this.Pull.DropOffset();
        }

        public TimeSpan TimeOffset
        {
            get
            {
                return this.Pull.TimeOffset;
            }
            set
            {
                this.Pull.TimeOffset = value;
            }
        }

        public bool ToUniversalTime
        {
            get
            {
                return this.Pull.ToUniversalTime;
            }
            set
            {
                this.Pull.ToUniversalTime = value;
            }
        }

        public string Controller
        {
            set
            {
                this.Loader.Controller = value;
                this.DataProcessor.Controller = value;
            }
        }

        public string SaveAction
        {
            get
            {
                return this.DataProcessor.Action;
            }
            set
            {
                this.DataProcessor.Action = value;
            }
        }

        public string DataAction
        {
            get
            {
                return this.Loader.Action;
            }
            set
            {
                this.Loader.Action = value;
            }
        }

        public string LoadDataUrl
        {
            get
            {
                return this.Loader.Url;
            }
        }

        public string SaveDataUrl
        {
            get
            {
                return this.DataProcessor.Url;
            }
        }

        public string DataUrl
        {
            get
            {
                return this.Loader.RequestUrl;
            }
        }

        public string SaveUrl
        {
            get
            {
                return this.DataProcessor.RequestUrl;
            }
        }

        public int Count
        {
            get
            {
                return this.Pull.Count;
            }
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (this.Pull.Count != 0)
            {
                if (this.EventRenderer == null)
                    builder.Append(string.Format("\n\t{0}.parse({1}, \"json\");", (object)parent, (object)this.Pull.Render()));
                else
                    builder.Append(string.Format("\n\t{0}.parse({1}, \"json\");", (object)parent, (object)this.Pull.Render(this.EventRenderer)));
            }
            this.DataProcessor.Render(builder, parent);
            this.Loader.Render(builder, parent);
        }
    }
    public class SchedulerDataLoader : SchedulerAjaxCaller
    {
        protected string convertLoadingMode(SchedulerDataLoader.DynamicalLoadingMode mode)
        {
            return mode.ToString("g").ToLower();
        }

        public SchedulerDataLoader.DataFormats ExpectedDataFormat
        {
            get
            {
                return (SchedulerDataLoader.DataFormats)this._properties["dataFormat"];
            }
            set
            {
                this._properties["dataFormat"] = (object)value;
            }
        }

        protected string convertDataFormat(SchedulerDataLoader.DataFormats mode)
        {
            if (mode == SchedulerDataLoader.DataFormats.JSON)
                return "json";
            return mode == SchedulerDataLoader.DataFormats.XML ? "xml" : "ical";
        }

        public void EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode mode)
        {
            this._properties["dynamicLoading"] = (object)true;
            this._properties["dybamicLoadingMode"] = (object)mode;
        }

        public AjaxUrl AddAction(string action)
        {
            AjaxUrl ajaxUrl = new AjaxUrl();
            ajaxUrl.Controller = this.Controller;
            ajaxUrl.Action = action;
            this._Urls.Add(ajaxUrl);
            return ajaxUrl;
        }

        internal override Dictionary<string, FileType> GetJS()
        {
            Dictionary<string, FileType> dictionary = new Dictionary<string, FileType>();
            if (this._Urls.Count > 1)
                dictionary.Add("ext/dhtmlxscheduler_multisource.js", FileType.Local);
            return dictionary;
        }

        public AjaxUrl AddPath(string path)
        {
            AjaxUrl ajaxUrl = new AjaxUrl();
            ajaxUrl.Controller = (string)null;
            ajaxUrl.Action = path;
            this._Urls.Add(ajaxUrl);
            return ajaxUrl;
        }

        public List<AjaxUrl> GetUrls()
        {
            return this._Urls;
        }

        protected override string _Template()
        {
            return this._Urls.Count == 1 ? "{1}{2}\n\t{0}.load(\"{3}\"{4});" : "{1}{2}\n\t{0}.load([{3}]{4});";
        }

        public SchedulerDataLoader(string controller)
          : this(controller, "Data")
        {
        }

        public SchedulerDataLoader(string controller, string action)
          : base(controller)
        {
            this.ExpectedDataFormat = SchedulerDataLoader.DataFormats.JSON;
            this.Action = action;
        }

        public void PreventCache()
        {
            this._properties["noCache"] = (object)true;
        }

        public void Load()
        {
            this.Enabled = true;
        }

        public void Load(NameValueCollection parameters)
        {
            this.Enabled = true;
            this.AddParameters(parameters);
        }

        protected string preventCacheString(string parent)
        {
            if (!this._properties.ContainsKey("noCache") || !(bool)this._properties["noCache"])
                return "";
            return string.Format("\n\t{0}.config.prevent_cache = true;", (object)parent);
        }

        protected string loadModeString(string parent)
        {
            if (!this._properties.ContainsKey("dynamicLoading") || !(bool)this._properties["dynamicLoading"])
                return "";
            return string.Format("\n\t{0}.setLoadMode(\"{1}\");", (object)parent, (object)this.convertLoadingMode((SchedulerDataLoader.DynamicalLoadingMode)this._properties["dybamicLoadingMode"]));
        }

        protected string urlsString()
        {
            string[] strArray = new string[this._Urls.Count];
            for (int index = 0; index < this._Urls.Count; ++index)
                strArray[index] = "\"" + this._Urls[index].Url + "\"";
            return string.Join(",", strArray);
        }

        public override void Render(StringBuilder builder, string parent)
        {
            if (!this.Enabled)
                return;
            if (this._Urls.Count == 1)
                builder.Append(string.Format(this._Template(), (object)parent, (object)this.preventCacheString(parent), (object)this.loadModeString(parent), (object)this.Url, this.ExpectedDataFormat != SchedulerDataLoader.DataFormats.XML ? (object)string.Format(", \"{0}\"", (object)this.ExpectedDataFormat.ToString("g").ToLower()) : (object)""));
            else
                builder.Append(string.Format(this._Template(), (object)parent, (object)this.preventCacheString(parent), (object)this.loadModeString(parent), (object)this.urlsString(), this.ExpectedDataFormat != SchedulerDataLoader.DataFormats.XML ? (object)string.Format(", \"{0}\"", (object)this.ExpectedDataFormat.ToString("g").ToLower()) : (object)""));
        }

        public enum DataFormats
        {
            XML,
            JSON,
            iCal,
        }

        public enum DynamicalLoadingMode
        {
            Year,
            Month,
            Week,
            Day,
        }
    }
    public sealed class SchedulerAjaxData
    {
        private DHXSchedulerDataStore data = new DHXSchedulerDataStore();

        public string FromUrl(string url)
        {
            return new RequestHelper().Send(url);
        }

        public void FromICal(string url)
        {
            this.data.Parse((IEnumerable)new ICalHelper().GetFromFeed(url));
        }

        public List<SchedulerEvent> ParseIcs(string ics)
        {
            return new ICalHelper().Parse(ics);
        }

        public List<SchedulerEvent> LoadIcs(string url)
        {
            return this.ParseIcs(this.FromUrl(url));
        }

        public TimeSpan TimeOffset
        {
            get
            {
                return this.data.TimeOffset;
            }
            set
            {
                this.data.TimeOffset = value;
            }
        }

        [Obsolete("Use DropOffset() instead")]
        public void UseUTCDate()
        {
            this.DropOffset();
        }

        public void DropOffset()
        {
            this.data.DropOffset();
        }

        public bool ToUniversalTime
        {
            get
            {
                return this.data.ToUniversalTime;
            }
            set
            {
                this.data.ToUniversalTime = value;
            }
        }

        public SchedulerAjaxData(IEnumerable items)
          : this()
        {
            this.data.Parse(items);
        }

        public void Add(IEnumerable items)
        {
            this.data.Parse(items);
        }

        public SchedulerAjaxData()
        {
            //TimeCheck.Check();
        }

        public static implicit operator string(SchedulerAjaxData data)
        {
            return data.Render();
        }

        public override string ToString()
        {
            return this.Render();
        }

        public string Render()
        {
            return this.data.Render();
        }

        public string Render(Action<StringBuilder, object> renderer)
        {
            return this.data.Render(renderer);
        }

        public static implicit operator ContentResult(SchedulerAjaxData data)
        {
            return SchedulerAjaxData.ToContentResult(data);
        }

        public static ContentResult ToContentResult(SchedulerAjaxData data)
        {
            return new ContentResult()
            {
                Content = data.data.Render(),
                ContentType = "text/json"
            };
        }

        public string DateFormat
        {
            get
            {
                return this.data.DateFormat;
            }
            set
            {
                this.data.DateFormat = value;
            }
        }
    }
    public abstract class SchedulerAjaxCaller : SchedulerSettingsBase
    {
        protected List<AjaxUrl> _Urls = new List<AjaxUrl>();

        protected abstract string _Template();

        public SchedulerAjaxCaller(string controller)
        {
            this._Urls.Add(new AjaxUrl()
            {
                Controller = controller
            });
            this.Enabled = false;
        }

        public string Action
        {
            get
            {
                return this._Urls[0].Action;
            }
            set
            {
                this._Urls[0].Action = value;
            }
        }

        public string Controller
        {
            get
            {
                return this._Urls[0].Controller;
            }
            set
            {
                this._Urls[0].Controller = value;
            }
        }

        public void AddParameters(Dictionary<string, object> param)
        {
            this._Urls[0].AddParameters(param);
        }

        public void AddParameters(NameValueCollection param)
        {
            this._Urls[0].AddParameters(param);
        }

        public void AddParameter(string name, object value)
        {
            this._Urls[0].AddParameter(name, value);
        }

        public bool Enabled
        {
            get
            {
                return (bool)this._properties["active"];
            }
            set
            {
                this._properties["active"] = (object)value;
            }
        }

        public virtual string RequestUrl
        {
            get
            {
                return this._Urls[0].RequestUrl;
            }
        }

        public virtual string Url
        {
            get
            {
                return this._Urls[0].Url;
            }
        }

        public override void Render(StringBuilder builder, string parent)
        {
            if (!this.Enabled && this._properties.IsNullOrDefault("parameters"))
                return;
            builder.Append(string.Format(this._Template(), (object)parent, (object)this.Url));
        }
    }
    public static class ResponseScriptHelper
    {
        public static string ToString(this SchedulerFormResponseScript data, string parent)
        {
            return data.Render(parent);
        }

        public static ContentResult ToContentResult(this SchedulerFormResponseScript data, string parent)
        {
            return new ContentResult()
            {
                Content = data.Render(parent),
                ContentType = "text/html"
            };
        }
    }
    public class ICalRenderer
    {
        private string ic_begin = "BEGIN:VEVENT\n";
        private string d_start = "DTSTART:";
        private string d_end = "DTEND:";
        private string summ = "SUMMARY:";
        private string ic_end = "END:VEVENT\n";
        private string fulldateFormat = DateFormatHelper.ConvertInnerFormatToNet("%Y%m%dT%H%i%s");
        private string full_day_format = DateFormatHelper.ConvertInnerFormatToNet("%Y%m%d");
        private ICalRenderer.DatInfo _Info;

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Text { get; set; }

        public ICalRenderer()
          : this("start_date", "end_date", "text")
        {
        }

        public ICalRenderer(string start, string end, string text)
        {
            this.StartDate = start;
            this.EndDate = end;
            this.Text = text;
        }

        protected void _RenderItem(StringBuilder builder, object item)
        {
            builder.Append(this.ic_begin);
            DateTime dateTime1 = (DateTime)this._Info.Start.GetValue(item, (object[])null);
            DateTime dateTime2 = (DateTime)this._Info.End.GetValue(item, (object[])null);
            string str1 = this.fulldateFormat;
            string str2 = this.fulldateFormat;
            if (dateTime1.Hour == 0 && dateTime1.Minute == 0)
                str1 = this.full_day_format;
            builder.Append(string.Format("{0}{1:" + str1 + "}\n", (object)this.d_start, (object)dateTime1));
            if (dateTime2.Hour == 0 && dateTime2.Minute == 0)
                str2 = this.full_day_format;
            builder.Append(string.Format("{0}{1:" + str2 + "}\n", (object)this.d_end, (object)dateTime2));
            builder.Append(string.Format("{0}{1}\n", (object)this.summ, this._Info.Text.GetValue(item, (object[])null)));
            builder.Append(this.ic_end);
        }

        protected void _RenderCustom(IEnumerable events, StringBuilder builder, Action<StringBuilder, object> renderer)
        {
            foreach (object obj in events)
                renderer(builder, obj);
        }

        protected void _RenderDefault(IEnumerable events, StringBuilder builder)
        {
            if (events == null || this.StartDate == null || (this.EndDate == null || this.Text == null))
                return;
            object obj1 = (object)null;
            IEnumerator enumerator = events.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                    obj1 = enumerator.Current;
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
            if (obj1 == null)
                return;
            PropertyInfo[] properties = obj1.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == this.StartDate));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == this.EndDate));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == this.Text));
            if (propertyInfo1 == null || propertyInfo2 == null || propertyInfo3 == null)
                return;
            this._Info = new ICalRenderer.DatInfo()
            {
                Start = propertyInfo1,
                End = propertyInfo2,
                Text = propertyInfo3
            };
            foreach (object obj2 in events)
                this._RenderItem(builder, obj2);
        }

        public string ToICal(IEnumerable events)
        {
            return this.ToICal(events, (Action<StringBuilder, object>)null, "");
        }

        public string ToICal(IEnumerable events, Action<StringBuilder, object> renderer)
        {
            return this.ToICal(events, renderer, "");
        }

        public string ToICal(IEnumerable events, Action<StringBuilder, object> renderer, string description)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("BEGIN:VCALENDAR\nVERSION:2.0\nPRODID:-//dhtmlXScheduler for .Net//NONSGML v2.2//EN\nDESCRIPTION:");
            builder.Append(description);
            builder.Append("\n");
            if ((Delegate)renderer != (Delegate)null)
                this._RenderCustom(events, builder, renderer);
            else
                this._RenderDefault(events, builder);
            builder.Append("END:VCALENDAR");
            return builder.ToString();
        }

        public string ToICal(IEnumerable events, string description)
        {
            return this.ToICal(events, (Action<StringBuilder, object>)null, description);
        }

        internal class DatInfo
        {
            public PropertyInfo Start;
            public PropertyInfo End;
            public PropertyInfo Text;
        }
    }
}
