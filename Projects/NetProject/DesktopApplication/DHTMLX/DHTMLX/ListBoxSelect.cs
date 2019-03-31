using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class LightboxSelect : LightboxField
    {
        private ServerListHelper<LightboxSelectOption> m_serverList;
        protected List<LightboxSelectOption> options;

        public string ServerList
        {
            get
            {
                return this.m_serverList.ServerListLink;
            }
            set
            {
                this.m_serverList.ServerListLink = value;
            }
        }

        public LightboxSelect(string name, string label = null)
          : base(name, label)
        {
            this.Type = "select";
            this.options = new List<LightboxSelectOption>();
            this.m_serverList = new ServerListHelper<LightboxSelectOption>(new Action<IEnumerable>(this._AddOptions), (IList<LightboxSelectOption>)this.options);
        }

        public void AddOption(LightboxSelectOption option)
        {
            this.m_serverList.AddOption(option);
        }

        public void AddOptions(IEnumerable opts)
        {
            this.m_serverList.AddOptions(opts);
        }

        public void AddOptions(IEnumerable opts, bool direct)
        {
            if (direct)
                this._AddOptions(opts);
            else
                this.AddOptions(opts);
        }

        protected void _AddOptions(IEnumerable opts)
        {
            if (opts is IEnumerable<LightboxSelectOption>)
            {
                this._AddOptions(opts as IEnumerable<LightboxSelectOption>);
            }
            else
            {
                object obj = (object)null;
                foreach (object opt in opts)
                {
                    LightboxSelectOption option = opt as LightboxSelectOption;
                    if (option != null)
                    {
                        this.AddOption(option);
                    }
                    else
                    {
                        obj = opt;
                        break;
                    }
                }
                if (obj == null)
                    return;
                PropertyInfo[] properties = obj.GetType().GetProperties();
                PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "key"));
                PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "label"));
                if (propertyInfo1 == null && propertyInfo2 == null)
                    return;
                foreach (object opt in opts)
                    this.AddOption(new LightboxSelectOption(propertyInfo1.GetValue(opt, (object[])null), propertyInfo2.GetValue(opt, (object[])null)));
            }
        }

        protected void _AddOptions(IEnumerable<LightboxSelectOption> opts)
        {
            foreach (LightboxSelectOption opt in opts)
                this.AddOption(opt);
        }

        public virtual string RenderParent()
        {
            return this.AfterInit();
        }

        public override string Render(StringBuilder beforeInit, string parent)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            string str1 = this.RenderOptions(beforeInit, parent);
            Dictionary<string, object> visibleProperties = this.GetVisibleProperties();
            string str2 = scriptSerializer.Serialize((object)visibleProperties);
            return str2.Insert(str2.Length - 1, ", \"options\":" + str1);
        }

        public string RenderOptions(StringBuilder beforeInit, string parent)
        {
            if (!this.m_serverList.IsEmptyServerList)
                return this.m_serverList.RenderCollection(parent);
            List<string> stringList = new List<string>();
            foreach (LightboxSelectOption option in this.options)
                stringList.Add(option.Render(beforeInit, parent));
            return string.Format("[{0}]", (object)string.Join(", ", stringList.ToArray()));
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            foreach (Dictionary<string, object> dictionary in data["options"] as object[])
                this.AddOption(new LightboxSelectOption((object)dictionary["key"].ToString(), (object)dictionary["label"].ToString()));
        }

        public override Dictionary<string, object> Serialize()
        {
            Dictionary<string, object> dictionary = base.Serialize();
            object[] objArray = new object[this.options.Count];
            for (int index = 0; index < this.options.Count; ++index)
                objArray[index] = (object)new Dictionary<string, object>()
        {
          {
            "key",
            this.options[index].Key
          },
          {
            "label",
            this.options[index].Label
          }
        };
            dictionary.Add("options", (object)objArray);
            return dictionary;
        }
    }
}
