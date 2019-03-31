using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace DHTMLX
{
    public sealed class DHTMLX
    {
        public DHTMLX()
        {
            //TimeCheck.Check();
        }

        public string Scheduler(IEnumerable data)
        {
            DHXScheduler dhxScheduler = new DHXScheduler();
            dhxScheduler.Data.Parse(data);
            return dhxScheduler.Render();
        }

        public string Scheduler(object model)
        {
            IEnumerable data = model as IEnumerable;
            if (data != null)
                return this.Scheduler(data);
            DHXScheduler config = model as DHXScheduler;
            if (config != null)
                return this.Scheduler(config);
            return this.Scheduler();
        }

        public string Scheduler()
        {
            return new DHXScheduler().Render();
        }

        public string Scheduler(DHXScheduler config)
        {
            return config.Render();
        }

        public string Scheduler(object config, object data)
        {
            DHXScheduler dhxScheduler1 = config as DHXScheduler;
            if (dhxScheduler1 == null)
                return "";
            DHXScheduler dhxScheduler2 = dhxScheduler1;
            IEnumerable data1 = data as IEnumerable;
            if (data1 != null)
                dhxScheduler2.Data.Parse(data1);
            return dhxScheduler2.Render();
        }

        public string ToICal(string button_text, string url)
        {
            return this.ToICal(button_text, url, "scheduler_toICal_control");
        }

        public string ToICal(string button_text, string url, string id)
        {
            return string.Format("<form id=\"{1}\" method=\"post\" target=\"_blank\" action=\"{0}\" accept-charset=\"utf-8\" enctype=\"application/x-www-form-urlencoded\">", (object)url, (object)id) + "<input type=\"hidden\" name=\"data\" value=\"\"/>" + string.Format("<input type=\"button\" value=\"{0}\" class=\"dhx-export dhx-export-ical\"", (object)button_text) + "onclick=\"this.parentNode.firstChild.value = scheduler.toICal();this.parentNode.submit();\" />" + "</form>";
        }

        public string ToPDF(string button_text, string url)
        {
            return this.ToPDF(button_text, url, "scheduler_toPDF_control");
        }

        public string ToPDF(string button_text, string url, string id)
        {
            return string.Format("<input class=\"dhx-export dhx-export-pdf\" id=\"{0}\" type=\"button\" value=\"{1}\" onclick=\"scheduler.toPDF('{2}');\" />", (object)id, (object)button_text, (object)url);
        }
    }
}
