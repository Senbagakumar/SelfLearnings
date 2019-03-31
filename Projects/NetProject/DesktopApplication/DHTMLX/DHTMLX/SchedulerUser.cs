using DHTMLX.Scheduler.Settings;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Authentication
{
    public enum EditModes
    {
        FullAccess,
        AuthenticatedOnly,
        OwnEventsOnly,
        Forbid,
    }
    public class SchedulerUser : SchedulerSettingsBase
    {
        protected List<string> _hidden = new List<string>()
    {
      "dhx_inner_property_user_id",
      "dhx_inner_property_event_user_id",
      "editMode"
    };
        private DHXEventHandlers _Handler;

        internal SchedulerUser(DHXEventHandlers events)
          : this()
        {
            this.IsEmpty = true;
            this._Handler = events;
        }

        public bool IsEmpty { get; protected set; }

        public string UserIdKey
        {
            get
            {
                return (string)this._properties["dhx_inner_property_user_id"];
            }
            set
            {
                this._properties["dhx_inner_property_user_id"] = (object)string.Format(".currentUser.{0}", (object)value);
            }
        }

        public string EventUserIdKey
        {
            get
            {
                return (string)this._properties["dhx_inner_property_event_user_id"];
            }
            set
            {
                this._properties["dhx_inner_property_event_user_id"] = (object)value;
            }
        }

        public void SetUserDetails(object data)
        {
            this.IsEmpty = false;
            foreach (PropertyInfo property in data.GetType().GetProperties())
                this._properties[property.Name] = property.GetValue(data, (object[])null);
        }

        public EditModes[] Modes
        {
            get
            {
                return (EditModes[])this._properties["editMode"];
            }
            set
            {
                this._properties["editMode"] = (object)value;
            }
        }

        public SchedulerUser()
        {
            this.IsEmpty = true;
            this._properties.SetDefaults(new Dictionary<string, object>()
      {
        {
          "editMode",
          (object) new EditModes[1]
        },
        {
          "dhx_inner_property_user_id",
          (object) "user_id"
        }
      });
        }

        public void SetUserDetails(Dictionary<string, object> data)
        {
            this.IsEmpty = false;
            foreach (KeyValuePair<string, object> keyValuePair in data)
                this._properties[keyValuePair.Key] = keyValuePair.Value;
        }

        public override void Render(StringBuilder builder, string parent)
        {
            this._prefix = parent + ".";
            if (!this.IsEmpty)
            {
                JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
                Dictionary<string, object> dictionary = this._properties.ToDictionary(false);
                foreach (string key in this._hidden)
                {
                    if (dictionary.ContainsKey(key))
                        dictionary.Remove(key);
                }
                builder.Append("\n\t");
                builder.Append(this._prefix);
                builder.Append("currentUser = ");
                builder.Append(scriptSerializer.Serialize((object)dictionary));
                builder.Append(";");
            }
            if (this._properties.IsNullOrDefault("editMode"))
                return;
            SchedulerUserPermissions schedulerUserPermissions = new SchedulerUserPermissions(this._Handler, parent);
            schedulerUserPermissions.Modes = this.Modes;
            schedulerUserPermissions.IsAuthorized = !this.IsEmpty;
            schedulerUserPermissions.UserIdKey = parent + this.UserIdKey;
            if (!string.IsNullOrEmpty(this.EventUserIdKey))
                schedulerUserPermissions.UserIdKeyInEvent = this.EventUserIdKey;
            builder.Append(schedulerUserPermissions.Render());
        }
    }
}

