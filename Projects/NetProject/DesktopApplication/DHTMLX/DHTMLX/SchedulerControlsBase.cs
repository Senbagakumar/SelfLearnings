using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Controls
{
    public abstract class SchedulerControlsBase
    {
        protected List<SchedulerControlsItem> data = new List<SchedulerControlsItem>();
        protected string _prefix;

        protected Dictionary<string, FileType> _getAttached(SourceType ext)
        {
            if (!this.IsSet())
                return new Dictionary<string, FileType>();
            Dictionary<string, FileType> first = new Dictionary<string, FileType>();
            List<string> stringList = new List<string>();
            foreach (SchedulerControlsItem schedulerControlsItem in this.data)
            {
                switch (ext)
                {
                    case SourceType.JS:
                        DictionaryMerger.Join(ref first, schedulerControlsItem.GetJS());
                        continue;
                    case SourceType.CSS:
                        DictionaryMerger.Join(ref first, schedulerControlsItem.GetCSS());
                        continue;
                    default:
                        continue;
                }
            }
            return first;
        }

        internal virtual Dictionary<string, FileType> GetJS()
        {
            return this._getAttached(SourceType.JS);
        }

        internal virtual Dictionary<string, FileType> GetCSS()
        {
            return this._getAttached(SourceType.CSS);
        }

        protected virtual void _renderStart(StringBuilder builder, string parent)
        {
        }

        protected virtual void _renderEnd(StringBuilder builder, string parent)
        {
        }

        protected virtual void _renderContent(StringBuilder builder, string parent)
        {
        }

        public virtual void Render(StringBuilder builder, string parent)
        {
            if (!this.IsSet())
                return;
            this._renderStart(builder, parent);
            this._renderContent(builder, parent);
            this._renderEnd(builder, parent);
        }

        internal virtual string AfterInit()
        {
            return "";
        }

        internal virtual object[] Serialize()
        {
            object[] objArray = new object[this.data.Count];
            for (int index = 0; index < this.data.Count; ++index)
                objArray[index] = (object)this.data[index].Serialize();
            return objArray;
        }

        internal virtual void Deserialize(object data)
        {
        }

        public virtual bool IsSet()
        {
            return this.data.Count != 0;
        }

        public virtual void Clear()
        {
            this.data.Clear();
        }

        public List<SchedulerControlsItem> Items
        {
            get
            {
                return this.data;
            }
        }
    }
}

