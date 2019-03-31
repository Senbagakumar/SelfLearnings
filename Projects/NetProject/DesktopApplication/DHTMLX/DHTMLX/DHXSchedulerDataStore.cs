using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using DHTMLX.Scheduler.GoogleCalendar;

namespace DHTMLX.Scheduler.Data
{
    public class DHXSchedulerDataStore
    {
        internal static string startMarker = "##STRT_DAT##";
        internal static string endMarker = "##END_DAT##";
        protected JavaScriptSerializer serializer = new JavaScriptSerializer();
        protected DateFormatHelper formatter = new DateFormatHelper();
        protected List<object> items;

        public TimeSpan TimeOffset { get; set; }

        [Obsolete("Use DropOffset() instead")]
        public void UseUTCDate()
        {
            this.DropOffset();
        }

        public void DropOffset()
        {
            this.TimeOffset = new TimeSpan();
        }

        public bool ToUniversalTime { get; set; }

        public string DateFormat { get; set; }

        public DHXSchedulerDataStore()
        {
            this.items = new List<object>();
            this.TimeOffset = new TimeSpan();
            this.DateFormat = "%m/%d/%Y %H:%i";
        }

        public object ParseGoogleEvent(ICalEvent ev)
        {
            return (object)SchedulerEvent.Convert(ev);
        }

        public int Count
        {
            get
            {
                if (this.items != null)
                    return this.items.Count;
                return 0;
            }
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public void Add(object item)
        {
            if (item is ICalEvent)
            {
                object googleEvent = this.ParseGoogleEvent(item as ICalEvent);
                if (googleEvent == null)
                    return;
                this.items.Add(googleEvent);
            }
            else
                this.items.Add(item);
        }

        public object this[int index]
        {
            get
            {
                return this.items[index];
            }
            set
            {
                this.items[index] = value;
            }
        }

        public void Parse(IEnumerable data)
        {
            foreach (object obj in data)
                this.Add(obj);
        }

        public object Serialize()
        {
            return (object)this.items;
        }

        public void Deserialize(object data)
        {
            this.Parse(data as IEnumerable);
        }

        protected string replaceDates(string val)
        {
            string input = val;
            this.DateFormat = DateFormatHelper.ConvertInnerFormatToNet(this.DateFormat);
            return new Regex(string.Format("\\[\"{0}\",(\".*?\"),\"{1}\"\\]", (object)DHXSchedulerDataStore.startMarker, (object)DHXSchedulerDataStore.endMarker)).Replace(input, new MatchEvaluator(this.Callback));
        }

        protected string Callback(Match match)
        {
            return match.Groups[1].Value;
        }

        public string Render(Action<StringBuilder, object> eventRenderer)
        {
            return this.RenderCustom(eventRenderer);
        }

        protected string RenderCustom(Action<StringBuilder, object> eventRenderer)
        {
            int count = this.items.Count;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            for (int index = 0; index < count; ++index)
            {
                eventRenderer(stringBuilder, this.items[index]);
                if (index < count - 1)
                    stringBuilder.Append(',');
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        protected string RenderDefault()
        {
            return this.replaceDates(DHXSchedulerDataStore.GetSerializer(DateFormatHelper.ConvertInnerFormatToNet(this.DateFormat), this.TimeOffset, this.ToUniversalTime).Serialize((object)this.items));
        }

        public string Render()
        {
            return this.RenderDefault();
        }

        public static JavaScriptSerializer GetSerializer(string dateformat, TimeSpan offset, bool toUniversalTime)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            scriptSerializer.RegisterConverters((IEnumerable<JavaScriptConverter>)new DHXSchedulerDataStore.DateTimeJavaScriptConverter[1]
            {
        new DHXSchedulerDataStore.DateTimeJavaScriptConverter()
        {
          DateFormat = dateformat,
          TimeOffset = offset,
          ToUniversalTime = toUniversalTime
        }
            });
            return scriptSerializer;
        }

        public class DateTimeJavaScriptConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                return (object)null;
            }

            public string DateFormat { get; set; }

            public TimeSpan TimeOffset { get; set; }

            public bool ToUniversalTime { get; set; }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                if (!(obj is DateTime))
                    return (IDictionary<string, object>)null;
                DateTime dateTime = (DateTime)obj;
                if (this.ToUniversalTime)
                    dateTime = dateTime.ToUniversalTime();
                return (IDictionary<string, object>)new DHXSchedulerDataStore.DateTimeJavaScriptConverter.CustomString((dateTime + this.TimeOffset).ToString(this.DateFormat));
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get
                {
                    return (IEnumerable<Type>)new Type[1]
                    {
            typeof (DateTime)
                    };
                }
            }

            private class CustomString : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
            {
                public string Value;

                public CustomString(string str)
                {
                    this.Value = str;
                }

                public override string ToString()
                {
                    return this.Value;
                }

                void IDictionary<string, object>.Add(string key, object value)
                {
                    throw new NotImplementedException();
                }

                bool IDictionary<string, object>.ContainsKey(string key)
                {
                    throw new NotImplementedException();
                }

                ICollection<string> IDictionary<string, object>.Keys
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                bool IDictionary<string, object>.Remove(string key)
                {
                    throw new NotImplementedException();
                }

                bool IDictionary<string, object>.TryGetValue(string key, out object value)
                {
                    throw new NotImplementedException();
                }

                ICollection<object> IDictionary<string, object>.Values
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                object IDictionary<string, object>.this[string key]
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                    set
                    {
                        throw new NotImplementedException();
                    }
                }

                void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
                {
                    throw new NotImplementedException();
                }

                void ICollection<KeyValuePair<string, object>>.Clear()
                {
                    throw new NotImplementedException();
                }

                bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
                {
                    throw new NotImplementedException();
                }

                void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
                {
                    throw new NotImplementedException();
                }

                int ICollection<KeyValuePair<string, object>>.Count
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                bool ICollection<KeyValuePair<string, object>>.IsReadOnly
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
                {
                    throw new NotImplementedException();
                }

                IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
                {
                    throw new NotImplementedException();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return (IEnumerator)new DHXSchedulerDataStore.DateTimeJavaScriptConverter.CustomStringEnum(this.Value);
                }
            }

            public class CustomStringEnum : IEnumerator
            {
                private int position = -1;
                public string value;

                public CustomStringEnum(string list)
                {
                    this.value = list;
                }

                public bool MoveNext()
                {
                    ++this.position;
                    return this.position < 3;
                }

                public void Reset()
                {
                    this.position = -1;
                }

                object IEnumerator.Current
                {
                    get
                    {
                        if (this.position == 0)
                            return (object)DHXSchedulerDataStore.startMarker;
                        if (this.position == 1)
                            return (object)this.value;
                        return (object)DHXSchedulerDataStore.endMarker;
                    }
                }

                public string Current
                {
                    get
                    {
                        try
                        {
                            return this.value;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            throw new InvalidOperationException();
                        }
                    }
                }
            }
        }
    }
}

