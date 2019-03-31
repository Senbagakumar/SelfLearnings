using DHTMLX.Scheduler.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class SchedulerExtensions : SchedulerControlsBase
    {
        protected Dictionary<SchedulerExtensions.Extension, string> translate = new Dictionary<SchedulerExtensions.Extension, string>()
    {
      {
        SchedulerExtensions.Extension.ActiveLinks,
        "dhtmlxscheduler_active_links.js"
      },
      {
        SchedulerExtensions.Extension.AgendaView,
        "dhtmlxscheduler_agenda_view.js"
      },
      {
        SchedulerExtensions.Extension.Collision,
        "dhtmlxscheduler_collision.js"
      },
      {
        SchedulerExtensions.Extension.Cookie,
        "dhtmlxscheduler_cookie.js"
      },
      {
        SchedulerExtensions.Extension.Editors,
        "dhtmlxscheduler_editors.js"
      },
      {
        SchedulerExtensions.Extension.Expand,
        "dhtmlxscheduler_expand.js"
      },
      {
        SchedulerExtensions.Extension.HtmlTemplates,
        "dhtmlxscheduler_html_templates.js"
      },
      {
        SchedulerExtensions.Extension.KeyboardNavigation,
        "dhtmlxscheduler_key_nav.js"
      },
      {
        SchedulerExtensions.Extension.Limit,
        "dhtmlxscheduler_limit.js"
      },
      {
        SchedulerExtensions.Extension.MapView,
        "dhtmlxscheduler_map_view.js"
      },
      {
        SchedulerExtensions.Extension.Matrix,
        "dhtmlxscheduler_matrix.js"
      },
      {
        SchedulerExtensions.Extension.Minical,
        "dhtmlxscheduler_minical.js"
      },
      {
        SchedulerExtensions.Extension.Multiselect,
        "dhtmlxscheduler_multiselect.js"
      },
      {
        SchedulerExtensions.Extension.Multisource,
        "dhtmlxscheduler_multisource.js"
      },
      {
        SchedulerExtensions.Extension.Offline,
        "dhtmlxscheduler_offline.js"
      },
      {
        SchedulerExtensions.Extension.OuterDrag,
        "dhtmlxscheduler_outerdrag.js"
      },
      {
        SchedulerExtensions.Extension.PDF,
        "dhtmlxscheduler_pdf.js"
      },
      {
        SchedulerExtensions.Extension.Readonly,
        "dhtmlxscheduler_readonly.js"
      },
      {
        SchedulerExtensions.Extension.Serialize,
        "dhtmlxscheduler_serialize.js"
      },
      {
        SchedulerExtensions.Extension.Tooltip,
        "dhtmlxscheduler_tooltip.js"
      },
      {
        SchedulerExtensions.Extension.TreeTimeline,
        "dhtmlxscheduler_treetimeline.js"
      },
      {
        SchedulerExtensions.Extension.Url,
        "dhtmlxscheduler_url.js"
      },
      {
        SchedulerExtensions.Extension.WeekAgenda,
        "dhtmlxscheduler_week_agenda.js"
      },
      {
        SchedulerExtensions.Extension.YearView,
        "dhtmlxscheduler_year_view.js"
      },
      {
        SchedulerExtensions.Extension.Recurring,
        "dhtmlxscheduler_recurring.js"
      },
      {
        SchedulerExtensions.Extension.GridView,
        "dhtmlxscheduler_grid_view.js"
      }
    };
        protected Dictionary<SchedulerExtensions.Extension, string> translateCss = new Dictionary<SchedulerExtensions.Extension, string>();
        protected Dictionary<string, FileType> _files = new Dictionary<string, FileType>();
        protected Dictionary<string, FileType> _style = new Dictionary<string, FileType>();

        internal override Dictionary<string, FileType> GetCSS()
        {
            return this._style;
        }

        internal override Dictionary<string, FileType> GetJS()
        {
            return this._files;
        }

        public override void Clear()
        {
            this._files.Clear();
            this.Items.Clear();
        }

        public override bool IsSet()
        {
            return this._files.Count != 0;
        }

        public void Add(SchedulerExtensions.Extension ext)
        {
            this._files.Add("ext/" + this.translate[ext], FileType.Local);
            if (this.translateCss.ContainsKey(ext))
                this._style.Add("ext/" + this.translateCss[ext], FileType.Local);
            if (this.Items == null)
                this.Items = new Dictionary<SchedulerExtensions.Extension, DHXExtension>();
            if (ext != SchedulerExtensions.Extension.Collision)
                return;
            this.Items.Add(SchedulerExtensions.Extension.Collision, (DHXExtension)new DHXCollisionExtension());
        }

        public override void Render(StringBuilder builder, string parent)
        {
        }

        public Dictionary<SchedulerExtensions.Extension, DHXExtension> Items { get; set; }

        public enum Extension
        {
            ActiveLinks,
            AgendaView,
            Collision,
            Cookie,
            Editors,
            Expand,
            HtmlTemplates,
            KeyboardNavigation,
            Limit,
            MapView,
            Matrix,
            Minical,
            Multiselect,
            Multisource,
            Offline,
            OuterDrag,
            PDF,
            Readonly,
            Serialize,
            Tooltip,
            TreeTimeline,
            Url,
            WeekAgenda,
            GridView,
            YearView,
            Recurring,
        }
    }
}

