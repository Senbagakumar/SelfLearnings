using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Common
{
    public class CommonPropertiesContainer : PropertiesContainer<object>
    {
        protected virtual CommonPropertiesContainer.PropertyTypes getType(object val)
        {
            Type type = val == null ? typeof(string) : val.GetType();
            if (type == typeof(bool))
                return CommonPropertiesContainer.PropertyTypes.Boolean;
            return type == typeof(string) || type != typeof(int) && type != typeof(byte) && (type != typeof(Decimal) && type != typeof(double)) && (type != typeof(short) && type != typeof(long) && (type != typeof(sbyte) && type != typeof(float))) && (type != typeof(ushort) && type != typeof(uint) && type != typeof(ulong)) ? CommonPropertiesContainer.PropertyTypes.String : CommonPropertiesContainer.PropertyTypes.Numeric;
        }

        public override string Render(string key)
        {
            if (this.IsNullOrDefault(key))
                return "";
            object obj = this._Values[key];
            return this.Render(key, obj);
        }

        public string Render(string key, object value)
        {
            CommonPropertiesContainer.PropertyTypes type = this.getType(value);
            string str = "";
            switch (type)
            {
                case CommonPropertiesContainer.PropertyTypes.Boolean:
                    str = (bool)value ? "true" : "false";
                    break;
                case CommonPropertiesContainer.PropertyTypes.String:
                    str = "'" + value.ToString() + "'";
                    break;
                case CommonPropertiesContainer.PropertyTypes.Numeric:
                    str = value.ToString();
                    break;
            }
            return str;
        }

        protected enum PropertyTypes
        {
            Boolean,
            String,
            Numeric,
            JSONObject,
        }
    }
}