using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace DHTMLX.Common
{
    public class DataAction
    {
        protected string status;

        public DataActionTypes Type
        {
            get
            {
                return DataActionResult.ParseType(this.status);
            }
            set
            {
                this.status = DataActionResult.Types[value];
            }
        }

        public long SourceId { get; set; }

        public long TargetId { get; set; }

        public string Message { get; set; }

        public DataAction(NameValueCollection form)
        {
            this.Type = DataActionResult.ParseType(form["!nativeeditor_status"]);
            if (form["id"] != null)
            {
                this.SourceId = long.Parse(form["id"]);
                this.TargetId = long.Parse(form["id"]);
            }
            else
            {
                this.SourceId = 0L;
                this.TargetId = 0L;
            }
        }

        public DataAction(string status, int source_id, int target_id)
        {
            this.Type = DataActionResult.ParseType(status);
            this.SourceId = (long)source_id;
            this.TargetId = (long)target_id;
        }

        public DataAction(string status, long source_id, long target_id)
        {
            this.Type = DataActionResult.ParseType(status);
            this.SourceId = source_id;
            this.TargetId = target_id;
        }

        public DataAction(DataActionTypes status, int source_id, int target_id)
        {
            this.Type = status;
            this.SourceId = (long)source_id;
            this.TargetId = (long)target_id;
        }
    }
}

