using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Api
{
    public interface IDHXServerList : IEnumerable
    {
        string Add(IEnumerable list);

        int Count { get; }

        bool ContainsKey(string key);

        bool ContainsValue(IEnumerable value);

        string GetKey(IEnumerable value);

        void Remove(string key);

        IEnumerable Get(string key);

        string CollectionLink { get; }

        string InitConfigSection { get; }

        string ConfigSection { get; }

        string RenderCollections(string parent);

        void Clear();
    }
    public class DHXServerList : IDHXServerList, IEnumerable
    {
        private static string _CollectionLink = "{0}.serverList(\"{1}\")";
        private static string _CollectionRender = "{0}.serverList(\"{1}\", {2});\n";
        private static string _ConfigSection = "{0}.config.serverLists[\"{1}\"] = \"{2}\";";
        private static string _InitConfigSection = "{0}.config.serverLists = {{}};";
        private Dictionary<string, IEnumerable> _Items;

        private void _CheckItems()
        {
            if (this._Items != null)
                return;
            this._Items = new Dictionary<string, IEnumerable>();
        }

        public string InitConfigSection
        {
            get
            {
                return DHXServerList._InitConfigSection;
            }
        }

        public string ConfigSection
        {
            get
            {
                return DHXServerList._ConfigSection;
            }
        }

        public string CollectionLink
        {
            get
            {
                return DHXServerList._CollectionLink;
            }
        }

        public string GetKey(IEnumerable value)
        {
            if (this._Items == null)
                return (string)null;
            foreach (KeyValuePair<string, IEnumerable> keyValuePair in this._Items)
            {
                if (keyValuePair.Value == value)
                    return keyValuePair.Key;
            }
            return (string)null;
        }

        public IEnumerator GetEnumerator()
        {
            this._CheckItems();
            return (IEnumerator)this._Items.GetEnumerator();
        }

        private string _GetNewKey()
        {
            return string.Format("sl_{0}_{1}", (object)(this.Count + 1), (object)DateTime.Now.Ticks);
        }

        public string Add(IEnumerable list)
        {
            this._CheckItems();
            if (this.ContainsValue(list))
                return this._Items.Single<KeyValuePair<string, IEnumerable>>((Func<KeyValuePair<string, IEnumerable>, bool>)(l => l.Value == list)).Key;
            string newKey = this._GetNewKey();
            this._Items.Add(newKey, list);
            return newKey;
        }

        public int Count
        {
            get
            {
                if (this._Items != null)
                    return this._Items.Count;
                return 0;
            }
        }

        public bool ContainsKey(string key)
        {
            if (this.Count != 0)
                return this._Items.ContainsKey(key);
            return false;
        }

        public bool ContainsValue(IEnumerable value)
        {
            if (this.Count != 0)
                return this._Items.ContainsValue(value);
            return false;
        }

        public void Remove(string key)
        {
            if (this.Count == 0 || !this.ContainsKey(key))
                throw new Exception(string.Format("Server list does not contain the following item:\"{0}\"", (object)key));
            this._Items.Remove(key);
        }

        public IEnumerable Get(string key)
        {
            this._CheckItems();
            if (this.Count != 0 && this.ContainsKey(key))
                return this._Items[key];
            throw new Exception(string.Format("Server list does not contain the following item:\"{0}\"", (object)key));
        }

        public void Clear()
        {
            this._Items = (Dictionary<string, IEnumerable>)null;
        }

        public string RenderCollections(string parent)
        {
            string str = "";
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            foreach (KeyValuePair<string, IEnumerable> keyValuePair in this._Items)
                str += string.Format(DHXServerList._CollectionRender, (object)parent, (object)keyValuePair.Key, (object)scriptSerializer.Serialize((object)keyValuePair.Value));
            return str;
        }
    }
}

