using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class YearView : SchedulerView
    {
        public YearView()
        {
            this.ViewType = "year";
            this.Set("name", (object)"year");
            this.TabPosition = 280;
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_year_view.js",
          FileType.Local
        }
      };
        }
    }
    public class WeekView : SchedulerView
    {
        public WeekView()
        {
            this.ViewType = "week";
            this.Set("name", (object)"week");
            this.TabPosition = 140;
        }
    }
    public class WeekAgendaView : YearView
    {
        public WeekAgendaView()
        {
            this.ViewType = "week_agenda";
            this.Set("name", (object)"week_agenda");
            this.TabPosition = 280;
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_week_agenda.js",
          FileType.Local
        }
      };
        }
    }
    public class UnitsView : SchedulerView
    {
        protected List<Unit> _units = new List<Unit>();
        private ServerListHelper<Unit> m_serverList;

        public string ServerList
        {
            get
            {
                return this.m_serverList.ServerListLink;
            }
            set
            {
                this.m_serverList.ServerListLink = value;
            }
        }

        internal UnitsView()
        {
            this.m_serverList = new ServerListHelper<Unit>(new Action<IEnumerable>(this._AddOptions), (IList<Unit>)this._units);
        }

        public void AddOption(Unit unit)
        {
            this.m_serverList.AddOption(unit);
        }

        public void AddOptions(Dictionary<object, object> units)
        {
            foreach (KeyValuePair<object, object> unit in units)
                this.AddOption(new Unit(unit.Key, unit.Value));
        }

        public void AddOptions(IEnumerable units)
        {
            this.m_serverList.AddOptions(units);
        }

        public void AddOptions(IEnumerable opts, bool direct)
        {
            if (direct)
                this._AddOptions(opts);
            else
                this.AddOptions(opts);
        }

        public void SetServerList(string listId)
        {
            this.m_serverList.ServerListLink = listId;
        }

        public void _AddOptions(IEnumerable units)
        {
            object obj = (object)null;
            foreach (object unit1 in units)
            {
                Unit unit2 = unit1 as Unit;
                if (unit2 != null)
                {
                    this.AddOption(unit2);
                }
                else
                {
                    obj = unit1;
                    break;
                }
            }
            if (obj == null)
                return;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
            if (propertyInfo1 == null && propertyInfo2 == null)
                return;
            foreach (object unit in units)
                this.AddOption(new Unit(propertyInfo1.GetValue(unit, (object[])null), propertyInfo2.GetValue(unit, (object[])null)));
        }

        public UnitsView(string name, string property)
          : this()
        {
            this.ViewType = "unit";
            this.Name = name;
            this.Label = "Units";
            this.TabPosition = 280;
            this.Property = property;
        }

        public string Property
        {
            get
            {
                return this.Get("property");
            }
            set
            {
                this.Set("property", (object)value);
            }
        }

        public int Size
        {
            get
            {
                return this.GetInt("size");
            }
            set
            {
                this.Set("size", (object)value);
            }
        }

        public int Step
        {
            get
            {
                return this.GetInt("step");
            }
            set
            {
                this.Set("step", (object)value);
            }
        }

        public bool SkipIncorrect
        {
            get
            {
                return this.GetBool("skip_incorrect");
            }
            set
            {
                this.Set("skip_incorrect", (object)value);
            }
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_units.js",
          FileType.Local
        }
      };
        }

        public override Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = base.Serialize();
            object[] objArray = new object[this._units.Count];
            for (int index = 0; index < this._units.Count; ++index)
                objArray[index] = (object)new Dictionary<string, object>()
        {
          {
            "key",
            this._units[index].Key
          },
          {
            "label",
            this._units[index].Label
          }
        };
            dictionary.Add("list", (object)objArray);
            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            foreach (Dictionary<string, object> dictionary in data["list"] as object[])
                this.AddOption(new Unit((object)dictionary["key"].ToString(), (object)dictionary["label"].ToString()));
        }

        public override void Render(StringBuilder builder, string parent)
        {
            base.Render(builder, parent);
            builder.Append("\n");
            if (!this.m_serverList.IsEmptyServerList)
                builder.Append("\n" + this.m_serverList.RenderConfig(parent, this.Name));
            builder.Append(string.Format("\n{0}.createUnitsView(", (object)parent));
            Dictionary<string, object> visibleProperties = this.GetVisibleProperties();
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<string> stringList1 = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in visibleProperties)
                stringList1.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, (object)scriptSerializer.Serialize(keyValuePair.Value)));
            if (this.m_serverList.IsEmptyServerList)
            {
                if (this._units.Count != 0)
                {
                    List<string> stringList2 = new List<string>();
                    foreach (Unit unit in this._units)
                        stringList2.Add(unit.Render());
                    stringList1.Add(string.Format("\"list\":[{0}]", (object)string.Join(", ", stringList2.ToArray())));
                }
            }
            else
                stringList1.Add(string.Format("\"list\":{0}", (object)this.m_serverList.RenderCollection(parent)));
            builder.Append("{");
            builder.Append(string.Join(",\n", stringList1.ToArray()));
            builder.Append("});");
        }
    }
    public class Unit
    {
        protected object _key;
        protected object _label;

        public object Key
        {
            get
            {
                return this._key;
            }
        }

        public object Label
        {
            get
            {
                return this._label;
            }
        }

        public Unit(object key, object label)
        {
            this._key = key;
            this._label = label;
        }

        public string Render()
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            return string.Format("{{ \"key\":{0}, \"label\":{1}}}", (object)scriptSerializer.Serialize(this._key), (object)scriptSerializer.Serialize(this._label));
        }
    }
    public class TimelineView : SchedulerView
    {
        protected List<TimelineUnit> _units = new List<TimelineUnit>();
        private ServerListHelper<TimelineUnit> m_serverList;

        public string ServerList
        {
            get
            {
                return this.m_serverList.ServerListLink;
            }
            set
            {
                this.m_serverList.ServerListLink = value;
            }
        }

        public TimelineView(string name, string y_property)
        {
            this._hiddenProperties.Add("label");
            this._hiddenProperties.Add("tabPosition");
            this._hiddenProperties.Add("y_unit");
            this._hiddenProperties.Add("view_type");
            this._hiddenProperties.Add("second_scale");
            this._hiddenProperties.Add("full_event_dy");
            this._hiddenProperties.Add("event_dy");
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          "section_autoheight",
          (object) true
        },
        {
          "x_step",
          (object) 1
        },
        {
          "y_step",
          (object) 1
        },
        {
          "x_start",
          (object) 0
        },
        {
          "x_size",
          (object) 24
        },
        {
          "y_start",
          (object) 0
        },
        {
          "y_size",
          (object) 7
        },
        {
          "dx",
          (object) 200
        },
        {
          "dy",
          (object) 50
        }
      });
            this.FitEvents = true;
            this.ViewType = "timeline";
            this.Set(nameof(name), (object)name);
            this.Label = "Timeline";
            this.Y_Property = y_property;
            this.TabPosition = 280;
            this.m_serverList = new ServerListHelper<TimelineUnit>(new Action<IEnumerable>(this._AddOptions), (IList<TimelineUnit>)this._units);
        }

        public void AddSecondScale(TimelineView.XScaleUnits x_unit, string x_date)
        {
            this.Set("second_scale", (object)("{x_unit:\"" + x_unit.ToString("g").ToLower() + "\", x_date:\"" + x_date + "\"}"));
        }

        public TimelineView.RenderModes RenderMode
        {
            get
            {
                string str = this.Get("render");
                if (str == "bar")
                    return TimelineView.RenderModes.Bar;
                if (str == "tree")
                    return TimelineView.RenderModes.Tree;
                return str == "cell" ? TimelineView.RenderModes.Cell : TimelineView.RenderModes.Bar;
            }
            set
            {
                switch (value)
                {
                    case TimelineView.RenderModes.Bar:
                        this.Set("render", (object)"bar");
                        break;
                    case TimelineView.RenderModes.Cell:
                        this.Set("render", (object)"cell");
                        break;
                    case TimelineView.RenderModes.Tree:
                        this.Set("render", (object)"tree");
                        break;
                }
            }
        }

        public int Dy
        {
            get
            {
                return this.GetInt("dy");
            }
            set
            {
                this.Set("dy", (object)value);
            }
        }

        public int Dx
        {
            get
            {
                return this.GetInt("dx");
            }
            set
            {
                this.Set("dx", (object)value);
            }
        }

        public int EventDy
        {
            get
            {
                return this.GetInt("event_dy");
            }
            set
            {
                this.Set("event_dy", (object)value);
            }
        }

        public int EventMinDy
        {
            get
            {
                return this.GetInt("event_min_dy");
            }
            set
            {
                this.Set("event_min_dy", (object)value);
            }
        }

        public bool FullEventDy
        {
            get
            {
                return this.GetBool("full_event__dy");
            }
            set
            {
                this.Set("full_event__dy", (object)value);
            }
        }

        public bool SectionAutoheight
        {
            get
            {
                return this.GetBool("section_autoheight");
            }
            set
            {
                this.Set("section_autoheight", (object)value);
            }
        }

        public bool FolderEventsAvailable
        {
            get
            {
                return this.GetBool("folder_events_available");
            }
            set
            {
                this.Set("folder_events_available", (object)value);
            }
        }

        public bool FitEvents
        {
            get
            {
                return this.GetBool("fit_events");
            }
            set
            {
                this.Set("fit_events", (object)value);
            }
        }

        public bool ResizeEvents
        {
            get
            {
                return this.GetBool("resize_events");
            }
            set
            {
                this.Set("resize_events", (object)value);
            }
        }

        public int FolderDy
        {
            get
            {
                return this.GetInt("folder_dy");
            }
            set
            {
                this.Set("folder_dy", (object)value);
            }
        }

        public TimelineView.XScaleUnits X_Unit
        {
            get
            {
                string str = this.Get("x_unit");
                if (str == "day")
                    return TimelineView.XScaleUnits.Day;
                if (str == "hour")
                    return TimelineView.XScaleUnits.Hour;
                if (str == "month")
                    return TimelineView.XScaleUnits.Month;
                if (str == "week")
                    return TimelineView.XScaleUnits.Week;
                return str == "minute" ? TimelineView.XScaleUnits.Minute : TimelineView.XScaleUnits.Year;
            }
            set
            {
                this.Set("x_unit", (object)value.ToString("g").ToLower());
            }
        }

        public int X_Step
        {
            get
            {
                return this.GetInt("x_step");
            }
            set
            {
                this.Set("x_step", (object)value);
            }
        }

        public int X_Size
        {
            get
            {
                return this.GetInt("x_size");
            }
            set
            {
                this.Set("x_size", (object)value);
            }
        }

        public int X_Start
        {
            get
            {
                return this.GetInt("x_start");
            }
            set
            {
                this.Set("x_start", (object)value);
            }
        }

        public int X_Length
        {
            get
            {
                return this.GetInt("x_length");
            }
            set
            {
                this.Set("x_length", (object)value);
            }
        }

        public string Y_Property
        {
            get
            {
                return this.Get("y_property");
            }
            set
            {
                this.Set("y_property", (object)value);
            }
        }

        public string X_Date
        {
            get
            {
                return this.Get("x_date");
            }
            set
            {
                this.Set("x_date", (object)value);
            }
        }

        public TimelineUnit AddOption(TimelineUnit unit)
        {
            this.m_serverList.AddOption(unit);
            return unit;
        }

        public TimelineUnit AddOption(object unit)
        {
            PropertyInfo[] properties = unit.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "open"));
            PropertyInfo propertyInfo4 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "children"));
            if (propertyInfo1 == null && propertyInfo2 == null)
                return (TimelineUnit)null;
            TimelineUnit timelineUnit = this.AddOption(new TimelineUnit(propertyInfo1.GetValue(unit, (object[])null), propertyInfo2.GetValue(unit, (object[])null), propertyInfo3 != null && (bool)propertyInfo3.GetValue(unit, (object[])null)));
            IEnumerable<object> objects = propertyInfo3.GetValue(unit, (object[])null) as IEnumerable<object>;
            if (propertyInfo4 != null && objects != null)
                timelineUnit.AddOptions((IEnumerable)objects);
            return timelineUnit;
        }

        public void AddOptions(IEnumerable units)
        {
            this.m_serverList.AddOptions(units);
        }

        public void AddOptions(IEnumerable opts, bool direct)
        {
            if (direct)
                this._AddOptions(opts);
            else
                this.AddOptions(opts);
        }

        protected void _AddOptions(IEnumerable units)
        {
            object obj = (object)null;
            foreach (object unit1 in units)
            {
                TimelineUnit unit2 = unit1 as TimelineUnit;
                if (unit2 != null)
                {
                    this.AddOption(unit2);
                }
                else
                {
                    obj = unit1;
                    break;
                }
            }
            if (obj == null)
                return;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "open"));
            PropertyInfo propertyInfo4 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "children"));
            if (propertyInfo1 == null && propertyInfo2 == null)
                return;
            foreach (object unit in units)
            {
                TimelineUnit timelineUnit = this.AddOption(new TimelineUnit(propertyInfo1.GetValue(unit, (object[])null), propertyInfo2.GetValue(unit, (object[])null), propertyInfo3 != null && (bool)propertyInfo3.GetValue(unit, (object[])null)));
                IEnumerable<object> objects = propertyInfo3.GetValue(unit, (object[])null) as IEnumerable<object>;
                if (propertyInfo4 != null && objects != null)
                    timelineUnit.AddOptions((IEnumerable)objects);
            }
        }

        public override Dictionary<string, FileType> GetJS()
        {
            Dictionary<string, FileType> dictionary = new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_timeline.js",
          FileType.Local
        }
      };
            if (this.RenderMode == TimelineView.RenderModes.Tree)
                dictionary.Add("ext/dhtmlxscheduler_treetimeline.js", FileType.Local);
            return dictionary;
        }

        public override Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = base.Serialize();
            object[] objArray = new object[this._units.Count];
            for (int index = 0; index < this._units.Count; ++index)
                objArray[index] = (object)this._units[index].Serialize();
            dictionary.Add("y_unit", (object)objArray);
            return dictionary;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            foreach (Dictionary<string, object> data1 in data["y_unit"] as object[])
            {
                TimelineUnit unit = new TimelineUnit((object)"1", (object)"1", true);
                unit.Deserialize(data1);
                this.AddOption(unit);
            }
        }

        public List<string> RenderItems()
        {
            List<string> stringList = new List<string>();
            foreach (TimelineUnit unit in this._units)
                stringList.Add(unit.Render());
            return stringList;
        }

        public override void Render(StringBuilder builder, string parent)
        {
            base.Render(builder, parent);
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            Dictionary<string, object> visibleProperties = this.GetVisibleProperties();
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in visibleProperties)
                stringList.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, (object)scriptSerializer.Serialize(keyValuePair.Value)));
            if (this.FullEventDy)
                stringList.Add("\"event_dy\":\"full\"");
            else if (this.EventDy > 0)
                stringList.Add(string.Format("\"event_dy\":{0}", (object)this.EventDy));
            if (this._properties.ContainsKey("second_scale"))
                stringList.Add("\"second_scale\":" + this.Get("second_scale"));
            if (this.m_serverList.IsEmptyServerList)
            {
                stringList.Add(string.Format("\"y_unit\":[{0}]", (object)string.Join(",", this.RenderItems().ToArray())));
            }
            else
            {
                stringList.Add("\"y_unit\":" + this.m_serverList.RenderCollection(parent));
                builder.Append("\n" + this.m_serverList.RenderConfig(parent, this.Name));
            }
            builder.Append(string.Format("\n{0}.createTimelineView({{{1}\n}});", (object)parent, (object)string.Join(",\n", stringList.ToArray())));
        }

        public enum RenderModes
        {
            Bar,
            Cell,
            Tree,
        }

        public enum XScaleUnits
        {
            Minute,
            Hour,
            Day,
            Week,
            Month,
            Year,
        }
    }
    public class TimelineUnit
    {
        protected List<TimelineUnit> elements = new List<TimelineUnit>();
        protected object label;
        protected bool open;
        protected object key;

        public List<TimelineUnit> Items
        {
            get
            {
                return this.elements;
            }
        }

        public TimelineUnit(object key, object label, bool open = false)
        {
            this.key = key;
            this.label = label;
            this.open = open;
        }

        public TimelineUnit AddOption(TimelineUnit unit)
        {
            this.elements.Add(unit);
            return unit;
        }

        public TimelineUnit AddOption(object unit)
        {
            PropertyInfo[] properties = unit.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "open"));
            PropertyInfo propertyInfo4 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "children"));
            if (propertyInfo1 == null && propertyInfo2 == null)
                return (TimelineUnit)null;
            TimelineUnit timelineUnit = this.AddOption(new TimelineUnit(propertyInfo1.GetValue(unit, (object[])null), propertyInfo2.GetValue(unit, (object[])null), propertyInfo3 != null && (bool)propertyInfo3.GetValue(unit, (object[])null)));
            IEnumerable<object> objects = propertyInfo3.GetValue(unit, (object[])null) as IEnumerable<object>;
            if (propertyInfo4 != null && objects != null)
                timelineUnit.AddOptions((IEnumerable)objects);
            return timelineUnit;
        }

        public void AddOptions(IEnumerable units)
        {
            object obj = (object)null;
            foreach (object unit in units)
            {
                TimelineUnit timelineUnit = unit as TimelineUnit;
                if (timelineUnit != null)
                {
                    this.elements.Add(timelineUnit);
                }
                else
                {
                    obj = unit;
                    break;
                }
            }
            if (obj == null)
                return;
            PropertyInfo[] properties = obj.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
            PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
            PropertyInfo propertyInfo3 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "open"));
            PropertyInfo propertyInfo4 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "children"));
            if (propertyInfo1 == null && propertyInfo2 == null)
                return;
            foreach (object unit in units)
            {
                IEnumerable<object> objects = propertyInfo3.GetValue(unit, (object[])null) as IEnumerable<object>;
                TimelineUnit timelineUnit = this.AddOption(new TimelineUnit(propertyInfo1.GetValue(unit, (object[])null), propertyInfo2.GetValue(unit, (object[])null), propertyInfo3 != null && (bool)propertyInfo3.GetValue(unit, (object[])null)));
                if (propertyInfo4 != null && objects != null)
                    timelineUnit.AddOptions((IEnumerable)objects);
            }
        }

        public string Render()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("label", this.label.ToString());
            dictionary.Add("key", this.key.ToString());
            List<string> stringList1 = new List<string>();
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            if (this.elements.Count != 0)
            {
                dictionary.Add("open", this.open ? "true" : "false");
                List<string> stringList2 = new List<string>();
                foreach (TimelineUnit element in this.elements)
                    stringList2.Add(element.Render());
                stringList1.Add(string.Format("\"children\":[{0}]", (object)string.Join(",", stringList2.ToArray())));
            }
            string str = "{";
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                stringList1.Add(string.Format("\"{0}\":\"{1}\"", (object)keyValuePair.Key, (object)keyValuePair.Value));
            return str + string.Join(",\n", stringList1.ToArray()) + "}";
        }

        public Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary.Add("key", this.key);
            dictionary.Add("label", this.label);
            dictionary.Add("open", (object)this.open);
            if (this.elements.Count != 0)
            {
                object[] objArray = new object[this.elements.Count];
                for (int index = 0; index < this.elements.Count; ++index)
                    objArray[index] = (object)this.elements[index].Serialize();
                dictionary.Add("children", (object)objArray);
            }
            return dictionary;
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            this.key = (object)data["key"].ToString();
            this.label = (object)data["label"].ToString();
            this.elements.Clear();
            if (!data.ContainsKey("children"))
                return;
            foreach (Dictionary<string, object> data1 in data["children"] as object[])
            {
                TimelineUnit unit = new TimelineUnit((object)"1", (object)"1", false);
                unit.Deserialize(data1);
                this.AddOption(unit);
            }
        }
    }
    internal class ServerListHelper<T>
    {
        protected IList<T> options;
        protected string m_serverListLink;
        protected Action<IEnumerable> _AddOptions;

        public string ServerListLink
        {
            get
            {
                return this.m_serverListLink;
            }
            set
            {
                this.m_serverListLink = value;
            }
        }

        public bool IsEmptyServerList
        {
            get
            {
                return this._IsEmptyList();
            }
        }

        public ServerListHelper(Action<IEnumerable> addOptions, IList<T> _optionsContainer)
        {
            this._AddOptions = addOptions;
            this.options = _optionsContainer;
        }

        public string RenderCollection(string name)
        {
            return string.Format(DHXScheduler.GetServerList().CollectionLink, (object)name, (object)this.ServerListLink);
        }

        public string RenderConfig(string name, string parent)
        {
            return string.Format(DHXScheduler.GetServerList().ConfigSection, (object)name, (object)parent, (object)this.ServerListLink);
        }

        public void AddOption(T option)
        {
            this.options.Add(option);
        }

        public void AddOptions(IEnumerable opts)
        {
            if (!this._IsEmptyList())
            {
                this._AddToServerList(opts);
            }
            else
            {
                this._GrabOptions();
                this._AddOptions(opts);
            }
        }

        private void _AddToServerList(IEnumerable opts)
        {
            this.m_serverListLink = DHXScheduler.GetServerList().Add(opts);
        }

        private bool _IsEmptyList()
        {
            return this.options.Count != 0;
        }

        private void _GrabOptions()
        {
            if (!string.IsNullOrEmpty(this.m_serverListLink))
                this._AddOptions(DHXScheduler.GetServerList().Get(this.m_serverListLink));
            this.m_serverListLink = (string)null;
        }
    }
    public class SchedulerViewsController : SchedulerControlsBase
    {
        protected int diff = 64;
        protected int baseTabPos = 76;

        public SchedulerViewsController()
        {
            this.Add((SchedulerView)new MonthView());
            this.Add((SchedulerView)new WeekView());
            this.Add((SchedulerView)new DayView());
        }

        public virtual void Add(SchedulerView obj)
        {
            obj.TabPosition = this.baseTabPos + this.data.Count * this.diff;
            this.data.Add((SchedulerControlsItem)obj);
        }

        public virtual int Count
        {
            get
            {
                return this.data.Count<SchedulerControlsItem>();
            }
        }

        public SchedulerView this[int index]
        {
            get
            {
                return this.data[index] as SchedulerView;
            }
            set
            {
                this.data[index] = (SchedulerControlsItem)value;
            }
        }

        protected override void _renderContent(StringBuilder builder, string parent)
        {
            foreach (SchedulerControlsItem schedulerControlsItem in this.data)
            {
                builder.Append("\n");
                (schedulerControlsItem as SchedulerView).Render(builder, parent);
                builder.Append("\n");
            }
        }

        public List<SchedulerView> GetViews()
        {
            List<SchedulerView> schedulerViewList = new List<SchedulerView>();
            foreach (SchedulerControlsItem schedulerControlsItem in this.data)
                schedulerViewList.Add(schedulerControlsItem as SchedulerView);
            return schedulerViewList;
        }

        internal override object[] Serialize()
        {
            object[] objArray = new object[this.data.Count];
            for (int index = 0; index < this.data.Count; ++index)
                objArray[index] = (object)this.data[index].Serialize();
            return objArray;
        }

        internal override void Deserialize(object data)
        {
            this.Clear();
            object[] objArray = data as object[];
            ((IEnumerable<object>)objArray).Count<object>();
            foreach (Dictionary<string, object> data1 in objArray)
            {
                if (data1.ContainsKey("view_type"))
                {
                    SchedulerView schedulerView = SchedulerViewFactory.Create(data1["view_type"].ToString(), data1["name"].ToString());
                    schedulerView.Deserialize(data1);
                    this.Add(schedulerView);
                }
            }
        }
    }
    public class SchedulerViewFactory
    {
        public static SchedulerView Create(string type, string name = null)
        {
            switch (type)
            {
                case "unit":
                    return (SchedulerView)new UnitsView(name, name);
                case "year":
                    return (SchedulerView)new YearView();
                case "agenda":
                    return (SchedulerView)new AgendaView();
                case "week_agenda":
                    return (SchedulerView)new WeekAgendaView();
                case "timeline":
                    return (SchedulerView)new TimelineView(name, name);
                case "map":
                    return (SchedulerView)new MapView();
                case "day":
                    return (SchedulerView)new DayView();
                case "week":
                    return (SchedulerView)new WeekView();
                case "month":
                    return (SchedulerView)new MonthView();
                case "grid":
                    return (SchedulerView)new GridView();
                default:
                    throw new Exception("Unknown type " + type);
            }
        }
    }
    public class SchedulerView : SchedulerControlsItem
    {
        private Filter _Filter;

        public SchedulerView()
        {
            this._hiddenProperties.Add("label");
            this._hiddenProperties.Add("tabPosition");
            this._hiddenProperties.Add("view_type");
            this._hiddenProperties.Add("label_width");
            this._hiddenProperties.Add("label_css");
            this.ViewType = "day";
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

        public string ViewType
        {
            get
            {
                return this.Get("view_type");
            }
            protected set
            {
                this.Set("view_type", (object)value);
            }
        }

        public int TabPosition
        {
            get
            {
                return this.GetInt("tabPosition");
            }
            set
            {
                this.Set("tabPosition", (object)value);
            }
        }

        public int TabWidth
        {
            get
            {
                return this.GetInt("label_width");
            }
            set
            {
                this.Set("label_width", (object)value);
            }
        }

        public string TabClass
        {
            get
            {
                return this.Get("label_css");
            }
            set
            {
                this.Set("label_css", (object)value);
            }
        }

        public string TabStyle
        {
            get
            {
                return this.Get("label_style");
            }
            set
            {
                this.Set("label_style", (object)value);
            }
        }

        public virtual void RenderLabel(StringBuilder builder, string parent)
        {
            if (this.Label == null || this.Name == null)
                return;
            builder.Append(string.Format("\n{0}.locale.labels.{1}_tab = '{2}';", (object)parent, (object)this.Name, (object)this.Label));
        }

        public virtual string RenderTab()
        {
            return this.RenderTab(DHXScheduler.Skins.Glossy);
        }

        public virtual string RenderTab(DHXScheduler.Skins skin)
        {
            return string.Format("<div class='dhx_cal_tab{0}' name='{1}_tab' style='{4}:{2}px;{3}{5}'></div>", string.IsNullOrEmpty(this.TabClass) ? (object)"" : (object)(" " + this.TabClass), (object)this.Name, (object)this.TabPosition, this.TabWidth == 0 ? (object)"" : (object)("width:" + this.TabWidth.ToString() + "px;"), skin == DHXScheduler.Skins.Terrace ? (object)"left" : (object)"right", (object)this.TabStyle);
        }

        public Filter Filter
        {
            get
            {
                if (this._Filter == null)
                    this._Filter = new Filter(this.Name);
                return this._Filter;
            }
            set
            {
                this._Filter = value;
            }
        }

        public virtual void Render(StringBuilder builder, string parent)
        {
            if (this._Filter != null)
                this.Filter.Render(builder, parent);
            this.RenderLabel(builder, parent);
        }
    }
}

