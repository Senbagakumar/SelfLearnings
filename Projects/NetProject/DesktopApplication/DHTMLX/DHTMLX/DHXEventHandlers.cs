using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    internal class DHXEventHandlers
    {
        private List<DHXSchedulerEvent> _Events;

        public bool HasEvents
        {
            get
            {
                return this._Events != null;
            }
        }

        public List<DHXSchedulerEvent> Events
        {
            get
            {
                if (this._Events == null)
                    this._Events = new List<DHXSchedulerEvent>();
                return this._Events;
            }
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (!this.HasEvents)
                return;
            for (int index = 0; index < this.Events.Count; ++index)
                this.Events[index].Render(builder, parent);
        }
    }
}

