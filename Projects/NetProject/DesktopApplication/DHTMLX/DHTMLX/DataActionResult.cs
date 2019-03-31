using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

namespace DHTMLX.Common
{
    public enum DataActionTypes
    {
        Insert,
        Update,
        Delete,
        Error,
    }
    public class DataActionResult
    {
        public static Dictionary<DataActionTypes, string> Types = new Dictionary<DataActionTypes, string>()
    {
      {
        DataActionTypes.Insert,
        "inserted"
      },
      {
        DataActionTypes.Update,
        "updated"
      },
      {
        DataActionTypes.Delete,
        "deleted"
      },
      {
        DataActionTypes.Error,
        "error"
      }
    };

        public object Event { get; protected set; }

        public static DataActionTypes ParseType(string type)
        {
            if (type == null || !DataActionResult.Types.ContainsValue(type))
                return DataActionTypes.Error;
            return DataActionResult.Types.SingleOrDefault<KeyValuePair<DataActionTypes, string>>((Func<KeyValuePair<DataActionTypes, string>, bool>)(a => a.Value == type)).Key;
        }

        public DataAction Result { get; protected set; }

        public object ParseItem(NameValueCollection data)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (string key in ((IEnumerable<string>)data.AllKeys).Where<string>((Func<string, bool>)(key => key != "!nativeeditor_status")))
                dictionary.Add(key, (object)data[key]);
            this.Event = (object)dictionary;
            return (object)dictionary;
        }

        public DataActionResult(NameValueCollection data)
        {
            string status = data["!nativeeditor_status"];
            long source_id = long.Parse(data["id"]);
            long target_id = source_id;
            this.Result = new DataAction(status, source_id, target_id);
            this.Event = this.ParseItem(data);
        }
    }
}
