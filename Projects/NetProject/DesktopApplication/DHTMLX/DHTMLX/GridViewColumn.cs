using DHTMLX.Scheduler.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public class GridViewColumn
    {
        public GridViewColumn(string id)
        {
            this.Id = id;
        }

        public GridViewColumn(string id, string label)
          : this(id)
        {
            this.Label = label;
        }

        public GridViewColumn(string id, string label, int width)
          : this(id, label)
        {
            this.Width = width;
        }

        public string Id { get; set; }

        public string Label { get; set; }

        public int Width { get; set; }

        public GridViewColumn.Aligns Align { get; set; }

        public GridViewColumn.VAligns VAlign { get; set; }

        protected string _sort { get; set; }

        public GridViewColumn.Sort Sorting { get; set; }

        public string Template { get; set; }

        public void SetCustomSort(string funct)
        {
            this.Sorting = GridViewColumn.Sort.Custom;
            this._sort = funct;
        }

        public string Render()
        {
            List<string> stringList = new List<string>();
            if (!string.IsNullOrEmpty(this.Id))
                stringList.Add("\"id\":\"" + this.Id + "\"");
            if (!string.IsNullOrEmpty(this.Label))
                stringList.Add("\"label\":\"" + this.Label + "\"");
            if (this.Width != 0)
                stringList.Add("\"width\":" + this.Width.ToString());
            if (this.Align != GridViewColumn.Aligns.Center)
                stringList.Add("\"align\":\"" + Enum.GetName(typeof(GridViewColumn.Aligns), (object)this.Align).ToLower() + "\"");
            if (this.VAlign != GridViewColumn.VAligns.Top)
                stringList.Add("\"valign\":\"" + Enum.GetName(typeof(GridViewColumn.VAligns), (object)this.VAlign).ToLower() + "\"");
            if (!string.IsNullOrEmpty(this.Template))
                stringList.Add("\"template\":" + EventTemplate.Render(this.Template));
            if (this.Sorting != GridViewColumn.Sort.String)
            {
                string str = (string)null;
                if (this.Sorting == GridViewColumn.Sort.String)
                    str = "\"str\"";
                else if (this.Sorting == GridViewColumn.Sort.Number)
                    str = "\"int\"";
                else if (this.Sorting == GridViewColumn.Sort.Date)
                    str = "\"date\"";
                else if (!string.IsNullOrEmpty(this._sort))
                    str = this._sort;
                stringList.Add("\"sort\":" + str);
            }
            return "{" + string.Join(",", stringList.ToArray()) + "}";
        }

        public static GridViewColumn FromObject(object unparsed)
        {
            if (unparsed == null)
                return (GridViewColumn)null;
            PropertyInfo[] properties1 = unparsed.GetType().GetProperties();
            PropertyInfo propertyInfo1 = ((IEnumerable<PropertyInfo>)properties1).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == "id"));
            if (propertyInfo1 == null)
                return (GridViewColumn)null;
            GridViewColumn gridViewColumn = new GridViewColumn((string)propertyInfo1.GetValue(unparsed, (object[])null));
            string[] properties = new string[7]
            {
        "label",
        "width",
        "align",
        "valign",
        "template",
        "sorting",
        "sort"
            };
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo propertyInfo2 = ((IEnumerable<PropertyInfo>)properties1).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(a => a.Name.ToLower() == properties[i]));
                if (propertyInfo2 != null)
                {
                    object obj = propertyInfo2.GetValue(unparsed, (object[])null);
                    switch (i)
                    {
                        case 0:
                            if (obj is string)
                            {
                                gridViewColumn.Label = (string)obj;
                                continue;
                            }
                            continue;
                        case 1:
                            if (obj is int)
                            {
                                gridViewColumn.Width = (int)obj;
                                continue;
                            }
                            continue;
                        case 2:
                            if (obj is GridViewColumn.Aligns)
                            {
                                gridViewColumn.Align = (GridViewColumn.Aligns)obj;
                                continue;
                            }
                            if (obj is string)
                            {
                                string str = (string)obj;
                                if (str != null)
                                {
                                    string lower = str.ToLower();
                                    if (lower == "left")
                                    {
                                        gridViewColumn.Align = GridViewColumn.Aligns.Left;
                                        continue;
                                    }
                                    if (lower == "right")
                                    {
                                        gridViewColumn.Align = GridViewColumn.Aligns.Right;
                                        continue;
                                    }
                                    if (lower == "center")
                                    {
                                        gridViewColumn.Align = GridViewColumn.Aligns.Center;
                                        continue;
                                    }
                                    continue;
                                }
                                continue;
                            }
                            continue;
                        case 3:
                            if (obj is GridViewColumn.VAligns)
                            {
                                gridViewColumn.VAlign = (GridViewColumn.VAligns)obj;
                                continue;
                            }
                            if (obj is string)
                            {
                                string str = (string)obj;
                                if (str != null)
                                {
                                    string lower = str.ToLower();
                                    if (lower == "top")
                                    {
                                        gridViewColumn.VAlign = GridViewColumn.VAligns.Top;
                                        continue;
                                    }
                                    if (lower == "bottom")
                                    {
                                        gridViewColumn.VAlign = GridViewColumn.VAligns.Bottom;
                                        continue;
                                    }
                                    if (lower == "middle")
                                    {
                                        gridViewColumn.VAlign = GridViewColumn.VAligns.Middle;
                                        continue;
                                    }
                                    continue;
                                }
                                continue;
                            }
                            continue;
                        case 4:
                            if (obj is string)
                            {
                                gridViewColumn.Template = (string)obj;
                                continue;
                            }
                            continue;
                        case 5:
                        case 6:
                            if (obj is GridViewColumn.Sort)
                            {
                                gridViewColumn.Sorting = (GridViewColumn.Sort)obj;
                                continue;
                            }
                            if (obj is string)
                            {
                                string funct = (string)obj;
                                if (funct != null)
                                {
                                    string lower = funct.ToLower();
                                    if (lower == Enum.GetName(typeof(GridViewColumn.Sort), (object)GridViewColumn.Sort.Number).ToLower() || lower == "int")
                                    {
                                        gridViewColumn.Sorting = GridViewColumn.Sort.Number;
                                        continue;
                                    }
                                    if (lower == Enum.GetName(typeof(GridViewColumn.Sort), (object)GridViewColumn.Sort.Date).ToLower())
                                    {
                                        gridViewColumn.Sorting = GridViewColumn.Sort.Date;
                                        continue;
                                    }
                                    if (lower == Enum.GetName(typeof(GridViewColumn.Sort), (object)GridViewColumn.Sort.String).ToLower() || lower == "str")
                                    {
                                        gridViewColumn.Sorting = GridViewColumn.Sort.String;
                                        continue;
                                    }
                                    if (lower != Enum.GetName(typeof(GridViewColumn.Sort), (object)GridViewColumn.Sort.Custom).ToLower())
                                    {
                                        gridViewColumn.SetCustomSort(funct);
                                        continue;
                                    }
                                    continue;
                                }
                                continue;
                            }
                            continue;
                        default:
                            continue;
                    }
                }
            }
            return gridViewColumn;
        }

        public enum Aligns
        {
            Center,
            Right,
            Left,
        }

        public enum VAligns
        {
            Top,
            Middle,
            Bottom,
        }

        public enum Sort
        {
            String,
            Number,
            Date,
            Custom,
        }
    }
}

