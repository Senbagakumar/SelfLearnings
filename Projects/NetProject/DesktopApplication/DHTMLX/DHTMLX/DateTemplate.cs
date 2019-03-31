using DHTMLX.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DHTMLX.Scheduler.Settings
{
    public static class TimelineScaleTemplate
    {
        public static string Render(string str)
        {
            return string.Format("function(section_id, section_label, section_options){{\n\tvar tpl = \"\";{0} return tpl;\n}};", (object)Template.CollectParts("tpl", Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceVariable))));
        }
    }
    public static class TimelineCellTemplate
    {
        public static string Render(string str)
        {
            return string.Format("function(evs, date, section){{\n\tvar tpl = \"\";{0} return tpl;\n}};", (object)Template.CollectParts("tpl", Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceVariable))));
        }
    }
    public static class Template
    {
        public static Regex PropertyTemplate = new Regex("([a-zA-Z_0-9\\(\\)\\[\\]\\.\\+\\-\\*\\/ ]+)(:([a-zA-Z_0-9]+)\\(([^)]*)\\))?");

        public static string DateToString(string variable, string dateFormat)
        {
            return string.Format("scheduler.date.date_to_str(\"{0}\")({1})", (object)variable, (object)dateFormat);
        }

        public static string ReplaceVariable(Match match)
        {
            return Template.ReplaceVar(match, (string)null);
        }

        public static string ReplaceEvent(Match match)
        {
            return Template.ReplaceVar(match, "ev");
        }

        public static string CollectParts(string varName, List<string> parts)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < parts.Count; ++index)
            {
                if (parts[index].StartsWith("\""))
                {
                    stringBuilder.Append(varName);
                    stringBuilder.Append(" += ");
                }
                stringBuilder.Append(parts[index]);
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public static string ReplaceVar(Match match, string var)
        {
            string format = string.IsNullOrEmpty(var) ? "{0}" : string.Format("{0}.{{0}}", (object)var);
            if (string.IsNullOrEmpty(match.Groups[3].Value))
                return string.Format(format, (object)match.Groups[1].Value);
            switch (match.Groups[3].Value)
            {
                case "date":
                    return Template.DateToString(match.Groups[4].Value, string.Format(format, (object)match.Groups[1].Value));
                default:
                    return string.Format(format, (object)match.Groups[1].Value);
            }
        }

        public static List<string> Parse(string template, string delemiter, MatchEvaluator rep)
        {
            List<string> stringList = new List<string>();
            StringBuilder stringBuilder1 = new StringBuilder();
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            StringBuilder stringBuilder2 = new StringBuilder();
            stringBuilder1.Append("\"");
            for (int index = 0; index < template.Length; ++index)
            {
                char ch = template[index];
                if (ch == '{' && !flag1 && !flag3)
                {
                    flag4 = flag2 = true;
                    flag5 = false;
                }
                else if (ch == '<' && !flag2 && (index + 1 < template.Length && template[index + 1] == '%'))
                {
                    if (index + 2 < template.Length && template[index + 2] == '=')
                    {
                        flag4 = flag3 = true;
                        ++index;
                    }
                    else
                        flag4 = flag1 = true;
                    ++index;
                    flag5 = false;
                }
                else if (ch == '}' && flag2)
                {
                    string str = stringBuilder2.ToString();
                    stringBuilder2.Remove(0, stringBuilder2.Length);
                    if (str.Length > 0)
                    {
                        stringBuilder1.Append(delemiter);
                        stringBuilder1.Append(" + ");
                        stringBuilder1.Append(Template.PropertyTemplate.Replace(str.Replace(Environment.NewLine, ""), rep));
                        stringBuilder1.Append(" + ");
                        stringBuilder1.Append(delemiter);
                    }
                    int num;
                    flag2 = (num = 0) != 0;
                    flag4 = num != 0;
                    flag5 = num != 0;
                }
                else if (ch == '%' && index + 1 < template.Length && template[index + 1] == '>' && (flag1 || flag3))
                {
                    string str = stringBuilder2.ToString();
                    ++index;
                    stringBuilder2.Remove(0, stringBuilder2.Length);
                    if (flag1)
                    {
                        if (str.Length > 0)
                        {
                            stringBuilder1.Append(delemiter);
                            stringBuilder1.Append(";");
                            stringList.Add(stringBuilder1.ToString());
                            stringList.Add(str);
                            stringBuilder1.Remove(0, stringBuilder1.Length);
                            stringBuilder1.Append(delemiter);
                        }
                        int num;
                        flag1 = (num = 0) != 0;
                        flag4 = num != 0;
                        flag5 = num != 0;
                    }
                    else
                    {
                        stringBuilder1.Append(delemiter);
                        stringBuilder1.Append(" + (");
                        stringBuilder1.Append(str);
                        stringBuilder1.Append(") + ");
                        stringBuilder1.Append(delemiter);
                        int num;
                        flag3 = (num = 0) != 0;
                        flag4 = num != 0;
                        flag5 = num != 0;
                    }
                }
                else
                {
                    if (!flag4)
                    {
                        if (ch == '\t' || ch == '\n' || (ch == '\r' || ch == '\f') || ch == '\b')
                            stringBuilder1.Append('\\');
                        else if (ch.ToString() == delemiter && !flag5)
                            stringBuilder1.Append('\\');
                        stringBuilder1.Append(ch);
                    }
                    else
                    {
                        if (ch.ToString() == delemiter && !flag5)
                            stringBuilder2.Append('\\');
                        stringBuilder2.Append(ch);
                    }
                    flag5 = ch == '\\';
                }
            }
            stringBuilder1.Append(delemiter);
            stringBuilder1.Append(";");
            stringList.Add(stringBuilder1.ToString());
            return stringList;
        }

        public static List<string> ProcessExpression(string str)
        {
            return Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceVariable));
        }
    }
    public class SchedulerXY : SchedulerSettingsBase
    {
        public int bar_height
        {
            get
            {
                return this.getIntProp(nameof(bar_height));
            }
            set
            {
                this.setProp(nameof(bar_height), (object)value);
            }
        }

        public int editor_width
        {
            get
            {
                return this.getIntProp(nameof(editor_width));
            }
            set
            {
                this.setProp(nameof(editor_width), (object)value);
            }
        }

        public int margin_left
        {
            get
            {
                return this.getIntProp(nameof(margin_left));
            }
            set
            {
                this.setProp(nameof(margin_left), (object)value);
            }
        }

        public int margin_top
        {
            get
            {
                return this.getIntProp(nameof(margin_top));
            }
            set
            {
                this.setProp(nameof(margin_top), (object)value);
            }
        }

        public int menu_width
        {
            get
            {
                return this.getIntProp(nameof(menu_width));
            }
            set
            {
                this.setProp(nameof(menu_width), (object)value);
            }
        }

        public int min_event_height
        {
            get
            {
                return this.getIntProp(nameof(min_event_height));
            }
            set
            {
                this.setProp(nameof(min_event_height), (object)value);
            }
        }

        public int month_scale_height
        {
            get
            {
                return this.getIntProp(nameof(month_scale_height));
            }
            set
            {
                this.setProp(nameof(month_scale_height), (object)value);
            }
        }

        public int nav_height
        {
            get
            {
                return this.getIntProp(nameof(nav_height));
            }
            set
            {
                this.setProp(nameof(nav_height), (object)value);
            }
        }

        public int scale_height
        {
            get
            {
                return this.getIntProp(nameof(scale_height));
            }
            set
            {
                this.setProp(nameof(scale_height), (object)value);
            }
        }

        public int scale_width
        {
            get
            {
                return this.getIntProp(nameof(scale_width));
            }
            set
            {
                this.setProp(nameof(scale_width), (object)value);
            }
        }

        public int scroll_width
        {
            get
            {
                return this.getIntProp(nameof(scroll_width));
            }
            set
            {
                this.setProp(nameof(scroll_width), (object)value);
            }
        }

        public int map_date_width
        {
            get
            {
                return this.getIntProp(nameof(map_date_width));
            }
            set
            {
                this.setProp(nameof(map_date_width), (object)value);
            }
        }

        public int map_description_width
        {
            get
            {
                return this.getIntProp(nameof(map_description_width));
            }
            set
            {
                this.setProp(nameof(map_description_width), (object)value);
            }
        }

        public int year_top
        {
            get
            {
                return this.getIntProp(nameof(year_top));
            }
            set
            {
                this.setProp(nameof(year_top), (object)value);
            }
        }

        public int week_agenda_scale_height
        {
            get
            {
                return this.getIntProp(nameof(week_agenda_scale_height));
            }
            set
            {
                this.setProp(nameof(week_agenda_scale_height), (object)value);
            }
        }

        public int lightbox_additional_height
        {
            get
            {
                return this.getIntProp(nameof(lightbox_additional_height));
            }
            set
            {
                this.setProp(nameof(lightbox_additional_height), (object)value);
            }
        }

        public SchedulerXY()
        {
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          nameof (bar_height),
          (object) 20
        },
        {
          nameof (editor_width),
          (object) 140
        },
        {
          nameof (margin_left),
          (object) 0
        },
        {
          nameof (margin_top),
          (object) 0
        },
        {
          nameof (menu_width),
          (object) 25
        },
        {
          nameof (min_event_height),
          (object) 40
        },
        {
          nameof (month_scale_height),
          (object) 20
        },
        {
          nameof (nav_height),
          (object) 22
        },
        {
          nameof (scale_height),
          (object) 20
        },
        {
          nameof (scale_width),
          (object) 50
        },
        {
          nameof (scroll_width),
          (object) 18
        },
        {
          nameof (map_date_width),
          (object) 188
        },
        {
          nameof (map_description_width),
          (object) 400
        },
        {
          nameof (year_top),
          (object) 0
        },
        {
          nameof (week_agenda_scale_height),
          (object) 20
        },
        {
          nameof (lightbox_additional_height),
          (object) 90
        }
      });
        }

        public override void Render(StringBuilder builder, string parent)
        {
            this._prefix = parent + ".xy.";
            base.Render(builder, parent);
        }
    }
    public class SchedulerTemplates : SchedulerSettingsBase
    {
        protected Dictionary<SchedulerTemplates.TemplateTypes, List<string>> _templateTypes = new Dictionary<SchedulerTemplates.TemplateTypes, List<string>>()
    {
      {
        SchedulerTemplates.TemplateTypes.Date,
        new List<string>()
        {
          nameof (day_date),
          nameof (month_date),
          nameof (day_scale_date),
          nameof (week_scale_date),
          nameof (month_scale_date),
          nameof (month_day),
          nameof (hour_scale),
          nameof (time_picker),
          nameof (xml_date),
          nameof (xml_format),
          nameof (api_date),
          nameof (timeline_scalex_class),
          nameof (timeline_second_scalex_class),
          nameof (timeline_scale_date),
          nameof (timeline_second_scale_date),
          nameof (year_month),
          nameof (year_date),
          nameof (year_scale_date),
          nameof (marker_date)
        }
      },
      {
        SchedulerTemplates.TemplateTypes.Period,
        new List<string>()
        {
          nameof (week_date),
          nameof (timeline_date)
        }
      },
      {
        SchedulerTemplates.TemplateTypes.Event,
        new List<string>()
        {
          nameof (event_header),
          nameof (event_text),
          nameof (event_class),
          nameof (event_bar_date),
          nameof (event_bar_text),
          nameof (lightbox_header),
          nameof (event_class),
          nameof (month_date_class),
          nameof (week_date_class),
          nameof (tooltip_text),
          nameof (week_agenda_event_text),
          nameof (year_tooltip),
          nameof (timeline_tooltip),
          nameof (agenda_time),
          nameof (agenda_text),
          nameof (map_time),
          nameof (map_text),
          nameof (marker_text)
        }
      },
      {
        SchedulerTemplates.TemplateTypes.Timeline,
        new List<string>()
        {
          nameof (timeline_scale_label),
          nameof (timeline_scaley_class)
        }
      },
      {
        SchedulerTemplates.TemplateTypes.TimelineCell,
        new List<string>()
        {
          nameof (timeline_cell_class),
          nameof (timeline_cell_value)
        }
      }
    };

        protected SchedulerTemplates.TemplateTypes _getType(string name)
        {
            SchedulerTemplates.TemplateTypes templateTypes = SchedulerTemplates.TemplateTypes.Invalid;
            foreach (KeyValuePair<SchedulerTemplates.TemplateTypes, List<string>> templateType in this._templateTypes)
            {
                if (templateType.Value.Contains(name))
                {
                    templateTypes = templateType.Key;
                    break;
                }
            }
            return templateTypes;
        }

        protected string _renderTemplate(string name, string value)
        {
            string str;
            switch (this._getType(name))
            {
                case SchedulerTemplates.TemplateTypes.Date:
                    str = DateTemplate.Render(value);
                    break;
                case SchedulerTemplates.TemplateTypes.Period:
                    str = PeriodTemplate.Render(value);
                    break;
                case SchedulerTemplates.TemplateTypes.Event:
                    str = EventTemplate.Render(value);
                    break;
                case SchedulerTemplates.TemplateTypes.Timeline:
                    str = TimelineScaleTemplate.Render(value);
                    break;
                case SchedulerTemplates.TemplateTypes.TimelineCell:
                    str = TimelineCellTemplate.Render(value);
                    break;
                default:
                    throw new Exception("Unkown template " + name);
            }
            return string.Format("{0};", (object)str);
        }

        public override void Render(StringBuilder builder, string parent)
        {
            this._prefix = parent + ".templates.";
            foreach (string key in this._properties.GetKeys())
            {
                if (this._getType(key) == SchedulerTemplates.TemplateTypes.Invalid)
                    throw new Exception("Unknown template " + key);
                builder.Append("\n\t");
                builder.Append(this._prefix);
                builder.Append(key);
                builder.Append(" = ");
                builder.Append(this._renderTemplate(key, (string)this._properties[key]));
            }
            if (this.EventBox == null)
                return;
            this.EventBox.Render(builder, parent);
        }

        public DHXEventTemplate EventBox { get; set; }

        public string day_date
        {
            get
            {
                return (string)this._properties[nameof(day_date)];
            }
            set
            {
                this._properties[nameof(day_date)] = (object)value;
            }
        }

        public string week_date
        {
            get
            {
                return (string)this._properties[nameof(week_date)];
            }
            set
            {
                this._properties[nameof(week_date)] = (object)value;
            }
        }

        public string month_date
        {
            get
            {
                return (string)this._properties[nameof(month_date)];
            }
            set
            {
                this._properties[nameof(month_date)] = (object)value;
            }
        }

        public string day_scale_date
        {
            get
            {
                return (string)this._properties[nameof(day_scale_date)];
            }
            set
            {
                this._properties[nameof(day_scale_date)] = (object)value;
            }
        }

        public string week_scale_date
        {
            get
            {
                return (string)this._properties[nameof(week_scale_date)];
            }
            set
            {
                this._properties[nameof(week_scale_date)] = (object)value;
            }
        }

        public string month_scale_date
        {
            get
            {
                return (string)this._properties[nameof(month_scale_date)];
            }
            set
            {
                this._properties[nameof(month_scale_date)] = (object)value;
            }
        }

        public string month_day
        {
            get
            {
                return (string)this._properties[nameof(month_day)];
            }
            set
            {
                this._properties[nameof(month_day)] = (object)value;
            }
        }

        public string hour_scale
        {
            get
            {
                return (string)this._properties[nameof(hour_scale)];
            }
            set
            {
                this._properties[nameof(hour_scale)] = (object)value;
            }
        }

        public string time_picker
        {
            get
            {
                return (string)this._properties[nameof(time_picker)];
            }
            set
            {
                this._properties[nameof(time_picker)] = (object)value;
            }
        }

        public string xml_date
        {
            get
            {
                return (string)this._properties[nameof(xml_date)];
            }
            set
            {
                this._properties[nameof(xml_date)] = (object)value;
            }
        }

        public string xml_format
        {
            get
            {
                return (string)this._properties[nameof(xml_format)];
            }
            set
            {
                this._properties[nameof(xml_format)] = (object)value;
            }
        }

        public string api_date
        {
            get
            {
                return (string)this._properties[nameof(api_date)];
            }
            set
            {
                this._properties[nameof(api_date)] = (object)value;
            }
        }

        public string event_header
        {
            get
            {
                return (string)this._properties[nameof(event_header)];
            }
            set
            {
                this._properties[nameof(event_header)] = (object)value;
            }
        }

        public string event_text
        {
            get
            {
                return (string)this._properties[nameof(event_text)];
            }
            set
            {
                this._properties[nameof(event_text)] = (object)value;
            }
        }

        public string event_bar_date
        {
            get
            {
                return (string)this._properties[nameof(event_bar_date)];
            }
            set
            {
                this._properties[nameof(event_bar_date)] = (object)value;
            }
        }

        public string event_bar_text
        {
            get
            {
                return (string)this._properties[nameof(event_bar_text)];
            }
            set
            {
                this._properties[nameof(event_bar_text)] = (object)value;
            }
        }

        public string lightbox_header
        {
            get
            {
                return (string)this._properties[nameof(lightbox_header)];
            }
            set
            {
                this._properties[nameof(lightbox_header)] = (object)value;
            }
        }

        public string event_class
        {
            get
            {
                return (string)this._properties[nameof(event_class)];
            }
            set
            {
                this._properties[nameof(event_class)] = (object)value;
            }
        }

        public string month_date_class
        {
            get
            {
                return (string)this._properties[nameof(month_date_class)];
            }
            set
            {
                this._properties[nameof(month_date_class)] = (object)value;
            }
        }

        public string week_date_class
        {
            get
            {
                return (string)this._properties[nameof(week_date_class)];
            }
            set
            {
                this._properties[nameof(week_date_class)] = (object)value;
            }
        }

        public string tooltip_text
        {
            get
            {
                return (string)this._properties[nameof(tooltip_text)];
            }
            set
            {
                this._properties[nameof(tooltip_text)] = (object)value;
            }
        }

        public string agenda_time
        {
            get
            {
                return (string)this._properties[nameof(agenda_time)];
            }
            set
            {
                this._properties[nameof(agenda_time)] = (object)value;
            }
        }

        public string agenda_text
        {
            get
            {
                return (string)this._properties[nameof(agenda_text)];
            }
            set
            {
                this._properties[nameof(agenda_text)] = (object)value;
            }
        }

        public string year_month
        {
            get
            {
                return (string)this._properties[nameof(year_month)];
            }
            set
            {
                this._properties[nameof(year_month)] = (object)value;
            }
        }

        public string year_date
        {
            get
            {
                return (string)this._properties[nameof(year_date)];
            }
            set
            {
                this._properties[nameof(year_date)] = (object)value;
            }
        }

        public string year_scale_date
        {
            get
            {
                return (string)this._properties[nameof(year_scale_date)];
            }
            set
            {
                this._properties[nameof(year_scale_date)] = (object)value;
            }
        }

        public string year_tooltip
        {
            get
            {
                return (string)this._properties[nameof(year_tooltip)];
            }
            set
            {
                this._properties[nameof(year_tooltip)] = (object)value;
            }
        }

        public string timeline_scale_date
        {
            get
            {
                return (string)this._properties[nameof(timeline_scale_date)];
            }
            set
            {
                this._properties[nameof(timeline_scale_date)] = (object)value;
            }
        }

        public string timeline_scale_label
        {
            get
            {
                return (string)this._properties[nameof(timeline_scale_label)];
            }
            set
            {
                this._properties[nameof(timeline_scale_label)] = (object)value;
            }
        }

        public string timeline_scalex_class
        {
            get
            {
                return (string)this._properties[nameof(timeline_scalex_class)];
            }
            set
            {
                this._properties[nameof(timeline_scalex_class)] = (object)value;
            }
        }

        public string timeline_second_scalex_class
        {
            get
            {
                return (string)this._properties[nameof(timeline_second_scalex_class)];
            }
            set
            {
                this._properties[nameof(timeline_second_scalex_class)] = (object)value;
            }
        }

        public string timeline_second_scale_date
        {
            get
            {
                return (string)this._properties[nameof(timeline_second_scale_date)];
            }
            set
            {
                this._properties[nameof(timeline_second_scale_date)] = (object)value;
            }
        }

        public string timeline_scaley_class
        {
            get
            {
                return (string)this._properties[nameof(timeline_scaley_class)];
            }
            set
            {
                this._properties[nameof(timeline_scaley_class)] = (object)value;
            }
        }

        public string timeline_cell_class
        {
            get
            {
                return (string)this._properties[nameof(timeline_cell_class)];
            }
            set
            {
                this._properties[nameof(timeline_cell_class)] = (object)value;
            }
        }

        public string timeline_cell_value
        {
            get
            {
                return (string)this._properties[nameof(timeline_cell_value)];
            }
            set
            {
                this._properties[nameof(timeline_cell_value)] = (object)value;
            }
        }

        public string timeline_date
        {
            get
            {
                return (string)this._properties[nameof(timeline_date)];
            }
            set
            {
                this._properties[nameof(timeline_date)] = (object)value;
            }
        }

        public string timeline_tooltip
        {
            get
            {
                return (string)this._properties[nameof(timeline_tooltip)];
            }
            set
            {
                this._properties[nameof(timeline_tooltip)] = (object)value;
            }
        }

        public string map_time
        {
            get
            {
                return (string)this._properties[nameof(map_time)];
            }
            set
            {
                this._properties[nameof(map_time)] = (object)value;
            }
        }

        public string map_text
        {
            get
            {
                return (string)this._properties[nameof(map_text)];
            }
            set
            {
                this._properties[nameof(map_text)] = (object)value;
            }
        }

        public string marker_text
        {
            get
            {
                return (string)this._properties[nameof(marker_text)];
            }
            set
            {
                this._properties[nameof(marker_text)] = (object)value;
            }
        }

        public string marker_date
        {
            get
            {
                return (string)this._properties[nameof(marker_date)];
            }
            set
            {
                this._properties[nameof(marker_date)] = (object)value;
            }
        }

        public string week_agenda_event_text
        {
            get
            {
                return (string)this._properties[nameof(week_agenda_event_text)];
            }
            set
            {
                this._properties[nameof(week_agenda_event_text)] = (object)value;
            }
        }

        protected enum TemplateTypes
        {
            Date,
            Period,
            Event,
            Timeline,
            TimelineCell,
            Invalid,
        }
    }
    public abstract class SchedulerSettingsBase
    {
        protected CommonPropertiesContainer _properties = new CommonPropertiesContainer();
        protected string _prefix;

        public void Reset()
        {
            this._properties.Reset();
        }

        protected object getProp(string name)
        {
            return this._properties[name];
        }

        protected void setProp(string name, object value)
        {
            this._properties[name] = value;
        }

        protected string getStringProp(string name)
        {
            return (string)this.getProp(name);
        }

        protected void setStringProp(string name, string value)
        {
            this.setProp(name, (object)value);
        }

        protected bool getBoolProp(string name)
        {
            return (bool)this.getProp(name);
        }

        protected void setBoolProp(string name, bool value)
        {
            this.setProp(name, (object)value);
        }

        protected int getIntProp(string name)
        {
            return (int)this.getProp(name);
        }

        protected void setIntProp(string name, int value)
        {
            this.setProp(name, (object)value);
        }

        protected virtual string _renderField(string name)
        {
            if (this._properties.IsNullOrDefault(name))
                return "";
            return "\n\t" + name + " = " + this._properties.Render(name) + ";";
        }

        internal virtual Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>();
        }

        internal virtual Dictionary<string, FileType> GetCSS()
        {
            return new Dictionary<string, FileType>();
        }

        public virtual void Render(StringBuilder builder, string parent)
        {
            CommonPropertiesContainer properties = this._properties;
            foreach (string key in properties.GetKeys())
            {
                if (!properties.IsNullOrDefault(key))
                {
                    builder.Append("\n\t");
                    builder.Append(this._prefix);
                    builder.Append(key);
                    builder.Append(" = ");
                    builder.Append(properties.Render(key));
                    builder.Append(";");
                }
            }
        }

        internal virtual string AfterInit()
        {
            return "";
        }

        internal virtual void Deserialize(Dictionary<string, object> data)
        {
            foreach (KeyValuePair<string, object> keyValuePair in data)
                this._properties[keyValuePair.Key] = keyValuePair.Value;
        }

        internal virtual Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> property in (PropertiesContainer<object>)this._properties)
                dictionary.Add(property.Key, property.Value);
            return dictionary;
        }
    }
    public static class DateTemplate
    {
        public static string Render(string str)
        {
            return string.Format("function(date){{\n\tvar dat = \"\";{0} return dat;\n}};", (object)Template.CollectParts("dat", Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceVariable))));
        }
    }

    public static class EventTemplate
    {
        public static string Render(string str)
        {
            return string.Format("function(Start,End,ev){{\n\tvar temp = \"\";{0} return temp;\n}}", (object)Template.CollectParts("temp", Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceEvent))));
        }
    }

    public static class PeriodTemplate
    {
        public static string Render(string str)
        {
            return string.Format("function(date1, date2){{\n\tvar dat = \"\";{0} return dat;\n}};", (object)Template.CollectParts("dat", Template.Parse(str, "\"", new MatchEvaluator(Template.ReplaceVariable))));
        }
    }

    public class SchedulerConfig : SchedulerSettingsBase
    {
        public int scroll_hour
        {
            get
            {
                return this.getIntProp(nameof(scroll_hour));
            }
            set
            {
                this.setProp(nameof(scroll_hour), (object)value);
            }
        }

        public int event_duration
        {
            get
            {
                return this.getIntProp(nameof(event_duration));
            }
            set
            {
                this.setProp(nameof(event_duration), (object)value);
            }
        }

        public int cascade_event_count
        {
            get
            {
                return this.getIntProp(nameof(cascade_event_count));
            }
            set
            {
                this.setProp(nameof(cascade_event_count), (object)value);
            }
        }

        public int cascade_event_margin
        {
            get
            {
                return this.getIntProp(nameof(cascade_event_margin));
            }
            set
            {
                this.setProp(nameof(cascade_event_margin), (object)value);
            }
        }

        public bool cascade_event_display
        {
            get
            {
                return this.getBoolProp(nameof(cascade_event_display));
            }
            set
            {
                this.setProp(nameof(cascade_event_display), (object)value);
            }
        }

        public bool update_render
        {
            get
            {
                return this.getBoolProp(nameof(update_render));
            }
            set
            {
                this.setProp(nameof(update_render), (object)value);
            }
        }

        public int year_x
        {
            get
            {
                return this.getIntProp(nameof(year_x));
            }
            set
            {
                this.setProp(nameof(year_x), (object)value);
            }
        }

        public int year_y
        {
            get
            {
                return this.getIntProp(nameof(year_y));
            }
            set
            {
                this.setProp(nameof(year_y), (object)value);
            }
        }

        public bool prevent_cache
        {
            get
            {
                return this.getBoolProp(nameof(prevent_cache));
            }
            set
            {
                this.setProp(nameof(prevent_cache), (object)value);
            }
        }

        public int first_hour
        {
            get
            {
                return this.getIntProp(nameof(first_hour));
            }
            set
            {
                this.setProp(nameof(first_hour), (object)value);
            }
        }

        public int hour_size_px
        {
            get
            {
                return this.getIntProp(nameof(hour_size_px));
            }
            set
            {
                this.setProp(nameof(hour_size_px), (object)value);
            }
        }

        public int last_hour
        {
            get
            {
                return this.getIntProp(nameof(last_hour));
            }
            set
            {
                this.setProp(nameof(last_hour), (object)value);
            }
        }

        public int time_step
        {
            get
            {
                return this.getIntProp(nameof(time_step));
            }
            set
            {
                this.setProp(nameof(time_step), (object)value);
            }
        }

        public int map_infowindow_max_width
        {
            get
            {
                return this.getIntProp(nameof(map_infowindow_max_width));
            }
            set
            {
                this.setProp(nameof(map_infowindow_max_width), (object)value);
            }
        }

        public int map_zoom_after_resolve
        {
            get
            {
                return this.getIntProp(nameof(map_zoom_after_resolve));
            }
            set
            {
                this.setProp(nameof(map_zoom_after_resolve), (object)value);
            }
        }

        public bool map_resolve_event_location
        {
            get
            {
                return this.getBoolProp(nameof(map_resolve_event_location));
            }
            set
            {
                this.setProp(nameof(map_resolve_event_location), (object)value);
            }
        }

        public int map_inital_zoom
        {
            get
            {
                return this.getIntProp(nameof(map_inital_zoom));
            }
            set
            {
                this.setProp(nameof(map_inital_zoom), (object)value);
            }
        }

        public bool map_resolve_user_location
        {
            get
            {
                return this.getBoolProp(nameof(map_resolve_user_location));
            }
            set
            {
                this.setProp(nameof(map_resolve_user_location), (object)value);
            }
        }

        public SchedulerConfig.MapTypes map_type
        {
            get
            {
                string property = (string)this._properties[nameof(map_type)];
                if (property == "google.maps.MapTypeId.HYBRID")
                    return SchedulerConfig.MapTypes.HYBRID;
                if (property == "google.maps.MapTypeId.ROADMAP")
                    return SchedulerConfig.MapTypes.ROADMAP;
                return property == "google.maps.MapTypeId.SATELLITE" ? SchedulerConfig.MapTypes.SATELLITE : SchedulerConfig.MapTypes.TERRAIN;
            }
            set
            {
                switch (value)
                {
                    case SchedulerConfig.MapTypes.HYBRID:
                        this._properties[nameof(map_type)] = (object)"google.maps.MapTypeId.HYBRID";
                        break;
                    case SchedulerConfig.MapTypes.ROADMAP:
                        this._properties[nameof(map_type)] = (object)"google.maps.MapTypeId.ROADMAP";
                        break;
                    case SchedulerConfig.MapTypes.SATELLITE:
                        this._properties[nameof(map_type)] = (object)"google.maps.MapTypeId.SATELLITE";
                        break;
                    default:
                        this._properties[nameof(map_type)] = (object)"google.maps.MapTypeId.TERRAIN";
                        break;
                }
            }
        }

        public SchedulerConfig.LightboxRecurring lightbox_recurring
        {
            get
            {
                string property = (string)this._properties[nameof(lightbox_recurring)];
                if (property == "ask")
                    return SchedulerConfig.LightboxRecurring.Ask;
                if (property == "instance")
                    return SchedulerConfig.LightboxRecurring.Instance;
                return property == "series" ? SchedulerConfig.LightboxRecurring.Series : SchedulerConfig.LightboxRecurring.Ask;
            }
            set
            {
                this._properties[nameof(lightbox_recurring)] = (object)value.ToString("g");
            }
        }

        public bool select
        {
            get
            {
                return this.getBoolProp(nameof(select));
            }
            set
            {
                this.setProp(nameof(select), (object)value);
            }
        }

        public bool occurrence_timestamp_in_utc
        {
            get
            {
                return this.getBoolProp(nameof(occurrence_timestamp_in_utc));
            }
            set
            {
                this.setProp(nameof(occurrence_timestamp_in_utc), (object)value);
            }
        }

        public bool include_end_by
        {
            get
            {
                return this.getBoolProp(nameof(include_end_by));
            }
            set
            {
                this.setProp(nameof(include_end_by), (object)value);
            }
        }

        public bool start_on_monday
        {
            get
            {
                return this.getBoolProp(nameof(start_on_monday));
            }
            set
            {
                this.setProp(nameof(start_on_monday), (object)value);
            }
        }

        public bool mark_now
        {
            get
            {
                return this.getBoolProp(nameof(mark_now));
            }
            set
            {
                this.setProp(nameof(mark_now), (object)value);
            }
        }

        public bool limit_time_select
        {
            get
            {
                return this.getBoolProp(nameof(limit_time_select));
            }
            set
            {
                this.setProp(nameof(limit_time_select), (object)value);
            }
        }

        public bool isReadonly
        {
            get
            {
                return this.getBoolProp("readonly");
            }
            set
            {
                this.setProp("readonly", (object)value);
            }
        }

        public bool show_loading
        {
            get
            {
                return this.getBoolProp(nameof(show_loading));
            }
            set
            {
                this.setProp(nameof(show_loading), (object)value);
            }
        }

        public bool drag_resize
        {
            get
            {
                return this.getBoolProp(nameof(drag_resize));
            }
            set
            {
                this.setProp(nameof(drag_resize), (object)value);
            }
        }

        public bool drag_move
        {
            get
            {
                return this.getBoolProp(nameof(drag_move));
            }
            set
            {
                this.setProp(nameof(drag_move), (object)value);
            }
        }

        public bool drag_create
        {
            get
            {
                return this.getBoolProp(nameof(drag_create));
            }
            set
            {
                this.setProp(nameof(drag_create), (object)value);
            }
        }

        public bool dblclick_create
        {
            get
            {
                return this.getBoolProp(nameof(dblclick_create));
            }
            set
            {
                this.setProp(nameof(dblclick_create), (object)value);
            }
        }

        public bool details_on_create
        {
            get
            {
                return this.getBoolProp(nameof(details_on_create));
            }
            set
            {
                this.setProp(nameof(details_on_create), (object)value);
            }
        }

        public bool edit_on_create
        {
            get
            {
                return this.getBoolProp(nameof(edit_on_create));
            }
            set
            {
                this.setProp(nameof(edit_on_create), (object)value);
            }
        }

        public bool click_form_details
        {
            get
            {
                return this.getBoolProp(nameof(click_form_details));
            }
            set
            {
                this.setProp(nameof(click_form_details), (object)value);
            }
        }

        public bool details_on_dblclick
        {
            get
            {
                return this.getBoolProp(nameof(details_on_dblclick));
            }
            set
            {
                this.setProp(nameof(details_on_dblclick), (object)value);
            }
        }

        public bool server_utc
        {
            get
            {
                return this.getBoolProp(nameof(server_utc));
            }
            set
            {
                this.setProp(nameof(server_utc), (object)value);
            }
        }

        public bool positive_closing
        {
            get
            {
                return this.getBoolProp(nameof(positive_closing));
            }
            set
            {
                this.setProp(nameof(positive_closing), (object)value);
            }
        }

        public bool multi_day
        {
            get
            {
                return this.getBoolProp(nameof(multi_day));
            }
            set
            {
                this.setProp(nameof(multi_day), (object)value);
            }
        }

        public bool full_day
        {
            get
            {
                return this.getBoolProp(nameof(full_day));
            }
            set
            {
                this.setProp(nameof(full_day), (object)value);
            }
        }

        public bool auto_end_date
        {
            get
            {
                return this.getBoolProp(nameof(auto_end_date));
            }
            set
            {
                this.setProp(nameof(auto_end_date), (object)value);
            }
        }

        public bool drag_lightbox
        {
            get
            {
                return this.getBoolProp(nameof(drag_lightbox));
            }
            set
            {
                this.setProp(nameof(drag_lightbox), (object)value);
            }
        }

        public bool preserve_scroll
        {
            get
            {
                return this.getBoolProp(nameof(preserve_scroll));
            }
            set
            {
                this.setProp(nameof(preserve_scroll), (object)value);
            }
        }

        public bool highlight_displayed_event
        {
            get
            {
                return this.getBoolProp(nameof(highlight_displayed_event));
            }
            set
            {
                this.setProp(nameof(highlight_displayed_event), (object)value);
            }
        }

        public bool wide_form
        {
            get
            {
                return this.getBoolProp(nameof(wide_form));
            }
            set
            {
                this.setProp(nameof(wide_form), (object)value);
            }
        }

        public string Tooltip_ClassName
        {
            get
            {
                return this.getStringProp("dhtmlXTooltip.config.className");
            }
            set
            {
                this.setProp("dhtmlXTooltip.config.className", (object)value);
            }
        }

        public string Tooltip_timeout_to_display
        {
            get
            {
                return this.getStringProp("dhtmlXTooltip.config.timeout_to_display");
            }
            set
            {
                this.setProp("htmlXTooltip.config.timeout_to_display", (object)value);
            }
        }

        public string Tooltip_delta_x
        {
            get
            {
                return this.getStringProp("dhtmlXTooltip.config.delta_x");
            }
            set
            {
                this.setProp("dhtmlXTooltip.config.delta_x", (object)value);
            }
        }

        public string Tooltip_delta_y
        {
            get
            {
                return this.getStringProp("dhtmlXTooltip.config.delta_y");
            }
            set
            {
                this.setProp("dhtmlXTooltip.config.delta_y", (object)value);
            }
        }

        public string default_date
        {
            get
            {
                return this.getStringProp(nameof(default_date));
            }
            set
            {
                this.setProp(nameof(default_date), (object)value);
            }
        }

        public string month_date
        {
            get
            {
                return this.getStringProp(nameof(month_date));
            }
            set
            {
                this.setProp(nameof(month_date), (object)value);
            }
        }

        public string load_date
        {
            get
            {
                return this.getStringProp(nameof(load_date));
            }
            set
            {
                this.setProp(nameof(load_date), (object)value);
            }
        }

        public string week_date
        {
            get
            {
                return this.getStringProp(nameof(week_date));
            }
            set
            {
                this.setProp(nameof(week_date), (object)value);
            }
        }

        public string day_date
        {
            get
            {
                return this.getStringProp(nameof(day_date));
            }
            set
            {
                this.setProp(nameof(day_date), (object)value);
            }
        }

        public string hour_date
        {
            get
            {
                return this.getStringProp(nameof(hour_date));
            }
            set
            {
                this.setProp(nameof(hour_date), (object)value);
            }
        }

        public string month_day
        {
            get
            {
                return this.getStringProp(nameof(month_day));
            }
            set
            {
                this.setProp(nameof(month_day), (object)value);
            }
        }

        public string api_date
        {
            get
            {
                return this.getStringProp(nameof(api_date));
            }
            set
            {
                this.setProp(nameof(api_date), (object)value);
            }
        }

        public string xml_date
        {
            get
            {
                return this.getStringProp(nameof(xml_date));
            }
            set
            {
                this.setProp(nameof(xml_date), (object)value);
            }
        }

        public string repeat_date
        {
            get
            {
                return this.getStringProp(nameof(repeat_date));
            }
            set
            {
                this.setProp(nameof(repeat_date), (object)value);
            }
        }

        public string year_mode_name
        {
            get
            {
                return this.getStringProp(nameof(year_mode_name));
            }
            set
            {
                this.setProp(nameof(year_mode_name), (object)value);
            }
        }

        public string displayed_event_color
        {
            get
            {
                return this.getStringProp(nameof(displayed_event_color));
            }
            set
            {
                this.setProp(nameof(displayed_event_color), (object)value);
            }
        }

        public string displayed_event_text_color
        {
            get
            {
                return this.getStringProp(nameof(displayed_event_text_color));
            }
            set
            {
                this.setProp(nameof(displayed_event_text_color), (object)value);
            }
        }

        public bool check_limits
        {
            get
            {
                return this.getBoolProp(nameof(check_limits));
            }
            set
            {
                this.setProp(nameof(check_limits), (object)value);
            }
        }

        public bool fix_tab_position
        {
            get
            {
                return this.getBoolProp(nameof(fix_tab_position));
            }
            set
            {
                this.setBoolProp(nameof(fix_tab_position), value);
            }
        }

        public bool use_select_menu_space
        {
            get
            {
                return this.getBoolProp(nameof(use_select_menu_space));
            }
            set
            {
                this.setBoolProp(nameof(use_select_menu_space), value);
            }
        }

        public int collision_limit
        {
            get
            {
                return this.getIntProp(nameof(collision_limit));
            }
            set
            {
                this.setProp(nameof(collision_limit), (object)value);
            }
        }

        public DateTime limit_start { get; set; }

        public DateTime limit_end { get; set; }

        public bool limit_view
        {
            get
            {
                return this.getBoolProp(nameof(limit_view));
            }
            set
            {
                this.setProp(nameof(limit_view), (object)value);
            }
        }

        public bool display_marked_timespans
        {
            get
            {
                return this.getBoolProp(nameof(display_marked_timespans));
            }
            set
            {
                this.setProp(nameof(display_marked_timespans), (object)value);
            }
        }

        public SchedulerConfig()
        {
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          nameof (cascade_event_count),
          (object) 4
        },
        {
          nameof (cascade_event_margin),
          (object) 20
        },
        {
          nameof (click_form_details),
          (object) 0
        },
        {
          nameof (dblclick_create),
          (object) 1
        },
        {
          nameof (details_on_create),
          (object) 0
        },
        {
          nameof (drag_create),
          (object) 1
        },
        {
          nameof (drag_move),
          (object) 1
        },
        {
          nameof (drag_resize),
          (object) 1
        },
        {
          nameof (edit_on_create),
          (object) 1
        },
        {
          nameof (first_hour),
          (object) 0
        },
        {
          nameof (hour_size_px),
          (object) 42
        },
        {
          nameof (last_hour),
          (object) 24
        },
        {
          nameof (start_on_monday),
          (object) 1
        },
        {
          nameof (time_step),
          (object) 5
        },
        {
          nameof (map_infowindow_max_width),
          (object) 300
        },
        {
          nameof (map_zoom_after_resolve),
          (object) 15
        },
        {
          nameof (collision_limit),
          (object) 1
        },
        {
          nameof (year_x),
          (object) 4
        },
        {
          nameof (year_y),
          (object) 3
        },
        {
          "dhtmlXTooltip.config.className",
          (object) "dhtmlXTooltip tooltip"
        },
        {
          "dhtmlXTooltip.config.timeout_to_display",
          (object) 50
        },
        {
          "dhtmlXTooltip.config.delta_x",
          (object) 15
        },
        {
          "dhtmlXTooltip.config.delta_y",
          (object) -20
        },
        {
          nameof (year_mode_name),
          (object) "year"
        },
        {
          nameof (api_date),
          (object) "%d-%m-%Y %H=>%i"
        },
        {
          nameof (day_date),
          (object) "%D, %F %j"
        },
        {
          nameof (default_date),
          (object) "%j %M %Y"
        },
        {
          nameof (hour_date),
          (object) "%H=>%i"
        },
        {
          nameof (load_date),
          (object) "%Y-%m-%d"
        },
        {
          nameof (month_date),
          (object) "%F %Y"
        },
        {
          nameof (month_day),
          (object) "%d"
        },
        {
          nameof (week_date),
          (object) "%l"
        },
        {
          nameof (xml_date),
          (object) "%m/%d/%Y %H:%i"
        },
        {
          nameof (positive_closing),
          (object) false
        },
        {
          "readonly",
          (object) false
        },
        {
          nameof (server_utc),
          (object) false
        },
        {
          nameof (cascade_event_display),
          (object) false
        },
        {
          nameof (map_resolve_event_location),
          (object) true
        },
        {
          nameof (map_resolve_user_location),
          (object) true
        },
        {
          nameof (repeat_date),
          (object) "%m.%d.%Y"
        },
        {
          nameof (wide_form),
          (object) false
        },
        {
          nameof (select),
          (object) true
        },
        {
          nameof (include_end_by),
          (object) false
        },
        {
          nameof (occurrence_timestamp_in_utc),
          (object) false
        },
        {
          nameof (limit_view),
          (object) false
        },
        {
          nameof (mark_now),
          (object) true
        },
        {
          nameof (display_marked_timespans),
          (object) true
        },
        {
          nameof (fix_tab_position),
          (object) true
        },
        {
          nameof (use_select_menu_space),
          (object) true
        },
        {
          nameof (highlight_displayed_event),
          (object) true
        },
        {
          nameof (update_render),
          (object) false
        },
        {
          nameof (check_limits),
          (object) true
        }
      });
        }

        public override void Render(StringBuilder builder, string parent)
        {
            this._prefix = parent + ".config.";
            base.Render(builder, parent);
            if (this.limit_end != new DateTime())
                builder.Append(string.Format("\n{0}limit_end = {1};", (object)this._prefix, (object)DateFormatHelper.DateToJS(this.limit_end)));
            if (!(this.limit_start != new DateTime()))
                return;
            builder.Append(string.Format("\n{0}limit_start = {1};", (object)this._prefix, (object)DateFormatHelper.DateToJS(this.limit_start)));
        }

        public enum MapTypes
        {
            HYBRID,
            ROADMAP,
            SATELLITE,
            TERRAIN,
        }

        public enum LightboxRecurring
        {
            Ask,
            Instance,
            Series,
        }
    }

}
