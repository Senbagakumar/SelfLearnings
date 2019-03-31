using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public enum Operator
    {
        Equals,
        NotEquals,
        Identical,
        NotIdentical,
        Greater,
        GreaterOrEqual,
        Lower,
        LowerOrEqual,
        GreaterOrIdentical,
        LowerOrIdentical,
    }
    public class MonthView : SchedulerView
    {
        public MonthView()
        {
            this.ViewType = "month";
            this.Set("name", (object)"month");
            this.TabPosition = 76;
        }
    }
    public class MiniCalendarManager : SchedulerControlsBase
    {
        protected string defaultContainer = "dhx_minical_icon";
        protected bool defaultNavigation = true;
        protected DateTime defaultDate = new DateTime();
        protected string after_init = "";
        protected string defaultDateString;

        public virtual void Add(MiniCalendar obj)
        {
            this.data.Add((SchedulerControlsItem)obj);
        }

        public MiniCalendar AttachMiniCalendar()
        {
            return this.AttachMiniCalendar(this.defaultDate, this.defaultNavigation, this.defaultContainer);
        }

        public MiniCalendar AttachMiniCalendar(DateTime date)
        {
            return this.AttachMiniCalendar(date, this.defaultNavigation, this.defaultContainer);
        }

        public MiniCalendar AttachMiniCalendar(bool navigation)
        {
            return this.AttachMiniCalendar(this.defaultDate, navigation, this.defaultContainer);
        }

        public MiniCalendar AttachMiniCalendar(string position)
        {
            return this.AttachMiniCalendar(this.defaultDate, this.defaultNavigation, position);
        }

        public MiniCalendar AttachMiniCalendar(DateTime date, bool navigation, string position)
        {
            MiniCalendar miniCalendar = new MiniCalendar();
            miniCalendar.Navigation = navigation;
            miniCalendar.Date = date;
            miniCalendar.AttachToHeader(position);
            this.Add(miniCalendar);
            return miniCalendar;
        }

        public string RenderTab(string parent)
        {
            return this.RenderTab(DHXScheduler.Skins.Glossy, parent);
        }

        public string RenderTab(DHXScheduler.Skins skin, string parent)
        {
            foreach (MiniCalendar miniCalendar in this.data)
            {
                if (miniCalendar.IsAttachedToHeader)
                {
                    miniCalendar.Position = parent + miniCalendar.Position;
                    return string.Format("<div class=\"{0}\" id=\"{1}\" onclick=\"{2}.__show_minical()\">&nbsp;</div>", (object)miniCalendar.IconClass, (object)miniCalendar.Position, (object)parent);
                }
            }
            return "";
        }

        internal override string AfterInit()
        {
            return this.after_init;
        }

        public override void Render(StringBuilder builder, string parent)
        {
            this.defaultDateString = parent + "._date";
            foreach (MiniCalendar cal in this.data)
            {
                if (cal.IsAttachedToHeader)
                {
                    builder.Append(string.Format("{0}.__show_minical = function(){{\r\n      if ({0}.isCalendarVisible())\r\n         {0}.destroyCalendar();\r\n      else", (object)parent));
                    builder.Append(this._renderCalendar(cal, parent));
                    builder.Append("\n }");
                }
                else
                    this.after_init += this._renderCalendar(cal, parent);
            }
        }

        protected string _renderCalendar(MiniCalendar cal, string parent)
        {
            Dictionary<string, object> visibleProperties = cal.GetVisibleProperties();
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in visibleProperties)
            {
                if (keyValuePair.Key != "date")
                    stringList.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, (object)scriptSerializer.Serialize(keyValuePair.Value)));
                else
                    stringList.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, cal.Date == new DateTime() ? (object)this.defaultDateString : (object)string.Format("new Date({0},{1},{2})", (object)cal.Date.Year, (object)(cal.Date.Month - 1), (object)cal.Date.Day)));
            }
            stringList.Add("\"handler\":" + string.Format(cal.Handler, (object)parent));
            return string.Format("\n{0}.renderCalendar({{{1}\n}});", (object)parent, (object)string.Join(",\n", stringList.ToArray()));
        }
    }
}

