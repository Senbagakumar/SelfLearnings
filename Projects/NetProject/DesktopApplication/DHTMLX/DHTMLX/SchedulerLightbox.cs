using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class SchedulerLightboxFieldFactory
    {
        public static LightboxField Create(string type, string name)
        {
            switch (type)
            {
                case "textarea":
                    return (LightboxField)new LightboxText(name, (string)null);
                case "select":
                    return (LightboxField)new LightboxSelect(name, (string)null);
                case "radio":
                    return (LightboxField)new LightboxRadio(name, (string)null);
                case "multiselec":
                    return (LightboxField)new LightboxMultiselect(name, (string)null);
                case "time":
                    return (LightboxField)new LightboxTime(name, (string)null);
                case "calendar_time":
                    return (LightboxField)new LightboxMiniCalendar(name, (string)null);
                case "checkbox":
                    return (LightboxField)new LightboxCheckbox(name, (string)null);
                case "recurring":
                    return (LightboxField)new LightboxRecurringBlock(name, (string)null);
                default:
                    throw new Exception("Unknown type " + type);
            }
        }
    }
    public class SchedulerLightbox : SchedulerControlsBase
    {
        protected bool externalLighbox;

        protected override void _renderStart(StringBuilder builder, string parent)
        {
            this._prefix = parent + ".lightbox";
            base._renderStart(builder, parent);
        }

        protected override void _renderContent(StringBuilder builder, string parent)
        {
            if (!this.externalLighbox)
            {
                List<string> stringList = new List<string>();
                JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
                foreach (SchedulerControlsItem schedulerControlsItem in this.data)
                {
                    builder.Append((schedulerControlsItem as LightboxField).RenderLabel(parent));
                    stringList.Add((schedulerControlsItem as LightboxField).Render(builder, parent));
                }
                builder.Append(string.Format("\n{0}.config.lightbox.sections = [{1}];\n", (object)parent, (object)string.Join(",\n", stringList.ToArray())));
            }
            else
                builder.Append((this.data.First<SchedulerControlsItem>() as ExternalLightboxForm).Render(builder, parent));
        }

        internal override string AfterInit()
        {
            string str = "";
            foreach (SchedulerControlsItem schedulerControlsItem in this.data)
                str += (schedulerControlsItem as LightboxItem).AfterInit();
            return str;
        }

        public override void Clear()
        {
            this.externalLighbox = false;
            this.data.Clear();
        }

        public virtual void Add(LightboxItem obj)
        {
            if (obj is ExternalLightboxForm)
                this.externalLighbox = true;
            if (this.externalLighbox)
                this.Clear();
            if (obj is ExternalLightboxForm)
                this.externalLighbox = true;
            this.data.Add((SchedulerControlsItem)obj);
        }

        internal override void Deserialize(object data)
        {
            this.Clear();
            object[] objArray = data as object[];
            ((IEnumerable<object>)objArray).Count<object>();
            foreach (Dictionary<string, object> data1 in objArray)
            {
                if (data1.ContainsKey("type"))
                {
                    LightboxField lightboxField = SchedulerLightboxFieldFactory.Create(data1["type"].ToString(), data1["name"].ToString());
                    lightboxField.Deserialize(data1);
                    this.Add((LightboxItem)lightboxField);
                }
            }
        }

        public ExternalLightboxForm SetExternalLightboxForm(string viewName)
        {
            ExternalLightboxForm externalLightboxForm = new ExternalLightboxForm(viewName);
            this.Add((LightboxItem)externalLightboxForm);
            externalLightboxForm.Width = 600;
            externalLightboxForm.Height = 400;
            return externalLightboxForm;
        }

        public ExternalLightboxForm SetExternalLightboxForm(string viewName, int width, int height)
        {
            ExternalLightboxForm externalLightboxForm = this.SetExternalLightboxForm(viewName);
            externalLightboxForm.Width = width;
            externalLightboxForm.Height = height;
            return externalLightboxForm;
        }

        public ExternalLightboxForm SetExternalLightboxForm(string viewName, string className)
        {
            ExternalLightboxForm externalLightboxForm = this.SetExternalLightboxForm(viewName);
            externalLightboxForm.ClassName = className;
            return externalLightboxForm;
        }

        public ExternalLightboxControl SetExternalLightbox(string viewName)
        {
            ExternalLightboxControl externalLightboxControl = new ExternalLightboxControl(viewName);
            this.Add((LightboxItem)externalLightboxControl);
            externalLightboxControl.Width = 600;
            externalLightboxControl.Height = 400;
            return externalLightboxControl;
        }

        public ExternalLightboxControl SetExternalLightbox(string viewName, int width, int height)
        {
            ExternalLightboxControl externalLightboxControl = this.SetExternalLightbox(viewName);
            externalLightboxControl.Width = width;
            externalLightboxControl.Height = height;
            return externalLightboxControl;
        }

        public ExternalLightboxControl SetExternalLightbox(string viewName, string className)
        {
            ExternalLightboxControl externalLightboxControl = this.SetExternalLightbox(viewName);
            externalLightboxControl.ClassName = className;
            return externalLightboxControl;
        }

        public void AddDefaults()
        {
            LightboxText lightboxText = new LightboxText("description", (string)null);
            lightboxText.Height = 200;
            lightboxText.MapTo = "text";
            this.Add((LightboxItem)lightboxText);
            LightboxTime lightboxTime = new LightboxTime("time", (string)null);
            lightboxTime.MapTo = "auto";
            lightboxTime.Height = 72;
            this.Add((LightboxItem)lightboxTime);
        }
    }
}

