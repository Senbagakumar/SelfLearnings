using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Helpers
{
    public enum RecTypes
    {
        Day,
        Week,
        Month,
        Year,
        None,
        Invalid,
    }

    public class RecurringType
    {
        public RecTypes Type { get; set; }

        public int Count { get; set; }

        public int Count2 { get; set; }

        public int Day { get; set; }

        public List<int> Days { get; set; }

        public string Extra { get; set; }

        public static RecurringType Parse(string rec_type)
        {
            if (string.IsNullOrEmpty(rec_type))
                return new RecurringType()
                {
                    Type = RecTypes.Invalid
                };
            string[] strArray1 = rec_type.Split('_');
            if (strArray1.Length < 5 && rec_type != "none")
                return new RecurringType()
                {
                    Type = RecTypes.Invalid
                };
            RecurringType recurringType = new RecurringType()
            {
                Type = (RecTypes)Enum.Parse(typeof(RecTypes), strArray1[0], true),
                Count = -1,
                Count2 = -1,
                Day = -1,
                Days = new List<int>(),
                Extra = (string)null
            };
            if (strArray1.Length == 5)
            {
                int result;
                if (!string.IsNullOrEmpty(strArray1[1]) && int.TryParse(strArray1[1], out result))
                    recurringType.Count = result;
                if (!string.IsNullOrEmpty(strArray1[2]) && int.TryParse(strArray1[2], out result))
                    recurringType.Day = result;
                if (!string.IsNullOrEmpty(strArray1[3]) && int.TryParse(strArray1[3], out result))
                    recurringType.Count2 = result;
                if (!string.IsNullOrEmpty(strArray1[4]))
                {
                    string[] strArray2 = strArray1[4].Split('#');
                    if (!string.IsNullOrEmpty(strArray2[0]))
                    {
                        string str = strArray2[0];
                        char[] chArray = new char[1] { ',' };
                        foreach (string s in str.Split(chArray))
                        {
                            if (int.TryParse(s, out result))
                                recurringType.Days.Add(result);
                        }
                    }
                    if (strArray2.Length > 1)
                        recurringType.Extra = strArray2[1];
                }
            }
            return recurringType;
        }
    }
}

