using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class Filter
    {
        protected static string _Name = "{0}.filter_{1}";
        protected static string _FunctionBegin = "function(ev_id, event){\n";
        protected static string _FunctionEnd = "\n}";

        public List<FieldRule> Rules { get; set; }

        internal string _ViewName { get; set; }

        public Filter(string viewName)
        {
            this.Rules = new List<FieldRule>();
            this._ViewName = viewName;
        }

        public Filter(Filter clone)
        {
            this.Rules = clone.Rules;
            this._ViewName = clone._ViewName;
        }

        public string RenderName(string parent)
        {
            return string.Format(Filter._Name, (object)parent, (object)this._ViewName);
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (this.Rules.Count <= 0)
                return;
            builder.Append(string.Format("{0} = {1};", (object)this.RenderName(parent), (object)this.RenderBody()));
        }

        public string RenderBody()
        {
            if (this.Rules.Count <= 0)
                return string.Empty;
            string[] strArray1 = new string[this.Rules.Count];
            for (int index = 0; index < this.Rules.Count; ++index)
                strArray1[index] = this.Rules[index].Render();
            string str1 = "";
            string str2 = "";
            if (this.Logic == Logic.AND)
                str1 = string.Join("&&", strArray1);
            else if (this.Logic == Logic.OR)
                str1 = string.Join("||", strArray1);
            else if (this.Logic == Logic.NOT)
                str1 = string.Format("!({0})", (object)string.Join("||", strArray1));
            else if (this.Logic == Logic.XOR)
            {
                string[] strArray2 = new string[strArray1.Length];
                string[] strArray3 = new string[strArray1.Length];
                for (int index = 0; index < strArray1.Length; ++index)
                {
                    strArray3[index] = "r" + index.ToString();
                    strArray2[index] = string.Format("{0} = {1}", (object)strArray3[index], (object)strArray1[index]);
                }
                str2 = string.Format("var {0};\n", (object)string.Join(", ", strArray2));
                str1 = string.Format("({0}) && !({1})", (object)string.Join("||", strArray3), (object)string.Join("&&", strArray3));
            }
            return string.Format("{0}{3}if({1})\n\treturn true;\nelse\n\treturn false;{2}", (object)Filter._FunctionBegin, (object)str1, (object)Filter._FunctionEnd, (object)str2);
        }

        public Logic Logic { get; set; }
    }
}

