using DHTMLX.Common;
using DHTMLX.Scheduler.Api;
using DHTMLX.Scheduler.Authentication;
using DHTMLX.Scheduler.Controls;
using DHTMLX.Scheduler.Data;
using DHTMLX.Scheduler.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static DHTMLX.Scheduler.DHXTimeSpan;
using System.Web.Script.Serialization;
using System.Web;

namespace DHTMLX.Scheduler
{
    public sealed class DHXScheduler
    {
        internal static string ServerListName = "dhx_scheduler_server_list";
        private readonly string DefaultName = "scheduler";
        private string _Name = "scheduler";
        private PropertiesContainer<object> _configuration = new PropertiesContainer<object>();
        private List<SchedulerSettingsBase> _Settings = new List<SchedulerSettingsBase>();
        private List<SchedulerControlsBase> _Controls = new List<SchedulerControlsBase>();
        private SchedulerDataManager _data = new SchedulerDataManager();
        private static Dictionary<string, IDHXServerList> ServerListsStore;
        private Dictionary<string, object> _DefaultValues;

        public string Name
        {
            get
            {
                return this._Name;
            }
        }

        public static IDHXServerList GetServerList()
        {
            return DHXScheduler.GetServerList(DHXScheduler.ServerListName);
        }

        internal static IDHXServerList GetServerList(string id)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains((object)id))
                    return HttpContext.Current.Items[(object)id] as IDHXServerList;
                HttpContext.Current.Items[(object)id] = (object)new DHXServerList();
                return HttpContext.Current.Items[(object)id] as IDHXServerList;
            }
            if (DHXScheduler.ServerListsStore == null)
                DHXScheduler.ServerListsStore = new Dictionary<string, IDHXServerList>();
            if (DHXScheduler.ServerListsStore.ContainsKey(id))
                return DHXScheduler.ServerListsStore[id];
            DHXScheduler.ServerListsStore.Add(id, (IDHXServerList)new DHXServerList());
            return DHXScheduler.ServerListsStore[id];
        }
        
        public string Version { get; set; }

        public DHXScheduler.Skins Skin
        {
            get
            {
                return (DHXScheduler.Skins)this._configuration["skin"];
            }
            set
            {
                this._configuration["skin"] = (object)value;
            }
        }

        public SchedulerDataManager Data
        {
            get
            {
                return this._data;
            }
        }

        public SchedulerLightbox Lightbox { get; set; }

        public SchedulerConfig Config { get; set; }

        public SchedulerXY XY { get; set; }

        public SchedulerTemplates Templates { get; set; }

        internal DHXEventHandlers EventHandlers { get; set; }

        public MiniCalendarManager Calendars { get; set; }

        public SchedulerViewsController Views { get; set; }

        public SchedulerExtensions Extensions { get; set; }

        public SchedulerLocalization Localization { get; set; }

        public SchedulerUser Authentication { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool HasBeforeInitCode
        {
            get
            {
                return this._configuration["before_init"] != null;
            }
        }

        public List<string> BeforeInit
        {
            get
            {
                if (this._configuration["before_init"] == null)
                    this._configuration["before_init"] = (object)new List<string>();
                return (List<string>)this._configuration["before_init"];
            }
            set
            {
                this._configuration["before_init"] = (object)value;
            }
        }

        public bool HasAfterInitCode
        {
            get
            {
                return this._configuration["after_init"] != null;
            }
        }

        public List<string> AfterInit
        {
            get
            {
                if (this._configuration["after_init"] == null)
                    this._configuration["after_init"] = (object)new List<string>();
                return (List<string>)this._configuration["after_init"];
            }
            set
            {
                this._configuration["after_init"] = (object)value;
            }
        }

        public string Codebase { get; set; }

        public string InitialView { get; set; }

        public DateTime InitialDate { get; set; }

        public string Controller
        {
            set
            {
                this.Data.Controller = value;
            }
            private get
            {
                return this.Data.Loader.Controller;
            }
        }

        public string DataAction
        {
            get
            {
                return this.Data.DataAction;
            }
            set
            {
                this.Data.DataAction = value;
            }
        }

        public string SaveAction
        {
            get
            {
                return this.Data.SaveAction;
            }
            set
            {
                this.Data.SaveAction = value;
            }
        }

        public string DataUrl
        {
            get
            {
                return this.Data.DataUrl;
            }
        }

        public string SaveUrl
        {
            get
            {
                return this.Data.SaveUrl;
            }
        }

        public bool EnableDataprocessor
        {
            get
            {
                return this.Data.DataProcessor.Enabled;
            }
            set
            {
                this.Data.DataProcessor.Enabled = value;
            }
        }

        public bool LoadData
        {
            get
            {
                return this.Data.Loader.Enabled;
            }
            set
            {
                this.Data.Loader.Enabled = value;
            }
        }

        public void SetUserDetails(object data, string userIdKey)
        {
            this.Authentication.SetUserDetails(data);
            this.Authentication.UserIdKey = userIdKey;
        }

        public void SetUserDetails(object data, string userIdKey, string eventUserIdKey)
        {
            this.SetUserDetails(data, userIdKey);
            this.Authentication.EventUserIdKey = eventUserIdKey;
        }

        public void PreventCache()
        {
            this.Data.Loader.PreventCache();
        }

        public void UpdateFieldsAfterSave()
        {
            this.Data.DataProcessor.UpdateFieldsAfterSave = true;
        }

        public void SetEditMode(params EditModes[] mode)
        {
            this.Authentication.Modes = mode;
        }

        public void SetUserDetails(Dictionary<string, object> data, string userIdKey)
        {
            this.Authentication.SetUserDetails(data);
            this.Authentication.UserIdKey = userIdKey;
        }

        public void SetUserDetails(Dictionary<string, object> data, string userIdKey, string eventUserIdKey)
        {
            this.SetUserDetails(data, userIdKey);
            this.Authentication.EventUserIdKey = eventUserIdKey;
        }

        public SchedulerTimeSpanMarker TimeSpans { get; set; }

        public SchedulerHighlighter Highlighter { get; set; }

        public bool ConnectorMode
        {
            get
            {
                return this.Data.DataProcessor.SendMode;
            }
            set
            {
                this.Data.DataProcessor.SendMode = value;
            }
        }

        public SchedulerDataLoader.DataFormats DataFormat
        {
            get
            {
                return this.Data.Loader.ExpectedDataFormat;
            }
            set
            {
                this.Data.Loader.ExpectedDataFormat = value;
            }
        }

        public void EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode mode)
        {
            this.Data.EnableDynamicLoading(mode);
        }

        public DHXScheduler()
        {
            //TimeCheck.Check();
            this.EventHandlers = new DHXEventHandlers();
            this.Config = new SchedulerConfig();
            this.XY = new SchedulerXY();
            this.Templates = new SchedulerTemplates();
            this.Authentication = new SchedulerUser(this.EventHandlers);
            this._Settings.Add((SchedulerSettingsBase)this.Config);
            this._Settings.Add((SchedulerSettingsBase)this.XY);
            this._Settings.Add((SchedulerSettingsBase)this.Templates);
            this._Settings.Add((SchedulerSettingsBase)this.Authentication);
            this.Extensions = new SchedulerExtensions();
            this.Lightbox = new SchedulerLightbox();
            this.Views = new SchedulerViewsController();
            this.Calendars = new MiniCalendarManager();
            this.Localization = new SchedulerLocalization();
            this._Controls.Add((SchedulerControlsBase)this.Extensions);
            this._Controls.Add((SchedulerControlsBase)this.Lightbox);
            this._Controls.Add((SchedulerControlsBase)this.Views);
            this._Controls.Add((SchedulerControlsBase)this.Calendars);
            this._Controls.Add((SchedulerControlsBase)this.Localization);
            this.TimeSpans = new SchedulerTimeSpanMarker();
            this.Highlighter = new SchedulerHighlighter();
            this.Config.details_on_create = true;
            this.Config.details_on_dblclick = true;
            this.Config.prevent_cache = true;
            this.Codebase = "/Scripts/dhtmlxScheduler";
            this._configuration.SetDefaults(new Dictionary<string, object>()
      {
        {
          "load_data",
          (object) false
        },
        {
          "dataprocessor",
          (object) false
        },
        {
          "saveAction",
          (object) "Save"
        },
        {
          "dataAction",
          (object) nameof (Data)
        },
        {
          "controller",
          (object) nameof (DHXScheduler)
        }
      });
            this.Skin = DHXScheduler.Skins.Glossy;
        }

        public DHXScheduler(System.Web.Mvc.Controller parent)
          : this()
        {
            string name = parent.GetType().Name;
            int length = nameof(Controller).Length;
            if (name.Length > length && name.EndsWith(nameof(Controller)))
                this.Controller = name.Substring(0, name.Length - length);
            else
                this.Controller = name;
        }

        public DHXScheduler(string name)
          : this()
        {
            this._Name = name;
        }

        public DHXScheduler(System.Web.Mvc.Controller parent, string name)
          : this(parent)
        {
            this._Name = name;
        }

        public string ToICal()
        {
            return this.ToICal("/" + this.Controller + "/Export");
        }

        public string ToICal(string url)
        {
            return this.ToICal(url, "data");
        }

        public string ToICal(string url, string paramName)
        {
            string str = DateTime.Now.Ticks.ToString();
            return "var d=document.createElement('div');d.style.display='none';document.body.appendChild(d);" + string.Format("d.innerHTML = '<form id=\\'{1}\\' method=\\'post\\' target=\\'_blank\\' action=\\'{0}\\' accept-charset=\\'utf-8\\' enctype=\\'application/x-www-form-urlencoded\\'><input type=\\'hidden\\' name=\\'{2}\\'/> </form>';document.getElementById('{1}').firstChild.value = {3}.toICal();document.getElementById('{1}').submit();d.parentNode.removeChild(d);", (object)url, (object)str, (object)paramName, (object)this.Name);
        }

        public string ToPDF()
        {
            return this.ToPDF("//dhtmlxscheduler.appspot.com/export/pdf");
        }

        public string ToPDF(string url)
        {
            return this.ToPDF(url, ExportColorScheme.Color);
        }

        public string ToPDF(string url, ExportColorScheme scheme)
        {
            if (scheme == ExportColorScheme.Color)
                return string.Format("{1}.toPDF('{0}');", (object)url, (object)this.Name);
            return string.Format("{2}.toPDF('{0}', '{1}');", (object)url, (object)this._SchemeToString(scheme), (object)this.Name);
        }

        public bool HasDefaultValues
        {
            get
            {
                return this._DefaultValues != null;
            }
        }

        private string _ProcessDefaultValues()
        {
            CommonPropertiesContainer propertiesContainer = new CommonPropertiesContainer();
            if (this.InitialValues.Count == 0)
                return "";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("if(!event_id){{return;}}var ev = {0}.getEvent(event_id);if(!ev)return;\n", (object)this.Name));
            foreach (string key in this.InitialValues.Keys)
            {
                string str;
                if (this.InitialValues[key] is DateTime)
                {
                    DateTime initialValue = (DateTime)this.InitialValues[key];
                    str = string.Format("new Date({0}, {1}, {2}, {3}, {4}, {5})", (object)initialValue.Year, (object)(initialValue.Month - 1), (object)initialValue.Day, (object)initialValue.Hour, (object)initialValue.Minute, (object)initialValue.Second);
                }
                else
                    str = propertiesContainer.Render(key, this.InitialValues[key]);
                stringBuilder.Append(string.Format("\t\tev.{0} = {1};\n", (object)key, (object)str));
            }
            return stringBuilder.ToString();
        }

        public Dictionary<string, object> InitialValues
        {
            get
            {
                if (this._DefaultValues == null)
                    this._DefaultValues = new Dictionary<string, object>();
                return this._DefaultValues;
            }
        }

        private string _SchemeToString(ExportColorScheme scheme)
        {
            switch (scheme)
            {
                case ExportColorScheme.FullColor:
                    return "full_color";
                case ExportColorScheme.Gray:
                    return "gray";
                case ExportColorScheme.BlackWhite:
                    return "bw";
                default:
                    return "color";
            }
        }

        public string GenerateMarkup()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format(" <div id='{0}_here' class='dhx_cal_container' style='width:{1};height:{2};'>\r\n\t\t\t\t\t\t<div class='dhx_cal_navline'>\r\n\t\t\t\t\t\t\t<div class='dhx_cal_prev_button'>&nbsp;</div>\r\n\t\t\t\t\t\t\t<div class='dhx_cal_next_button'>&nbsp;</div>\r\n\t\t\t\t\t\t\t<div class='dhx_cal_today_button'></div>\r\n\t\t\t\t\t\t\t<div class='dhx_cal_date'></div>", (object)this.Name, this.Width == 0 ? (object)"100%" : (object)(this.Width.ToString() + "px"), this.Height == 0 ? (object)"100%" : (object)(this.Height.ToString() + "px")));
            builder.Append(this.Calendars.RenderTab(this.Skin, this.Name));
            foreach (SchedulerView view in this.Views.GetViews())
                builder.Append(view.RenderTab(this.Skin));
            builder.Append("</div>\r\n\t\t\t\t\t<div class='dhx_cal_header'>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class='dhx_cal_data'>\r\n\t\t\t\t\t</div>\r\n\r\n\t\t\t\t</div>");
            if (this.Templates.EventBox != null && (this.Templates.EventBox.CanResize || this.Templates.EventBox.CanDrag))
            {
                builder.Append("<style>");
                this.Templates.EventBox.RenderCss(builder);
                builder.Append("</style>");
            }
            return builder.ToString();
        }

        public string GenerateJSCode()
        {
            StringBuilder builder = new StringBuilder();
            if (this.Name != this.DefaultName)
                builder.Append(string.Format("\n\twindow.{0} = Scheduler.getSchedulerInstance();", (object)this.Name));
            builder.Append("\n");
            builder.Append(string.Format(DHXScheduler.GetServerList().InitConfigSection, (object)this.Name));
            if (DHXScheduler.GetServerList().Count > 0)
                builder.Append(DHXScheduler.GetServerList().RenderCollections(this.Name));
            foreach (SchedulerControlsBase control in this._Controls)
                control.Render(builder, this.Name);
            foreach (SchedulerSettingsBase setting in this._Settings)
                setting.Render(builder, this.Name);
            if (this.HasBeforeInitCode)
            {
                builder.Append("\n");
                builder.Append(string.Join("\n", this.BeforeInit.ToArray()));
                builder.Append("\n");
            }
            if (this.EventHandlers.Events.Count > 0)
                this.EventHandlers.Render(builder, this.Name);
            if (this.TimeSpans.Items.Count > 0)
                this.TimeSpans.Render(builder, this.Name);
            if (this.Highlighter.Enabled)
                this.Highlighter.Render(builder, this.Name);
            List<string> stringList = new List<string>();
            stringList.Add(string.Format("'{0}_here'", (object)this.Name));
            DateTime initialDate = this.InitialDate;
            string str = initialDate == new DateTime() ? "null" : DateFormatHelper.DateToJS(initialDate);
            string initialView = this.InitialView;
            if (initialDate != new DateTime() || initialView != (string)null)
            {
                if (initialView != (string)null)
                {
                    stringList.Add(str);
                    stringList.Add("'" + initialView + "'");
                }
                else if (initialDate != new DateTime())
                    stringList.Add(str);
            }
            builder.Append(string.Format("\n\t{0}.init({1});", (object)this.Name, (object)string.Join(",", stringList.ToArray())));
            if (this.Extensions != null && this.Extensions.Items != null && this.Extensions.Items.Count != 0)
            {
                foreach (KeyValuePair<SchedulerExtensions.Extension, DHXExtension> keyValuePair in this.Extensions.Items)
                    keyValuePair.Value.Render(builder, this);
            }
            if (this.HasAfterInitCode)
            {
                builder.Append("\n");
                builder.Append(string.Join("\n", this.AfterInit.ToArray()));
                builder.Append("\n");
            }
            if (this.HasDefaultValues)
                this.EventHandlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onEventCreated, this._ProcessDefaultValues()));
            this.Data.DateFormat = this.Config.xml_date;
            this.Data.Render(builder, this.Name);
            foreach (SchedulerControlsBase control in this._Controls)
                builder.Append(control.AfterInit());
            return builder.ToString();
        }

        public string GenerateHTML()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.GenerateMarkup());
            stringBuilder.Append("<script>");
            stringBuilder.Append(this.GenerateJSCode());
            stringBuilder.Append("</script>");
            return stringBuilder.ToString();
        }

        public string Render()
        {
            return this.GenerateLinks() + this.GenerateHTML();
        }

        public string GenerateLinks()
        {
            return this.GenerateJS() + this.GenerateCSS();
        }

        private Dictionary<string, FileType> GetJS()
        {
            Dictionary<string, FileType> dictionary = new Dictionary<string, FileType>()
      {
        {
          "dhtmlxscheduler.js",
          FileType.Local
        }
      };
            if (this.Skin == DHXScheduler.Skins.Terrace)
                dictionary.Add("ext/dhtmlxscheduler_dhx_terrace.js", FileType.Local);
            return dictionary;
        }

        private Dictionary<string, FileType> GetCSS()
        {
            string str = "";
            switch (this.Skin)
            {
                case DHXScheduler.Skins.Glossy:
                    str = "_glossy";
                    break;
                case DHXScheduler.Skins.Terrace:
                    str = "_dhx_terrace";
                    break;
            }
            return new Dictionary<string, FileType>()
      {
        {
          string.Format("dhtmlxscheduler{0}.css", (object) str),
          FileType.Local
        }
      };
        }

        private Dictionary<string, FileType> _getAttached(SourceType ext)
        {
            Dictionary<string, FileType> first = new Dictionary<string, FileType>();
            switch (ext)
            {
                case SourceType.JS:
                    first = this.GetJS();
                    foreach (SchedulerControlsBase control in this._Controls)
                        DictionaryMerger.Join(ref first, control.GetJS());
                    foreach (SchedulerSettingsBase setting in this._Settings)
                        DictionaryMerger.Join(ref first, setting.GetJS());
                    DictionaryMerger.Join(ref first, this.Data.GetJS());
                    break;
                case SourceType.CSS:
                    first = this.GetCSS();
                    foreach (SchedulerControlsBase control in this._Controls)
                        DictionaryMerger.Join(ref first, control.GetCSS());
                    using (List<SchedulerSettingsBase>.Enumerator enumerator = this._Settings.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            SchedulerSettingsBase current = enumerator.Current;
                            DictionaryMerger.Join(ref first, current.GetJS());
                        }
                        break;
                    }
            }
            return first;
        }

        private string _generateSource(SourceType ext, List<string> pattern, bool inline)
        {
            Dictionary<string, FileType> attached = this._getAttached(ext);
            List<string> stringList = new List<string>();
            string str1 = this.Codebase != (string)null ? this.Codebase + "/" : "";
            foreach (KeyValuePair<string, FileType> keyValuePair in attached)
            {
                string str2 = keyValuePair.Key;
                if (!string.IsNullOrEmpty(this.Version) && !str2.Contains("google.com"))
                    str2 = str2 + (str2.IndexOf('?') < 0 ? "?v=" : "&v=") + this.Version;
                if (!inline)
                {
                    stringList.Add(pattern[0] + (keyValuePair.Value == FileType.Local ? str1 : "") + str2 + pattern[1]);
                }
                else
                {
                    FileGrabber fileGrabber = new FileGrabber();
                    string str3 = (keyValuePair.Value == FileType.Local ? str1 : "") + str2;
                    string str4 = !Regex.IsMatch(str3, "http(s)?://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?") ? fileGrabber.GetLocalFile(str3) : fileGrabber.GetRemoteFile(str3);
                    stringList.Add(pattern[0] + str4 + pattern[1]);
                }
            }
            return string.Join("\n", stringList.ToArray());
        }

        public string GenerateJS()
        {
            return this.GenerateJS(false);
        }

        public string GenerateJS(bool inline)
        {
            if (this.TimeSpans.Items.Count > 0 || this.Highlighter.Enabled)
                this.Extensions.Add(SchedulerExtensions.Extension.Limit);
            List<string> pattern = new List<string>();
            if (!inline)
            {
                pattern.Add("<script src=\"");
                pattern.Add("\" type=\"text/javascript\" charset=\"utf-8\"></script>");
            }
            else
            {
                pattern.Add("<script type=\"text/javascript\" charset=\"utf-8\">");
                pattern.Add("</script>");
            }
            return this._generateSource(SourceType.JS, pattern, inline);
        }

        public string GenerateCSS()
        {
            return this.GenerateCSS(false);
        }

        public string GenerateCSS(bool inline)
        {
            List<string> pattern = new List<string>();
            if (!inline)
            {
                pattern.Add("<link rel=\"stylesheet\" href=\"");
                pattern.Add("\" type=\"text/css\" charset=\"utf-8\">");
            }
            else
            {
                pattern.Add("<style type=\"text/css\">");
                pattern.Add("</style>");
            }
            return this._generateSource(SourceType.CSS, pattern, inline);
        }

        public void Deserialize(string data)
        {
            Dictionary<string, object> dictionary = new JavaScriptSerializer().DeserializeObject(data) as Dictionary<string, object>;
            this._configuration.Clear();
            foreach (KeyValuePair<string, object> keyValuePair in dictionary)
                ;
            if (dictionary.ContainsKey("settings"))
            {
                foreach (KeyValuePair<string, object> keyValuePair in dictionary["settings"] as Dictionary<string, object>)
                    this._configuration.Add(keyValuePair.Key, (object)keyValuePair.Value.ToString());
            }
            this.Data.Pull.Clear();
            if (!dictionary.ContainsKey(nameof(data)))
                return;
            this.Data.Pull.Deserialize((object)this._data);
        }

        public string Serialize()
        {
            Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
            dictionary1.Add("settings", (object)dictionary2);
            dictionary1.Add("data", this.Data.Pull.Serialize());
            return JSONHelper.ToJSON((object)dictionary1);
        }

        public enum Skins
        {
            Standart,
            Glossy,
            Terrace,
        }
    }
}

