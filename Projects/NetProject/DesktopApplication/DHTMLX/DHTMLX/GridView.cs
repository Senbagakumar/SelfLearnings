using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class GridView : SchedulerView
    {
        public int RowHeight
        {
            get
            {
                return this.GetInt("rowHeight");
            }
            set
            {
                this.Set("rowHeight", (object)value);
            }
        }

        public DateTime From
        {
            get
            {
                return (DateTime)this.GetRaw("from");
            }
            set
            {
                this.Set("from", (object)value);
            }
        }

        public DateTime To
        {
            get
            {
                return (DateTime)this.GetRaw("to");
            }
            set
            {
                this.Set("to", (object)value);
            }
        }

        public bool Select
        {
            get
            {
                return this.GetBool("select");
            }
            set
            {
                this.Set("select", (object)value);
            }
        }

        public bool Paging
        {
            get
            {
                return this.GetBool("paging");
            }
            set
            {
                this.Set("paging", (object)value);
            }
        }

        public List<GridViewColumn> Columns { get; set; }

        public GridView()
        {
            this._hiddenProperties.Add("label");
            this._hiddenProperties.Add("tabPosition");
            this._hiddenProperties.Add("view_type");
            this.ViewType = "grid";
            this.Select = true;
            this.Paging = false;
            this.Columns = new List<GridViewColumn>();
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          "select",
          (object) true
        },
        {
          "paging",
          (object) false
        }
      });
        }

        public GridView(string name)
          : this()
        {
            this.Name = name;
        }

        public GridView(IEnumerable columns)
          : this()
        {
            this.AddOptions(columns);
        }

        public void AddOption(GridViewColumn column)
        {
            this.Columns.Add(column);
        }

        public void AddOption(object column)
        {
            GridViewColumn gridViewColumn = GridViewColumn.FromObject(column);
            if (gridViewColumn == null)
                return;
            this.Columns.Add(gridViewColumn);
        }

        public void AddOptions(IEnumerable columns)
        {
            foreach (object column in columns)
            {
                if (column is GridViewColumn)
                {
                    this.Columns.Add(column as GridViewColumn);
                }
                else
                {
                    GridViewColumn gridViewColumn = GridViewColumn.FromObject(column);
                    if (gridViewColumn != null)
                        this.Columns.Add(gridViewColumn);
                }
            }
        }

        public override void Render(StringBuilder builder, string parent)
        {
            base.Render(builder, parent);
            builder.Append("\n");
            builder.Append(string.Format("\n{0}.createGridView(", (object)parent));
            Dictionary<string, object> visibleProperties = this.GetVisibleProperties();
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            List<string> stringList1 = new List<string>();
            foreach (KeyValuePair<string, object> keyValuePair in visibleProperties)
            {
                if (!this._properties.IsNullOrDefault(keyValuePair.Key))
                {
                    if (keyValuePair.Key != "from" && keyValuePair.Key != "to")
                        stringList1.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, (object)scriptSerializer.Serialize(keyValuePair.Value)));
                    else if (keyValuePair.Value is DateTime && (DateTime)keyValuePair.Value != new DateTime())
                    {
                        DateTime dateTime = (DateTime)keyValuePair.Value;
                        string str = string.Format("new Date({0},{1},{2})", (object)dateTime.Year, (object)(dateTime.Month - 1), (object)dateTime.Day);
                        stringList1.Add(string.Format("\"{0}\":{1}", (object)keyValuePair.Key, (object)str));
                    }
                }
            }
            if (this.Columns.Count == 0)
            {
                this.Columns.Add(new GridViewColumn("date", "Date", 200));
                this.Columns.Add(new GridViewColumn("text", "Description"));
            }
            List<string> stringList2 = new List<string>();
            foreach (GridViewColumn column in this.Columns)
                stringList2.Add(column.Render());
            stringList1.Add(string.Format("\"fields\":[{0}]", (object)string.Join(", ", stringList2.ToArray())));
            builder.Append("{");
            builder.Append(string.Join(",\n", stringList1.ToArray()));
            builder.Append("});");
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "ext/dhtmlxscheduler_grid_view.js",
          FileType.Local
        }
      };
        }
    }
}

