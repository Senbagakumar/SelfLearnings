using DHTMLX.Scheduler.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class DHXCollisionExtension : DHXExtension
    {
        public bool HandleDifferentResources = true;

        public override void Render(StringBuilder builder, DHXScheduler par)
        {
            if (!this.HandleDifferentResources)
                return;
            List<SchedulerView> schedulerViewList = new List<SchedulerView>();
            for (int index = 0; index < par.Views.Count; ++index)
            {
                if (par.Views[index] is UnitsView || par.Views[index] is TimelineView)
                    schedulerViewList.Add(par.Views[index]);
            }
            if (schedulerViewList.Count == 0)
                return;
            List<string> stringList = new List<string>();
            foreach (SchedulerView schedulerView in schedulerViewList)
            {
                UnitsView unitsView = schedulerView as UnitsView;
                if (unitsView != null)
                {
                    stringList.Add(unitsView.Property);
                }
                else
                {
                    TimelineView timelineView = schedulerView as TimelineView;
                    if (timelineView != null)
                        stringList.Add(timelineView.Y_Property);
                }
            }
            string[] strArray = new string[stringList.Count];
            for (int index = 0; index < stringList.Count; ++index)
                strArray[index] = string.Format("(ev.{0} == evs[i].{0})", (object)stringList[index]);
            builder.Append(string.Format("\n\t{0}.attachEvent(\"onEventCollision\", function(ev, evs){{ var c = 0, l = {0}.config.collision_limit;for (var i=0; i<evs.length; i++) {{  if (", (object)par.Name));
            builder.Append(string.Join("&&", strArray));
            builder.Append("&& ev.id != evs[i].id) c++; } return !(c < l);});");
        }
    }
}

