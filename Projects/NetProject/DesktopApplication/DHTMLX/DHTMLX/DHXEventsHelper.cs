using Hyper.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Linq;
using DHTMLX.Helpers;
using System.Reflection;
using System.Collections;

namespace DHTMLX.Common
{
    public class DHXEventsHelper
    {
        public static object Bind(Type obj, NameValueCollection values)
        {
            return DHXEventsHelper.Bind(obj, values, CultureInfo.InvariantCulture);
        }

        public static object Bind(Type obj, NameValueCollection values, CultureInfo culture)
        {
            object component = obj.GetConstructor(new Type[0]).Invoke(new object[0]);
            PropertyDescriptorCollection properties = new HyperTypeDescriptionProvider(obj).GetTypeDescriptor(obj).GetProperties();
            foreach (string allKey in values.AllKeys)
            {
                PropertyDescriptor propertyDescriptor = properties.Find(allKey, true);
                if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
                {
                    if (propertyDescriptor.Converter.CanConvertFrom(typeof(string)))
                    {
                        try
                        {
                            int result;
                            if (allKey != "id" || allKey == "id" && int.TryParse(values[allKey], out result))
                                propertyDescriptor.SetValue(component, propertyDescriptor.Converter.ConvertFrom((ITypeDescriptorContext)null, culture, (object)values[allKey]));
                            else
                                propertyDescriptor.SetValue(component, propertyDescriptor.ComponentType.IsValueType ? Activator.CreateInstance(propertyDescriptor.ComponentType) : (object)(obj = (Type)null));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return component;
        }

        public static void Update(object target, object source, List<string> except)
        {
            if (source == null || target == null || !source.GetType().Equals(target.GetType()))
                return;
            foreach (PropertyDescriptor property in new HyperTypeDescriptionProvider(source.GetType()).GetTypeDescriptor(source.GetType()).GetProperties())
            {
                if (!property.IsReadOnly && !except.Contains(property.Name))
                    property.SetValue(target, property.GetValue(source));
            }
        }

        public static void Update(object target, object source)
        {
            DHXEventsHelper.Update(target, source, new List<string>());
        }

        protected object ConstructItem(object part, PropertyDescriptorCollection objectProperties, Dictionary<string, object> properties)
        {
            foreach (KeyValuePair<string, object> property in properties)
            {
                if (objectProperties.Find(property.Key, true) != null)
                    objectProperties.Find(property.Key, true).SetValue(part, property.Value);
            }
            return part;
        }

        public List<object> GetOccurrences(IEnumerable items, DateTime from, DateTime to)
        {
            List<object> objectList = new List<object>();
            bool flag = true;
            List<DHXEventsHelper._ParsedEventData> source = new List<DHXEventsHelper._ParsedEventData>();
            PropertyDescriptorCollection info = (PropertyDescriptorCollection)null;
            foreach (object ev in items)
            {
                if (flag)
                {
                    info = new HyperTypeDescriptionProvider(ev.GetType()).GetTypeDescriptor(ev.GetType()).GetProperties();
                    flag = false;
                }
                DHXEventsHelper._ParsedEventData occurrences = this._GetOccurrences(ev, from, to, 300, info);
                if (occurrences != null)
                    source.Add(occurrences);
            }
            TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            using (List<DHXEventsHelper._ParsedEventData>.Enumerator enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    DHXEventsHelper._ParsedEventData eventSet = enumerator.Current;
                    if (eventSet.EventInfo.parsed_type == null || eventSet.EventInfo.parsed_type.Type == RecTypes.None || string.IsNullOrEmpty(eventSet.EventInfo.rec_type))
                    {
                        DHXEventsHelper._ParsedEventData parsedEventData = source.SingleOrDefault<DHXEventsHelper._ParsedEventData>((Func<DHXEventsHelper._ParsedEventData, bool>)(set =>
                        {
                            int id = set.EventInfo.id;
                            int? eventPid = eventSet.EventInfo.event_pid;
                            if (id == eventPid.GetValueOrDefault())
                                return eventPid.HasValue;
                            return false;
                        }));
                        if (parsedEventData != null && eventSet.EventInfo.event_length.HasValue)
                        {
                            DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds((double)eventSet.EventInfo.event_length.Value) + utcOffset;
                            parsedEventData.Occurrences.Remove(dateTime);
                        }
                    }
                }
            }
            foreach (DHXEventsHelper._ParsedEventData data in source)
            {
                if (data.EventInfo.parsed_type == null && string.IsNullOrEmpty(data.EventInfo.rec_type) || data.EventInfo.parsed_type != null && (data.EventInfo.parsed_type.Type == RecTypes.Invalid || data.EventInfo.parsed_type.Type != RecTypes.None))
                    objectList.AddRange((IEnumerable<object>)this._ProcessEvData(data, info));
            }
            return objectList;
        }

        internal List<object> _ProcessEvData(DHXEventsHelper._ParsedEventData data, PropertyDescriptorCollection info)
        {
            List<object> objectList = new List<object>();
            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (string.IsNullOrEmpty((string)info.Find("rec_type", true).GetValue(data.EventObject)))
            {
                objectList.Add(data.EventObject);
            }
            else
            {
                foreach (PropertyDescriptor propertyDescriptor in info)
                {
                    if (propertyDescriptor.Name != "rec_type" && propertyDescriptor.Name != "event_pid" && propertyDescriptor.Name != "event_length")
                        properties.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(data.EventObject));
                }
                ConstructorInfo constructor = data.EventObject.GetType().GetConstructor(new Type[0]);
                foreach (DateTime occurrence in data.Occurrences)
                {
                    long? nullable = (long?)info.Find("event_length", true).GetValue(data.EventObject);
                    DateTime dateTime = !nullable.HasValue ? occurrence : occurrence.AddSeconds((double)nullable.Value);
                    object part = constructor.Invoke(new object[0]);
                    properties["start_date"] = (object)occurrence;
                    properties["end_date"] = (object)dateTime;
                    objectList.Add(this.ConstructItem(part, info, properties));
                }
            }
            return objectList;
        }

        public static DateTime Next(DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((double)((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek));
        }

        public DateTime GetNthWeekofMonth(DateTime date, int nthWeek, DayOfWeek dayofWeek)
        {
            return DHXEventsHelper.Next(date, dayofWeek).AddDays((double)((nthWeek - 1) * 7));
        }

        public bool CheckTimeframe(DateTime evStart, DateTime evEnd, DateTime From, DateTime To)
        {
            if (evStart > From && evStart < evEnd)
                return evStart < To;
            return false;
        }

        internal DHXEventsHelper._ParsedEventData _GetOccurrences(object ev, DateTime from_date, DateTime to_date, int max, PropertyDescriptorCollection info)
        {
            if (info.Find("id", true) == null || info.Find("start_date", true) == null || info.Find("end_date", true) == null)
                return (DHXEventsHelper._ParsedEventData)null;
            DHXEventsHelper.RecEvent recEvent = new DHXEventsHelper.RecEvent();
            recEvent.id = (int)info.Find("id", true).GetValue(ev);
            object obj1 = info.Find("start_date", true).GetValue(ev);
            if (obj1 != null)
                recEvent.start_date = (DateTime)obj1;
            object obj2 = info.Find("end_date", true).GetValue(ev);
            if (obj2 != null)
                recEvent.end_date = (DateTime)obj2;
            if (info.Find("rec_type", true) != null && info.Find("event_length", true) != null && info.Find("event_pid", true) != null)
            {
                recEvent.rec_type = (string)info.Find("rec_type", true).GetValue(ev);
                recEvent.event_length = (long?)info.Find("event_length", true).GetValue(ev);
                recEvent.event_pid = (int?)info.Find("event_pid", true).GetValue(ev);
            }
            RecurringType recurringType = RecurringType.Parse(recEvent.rec_type);
            if (recurringType.Type != RecTypes.Invalid && recurringType.Type != RecTypes.None && recEvent.event_length.HasValue)
            {
                List<DateTime> dateTimeList = new List<DateTime>();
                recEvent.parsed_type = recurringType;
                DateTime dateTime = recEvent.start_date;
                DateTime endDate = recEvent.end_date;
                int result = -1;
                if (int.TryParse(recurringType.Extra, out result) && result < max)
                    max = result;
                while (dateTime < endDate && dateTime < to_date && dateTimeList.Count < max)
                {
                    if (recurringType.Count2 != -1 && recurringType.Day != -1)
                    {
                        DateTime nthWeekofMonth = this.GetNthWeekofMonth(dateTime, recurringType.Count2, (DayOfWeek)recurringType.Day);
                        if (this.CheckTimeframe(nthWeekofMonth, recEvent.end_date, from_date, to_date))
                            dateTimeList.Add(nthWeekofMonth);
                    }
                    else if (recurringType.Days.Count == 0 && this.CheckTimeframe(dateTime, recEvent.end_date, from_date, to_date))
                        dateTimeList.Add(dateTime);
                    else if (recurringType.Days.Count > 0)
                    {
                        foreach (int day in recurringType.Days)
                        {
                            int num = (int)(7 - dateTime.DayOfWeek + day) % 7;
                            DateTime evStart = dateTime.AddDays((double)num);
                            if (this.CheckTimeframe(evStart, recEvent.end_date, from_date, to_date) && dateTimeList.Count < max)
                                dateTimeList.Add(evStart);
                        }
                    }
                    switch (recurringType.Type)
                    {
                        case RecTypes.Day:
                            dateTime = dateTime.AddDays((double)recurringType.Count);
                            continue;
                        case RecTypes.Week:
                            dateTime = dateTime.AddDays((double)(7 * recurringType.Count));
                            continue;
                        case RecTypes.Month:
                            dateTime = dateTime.AddMonths(recurringType.Count);
                            continue;
                        case RecTypes.Year:
                            dateTime = dateTime.AddYears(recurringType.Count);
                            continue;
                        default:
                            continue;
                    }
                }
                return new DHXEventsHelper._ParsedEventData()
                {
                    EventInfo = recEvent,
                    EventObject = ev,
                    Occurrences = dateTimeList
                };
            }
            if (!(recEvent.start_date < to_date) || !(recEvent.end_date > from_date))
                return (DHXEventsHelper._ParsedEventData)null;
            return new DHXEventsHelper._ParsedEventData()
            {
                EventInfo = recEvent,
                EventObject = ev,
                Occurrences = new List<DateTime>()
        {
          recEvent.start_date
        }
            };
        }

        internal List<object> _ParseConfigs(List<DHXEventsHelper._ParsedEventData> events)
        {
            List<object> objectList = new List<object>();
            foreach (DHXEventsHelper._ParsedEventData parsedEventData in events)
            {
                if (!string.IsNullOrEmpty(parsedEventData.EventInfo.rec_type))
                    return objectList;
                objectList.Add(parsedEventData.EventObject);
            }
            return objectList;
        }

        internal class _ParsedEventData
        {
            public DHXEventsHelper.RecEvent EventInfo { get; set; }

            public List<DateTime> Occurrences { get; set; }

            public object EventObject { get; set; }
        }

        internal class RecEvent
        {
            public int id { get; set; }

            public DateTime start_date { get; set; }

            public DateTime end_date { get; set; }

            public string rec_type { get; set; }

            public RecurringType parsed_type { get; set; }

            public long? event_length { get; set; }

            public int? event_pid { get; set; }
        }
    }
}

