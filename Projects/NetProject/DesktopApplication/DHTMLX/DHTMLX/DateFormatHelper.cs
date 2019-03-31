using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX
{
    public class DateFormatHelper
    {
        private static List<KeyValuePair<string, string>> formats = new List<KeyValuePair<string, string>>()
    {
      new KeyValuePair<string, string>("dddd", "%l"),
      new KeyValuePair<string, string>("ddd", "%D"),
      new KeyValuePair<string, string>("dd", "%d"),
      new KeyValuePair<string, string>("d", "%j"),
      new KeyValuePair<string, string>("yyyy", "%Y"),
      new KeyValuePair<string, string>("yy", "%y"),
      new KeyValuePair<string, string>("h", "%h"),
      new KeyValuePair<string, string>("H", "%H"),
      new KeyValuePair<string, string>("MMMM", "%F"),
      new KeyValuePair<string, string>("MMM", "%M"),
      new KeyValuePair<string, string>("MM", "%m"),
      new KeyValuePair<string, string>("M", "%n"),
      new KeyValuePair<string, string>("mm", "%i"),
      new KeyValuePair<string, string>("ss", "%s"),
      new KeyValuePair<string, string>("tt", "%A")
    };

        public static string ConvertInnerFormatToNet(string format)
        {
            string str = format;
            foreach (KeyValuePair<string, string> format1 in DateFormatHelper.formats)
                str = str.Replace(format1.Value, format1.Key);
            return str;
        }

        public static string ConvertNetFormatToInner(string format)
        {
            string str = format;
            for (int index = 0; index < DateFormatHelper.formats.Count; ++index)
                str = str.Replace(DateFormatHelper.formats[index].Key, DateFormatHelper.formats[index].Value);
            return str;
        }

        public static string DateToJS(DateTime date)
        {
            return string.Format("new Date({0},{1},{2})", (object)date.Year, (object)(date.Month - 1), (object)date.Day);
        }

        public static string DateToJS(DateTime date, bool detailed)
        {
            return string.Format("new Date({0},{1},{2},{3},{4})", (object)date.Year, (object)(date.Month - 1), (object)date.Day, (object)date.Hour, (object)date.Minute);
        }
    }
}
