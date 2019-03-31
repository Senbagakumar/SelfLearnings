using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DHTMLX.Common
{
    public sealed class AjaxSaveResponse
    {
        public string DateFormat = "%m/%d/%Y %H:%i";
        private DataAction action;
        private CommonPropertiesContainer updateFields;

        private string getUpdatedFieldsString()
        {
            if (this.updateFields == null || this.action.Type == DataActionTypes.Delete)
                return "";
            string net = DateFormatHelper.ConvertInnerFormatToNet(this.DateFormat);
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            string str1 = "";
            foreach (string key in this.updateFields.GetKeys())
            {
                string str2 = this.updateFields[key].GetType() != Type.GetType("System.DateTime") ? (this.updateFields[key].GetType() != Type.GetType("System.Boolean") ? scriptSerializer.Serialize(this.updateFields[key]).Replace("\"", "") : ((bool)this.updateFields[key] ? "1" : "0")) : string.Format("{0:" + net + "}", (object)(DateTime)this.updateFields[key]);
                str1 += string.Format(" dhx_{0}=\"{1}\"", (object)key, (object)str2);
            }
            return str1;
        }

        public void UpdateField(string name, object value)
        {
            if (this.updateFields == null)
                this.updateFields = new CommonPropertiesContainer();
            this.updateFields.Add(name, value);
        }

        public void UpdateFields(Dictionary<string, object> fields)
        {
            if (this.updateFields == null)
                this.updateFields = new CommonPropertiesContainer();
            foreach (string key in fields.Keys)
                this.updateFields.Add(key, fields[key]);
        }

        public AjaxSaveResponse(NameValueCollection item)
          : this()
        {
            string status = item["!nativeeditor_status"];
            int num = int.Parse(item["id"]);
            this.action = new DataAction(status, num, num);
        }

        public AjaxSaveResponse(DataAction action)
          : this()
        {
            this.action = action;
        }

        private AjaxSaveResponse()
        {
            //TimeCheck.Check();
        }

        public string Render()
        {
            string str = string.IsNullOrEmpty(this.action.Message) ? "" : string.Format("<![CDATA[{0}]]>", (object)this.action.Message);
            return string.Format("<data><action type=\"{0}\" sid=\"{1}\" tid=\"{2}\"{3}>{4}</action></data>", (object)DataActionResult.Types[this.action.Type], (object)this.action.SourceId.ToString(), (object)this.action.TargetId.ToString(), (object)this.getUpdatedFieldsString(), (object)str);
        }

        public static implicit operator string(AjaxSaveResponse data)
        {
            return data.Render();
        }

        public override string ToString()
        {
            return this.Render();
        }

        public static implicit operator ContentResult(AjaxSaveResponse data)
        {
            return AjaxSaveResponse.ToContentResult(data);
        }

        public static ContentResult ToContentResult(AjaxSaveResponse data)
        {
            return new ContentResult()
            {
                Content = data.Render(),
                ContentType = "text/xml"
            };
        }
    }
}
