using DHTMLX.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class SchedulerControlsItem
    {
        protected CommonPropertiesContainer _properties = new CommonPropertiesContainer();
        protected List<string> _hiddenProperties = new List<string>();

        public void Set(string name, object value)
        {
            this._properties[name] = value;
        }

        public object GetObject(string name)
        {
            return this._properties[name];
        }

        public string Get(string name)
        {
            return (string)this._properties[name];
        }

        public DateTime GetDate(string name)
        {
            return (DateTime)this._properties[name];
        }

        public object GetRaw(string name)
        {
            return this._properties[name];
        }

        public int GetInt(string name)
        {
            if (this._properties.ContainsKey(name))
                return int.Parse(this._properties[name].ToString());
            return 0;
        }

        public bool GetBool(string name)
        {
            if (this._properties[name] == null)
                return false;
            return bool.Parse(this._properties[name].ToString());
        }

        public virtual Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>();
        }

        public virtual Dictionary<string, FileType> GetCSS()
        {
            return new Dictionary<string, FileType>();
        }

        public virtual void Deserialize(Dictionary<string, object> data)
        {
            foreach (KeyValuePair<string, object> keyValuePair in data)
                this.Set(keyValuePair.Key, keyValuePair.Value);
        }

        public virtual Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> property in (PropertiesContainer<object>)this._properties)
                dictionary.Add(property.Key, property.Value);
            return dictionary;
        }

        public Dictionary<string, object> GetVisibleProperties()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> property in (PropertiesContainer<object>)this._properties)
            {
                if (!this._hiddenProperties.Contains(property.Key))
                    dictionary.Add(property.Key, property.Value);
            }
            return dictionary;
        }
    }
    public class Rule : FieldRule
    {
        public string Field { get; set; }

        public object Value { get; set; }

        public Operator Operator { get; set; }

        public Rule(string RuleField, Operator CompareOperator, object CompareValue)
        {
            this.Field = RuleField;
            this.Operator = CompareOperator;
            this.Value = CompareValue;
        }

        public override string Render()
        {
            if (this.Value == null || this.Field == null)
                return this._Default;
            string str1;
            switch (this.Operator)
            {
                case Operator.Equals:
                    str1 = "==";
                    break;
                case Operator.NotEquals:
                    str1 = "!=";
                    break;
                case Operator.Identical:
                    str1 = "===";
                    break;
                case Operator.NotIdentical:
                    str1 = "!==";
                    break;
                case Operator.Greater:
                    str1 = ">";
                    break;
                case Operator.GreaterOrEqual:
                    str1 = ">=";
                    break;
                case Operator.Lower:
                    str1 = "<";
                    break;
                case Operator.LowerOrEqual:
                    str1 = "<=";
                    break;
                case Operator.GreaterOrIdentical:
                    str1 = ">==";
                    break;
                case Operator.LowerOrIdentical:
                    str1 = "<==";
                    break;
                default:
                    throw new NotImplementedException(this.Operator.ToString() + " operator support has not been implemented yet!");
            }
            string format = "(event.{0} {1} {2})";
            string str2 = this.Value == null || this.Value.GetType() != typeof(DateTime) ? JSONHelper.ToJSON(this.Value) : DateFormatHelper.DateToJS((DateTime)this.Value);
            return string.Format(format, (object)this.Field, (object)str1, (object)str2);
        }
    }
}
