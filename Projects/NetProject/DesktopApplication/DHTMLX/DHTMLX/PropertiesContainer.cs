using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace DHTMLX.Common
{
    public class PropertiesContainer<T> : IEnumerable
    {
        protected Dictionary<string, T> _Values = new Dictionary<string, T>();
        protected Dictionary<string, T> _Defaults = new Dictionary<string, T>();

        public void SetDefaults(Dictionary<string, T> values)
        {
            foreach (KeyValuePair<string, T> keyValuePair in values)
            {
                if (this._Defaults.ContainsKey(keyValuePair.Key))
                    this._Defaults[keyValuePair.Key] = keyValuePair.Value;
                else
                    this._Defaults.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public void Add(string key, T value)
        {
            this._Values.Add(key, value);
        }

        public List<string> GetKeys()
        {
            return this._Values.Keys.ToList<string>();
        }

        public void Reset()
        {
            this.Clear();
        }

        public void Clear()
        {
            this._Values.Clear();
        }

        public int Count
        {
            get
            {
                return this._Values.Count;
            }
        }

        public bool ContainsKey(string key)
        {
            return this._Values.ContainsKey(key);
        }

        public bool IsNullOrDefault(string key)
        {
            if (this._Values.ContainsKey(key) && (object)this._Values[key] == null || !this._Values.ContainsKey(key))
                return true;
            if (this._Defaults.ContainsKey(key))
                return this._Values[key].Equals((object)this._Defaults[key]);
            return false;
        }

        public virtual string Render(string key)
        {
            if (this.IsNullOrDefault(key))
                return "";
            return this._Values[key].ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this._Values.GetEnumerator();
        }

        public T this[string key]
        {
            get
            {
                if (this._Values.ContainsKey(key))
                    return this._Values[key];
                if (this._Defaults.ContainsKey(key))
                    return this._Defaults[key];
                return default(T);
            }
            set
            {
                if (!this._Values.ContainsKey(key))
                    this._Values.Add(key, value);
                else
                    this._Values[key] = value;
            }
        }

        public Dictionary<string, T> ToDictionary(bool takeDefaultValues = false)
        {
            Dictionary<string, T> dictionary = new Dictionary<string, T>();
            if (takeDefaultValues)
            {
                foreach (KeyValuePair<string, T> keyValuePair in this._Defaults)
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
            foreach (KeyValuePair<string, T> keyValuePair in this._Values)
            {
                if (dictionary.ContainsKey(keyValuePair.Key))
                    dictionary[keyValuePair.Key] = keyValuePair.Value;
                else
                    dictionary.Add(keyValuePair.Key, keyValuePair.Value);
            }
            return dictionary;
        }
    }
}

