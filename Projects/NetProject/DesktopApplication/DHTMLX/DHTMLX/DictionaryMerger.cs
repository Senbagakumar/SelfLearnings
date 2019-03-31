using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX
{
    public class DictionaryMerger
    {
        public static Dictionary<string, FileType> Merge(IEnumerable<Dictionary<string, FileType>> dictionaries)
        {
            Dictionary<string, FileType> dictionary1 = new Dictionary<string, FileType>();
            foreach (Dictionary<string, FileType> dictionary2 in dictionaries)
            {
                foreach (KeyValuePair<string, FileType> keyValuePair in dictionary2)
                    dictionary1[keyValuePair.Key] = keyValuePair.Value;
            }
            return dictionary1;
        }

        public static Dictionary<string, FileType> Merge(Dictionary<string, FileType> first, Dictionary<string, FileType> second)
        {
            Dictionary<string, FileType> dictionary = new Dictionary<string, FileType>((IDictionary<string, FileType>)first);
            foreach (KeyValuePair<string, FileType> keyValuePair in second)
                dictionary[keyValuePair.Key] = keyValuePair.Value;
            return dictionary;
        }

        public static void Join(ref Dictionary<string, FileType> first, Dictionary<string, FileType> target)
        {
            foreach (KeyValuePair<string, FileType> keyValuePair in target)
                first[keyValuePair.Key] = keyValuePair.Value;
        }
    }
}
