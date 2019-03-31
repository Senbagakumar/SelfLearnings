using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    internal class DHXSchedulerEvent
    {
        private static Dictionary<DHXSchedulerEvent.Types, string> handlers = new Dictionary<DHXSchedulerEvent.Types, string>()
    {
      {
        DHXSchedulerEvent.Types.onBeforeLightbox,
        "\n\t{1}.attachEvent('onBeforeLightbox', function (event_id){{\n\t\t{0}\n\t}});"
      },
      {
        DHXSchedulerEvent.Types.onBeforeDrag,
        "\n\t{1}.attachEvent('onBeforeDrag', function (event_id, mode, native_event_object){{\n\t\t{0}\n\t}});"
      },
      {
        DHXSchedulerEvent.Types.onClick,
        "\n\t{1}.attachEvent('onClick', function (event_id, mode, native_event_object){{\n\t\t{0}\n\t}});"
      },
      {
        DHXSchedulerEvent.Types.onDblClick,
        "\n\t{1}.attachEvent('onDblClick', function (event_id, mode, native_event_object){{\n\t\t{0}\n\t}});"
      },
      {
        DHXSchedulerEvent.Types.onEventCreated,
        "\n\t{1}.attachEvent('onEventCreated', function(event_id){{\n\t\t{0}\n\t}});"
      }
    };

        public DHXSchedulerEvent.Types Type { get; set; }

        public string Code { get; set; }

        public DHXSchedulerEvent(DHXSchedulerEvent.Types type, string code)
        {
            this.Type = type;
            this.Code = code;
        }

        public void Render(StringBuilder builder, string parent)
        {
            if (string.IsNullOrEmpty(this.Code))
                return;
            builder.Append(string.Format(DHXSchedulerEvent.handlers[this.Type], (object)this.Code, (object)parent));
        }

        public enum Types
        {
            onBeforeLightbox,
            onBeforeDrag,
            onClick,
            onDblClick,
            onEventCreated,
        }
    }
}

