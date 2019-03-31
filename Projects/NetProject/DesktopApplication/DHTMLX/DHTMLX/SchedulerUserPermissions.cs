using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler.Authentication
{
    public class SchedulerUserPermissions
    {
        protected string _Parent { get; set; }

        private DHXEventHandlers _Handlers { get; set; }

        internal SchedulerUserPermissions(DHXEventHandlers handlers, string parent)
        {
            this._Parent = parent;
            this._Handlers = handlers;
        }

        public EditModes[] Modes { get; set; }

        public bool IsAuthorized { get; set; }

        public string UserIdKey { get; set; }

        public string UserIdKeyInEvent { get; set; }

        protected string _forbid()
        {
            return string.Format("\n\t{0}.config.readonly = true;", (object)this._Parent);
        }

        protected string _fullAccess()
        {
            return "";
        }

        protected string _authorizedOnly()
        {
            if (!this.IsAuthorized)
                return this._forbid();
            return this._fullAccess();
        }

        protected string _ownEventsOnly()
        {
            if (string.IsNullOrEmpty(this.UserIdKey) || !this.IsAuthorized)
                return this._forbid();
            string str = "" + string.Format(this._getCondition(), (object)string.Format("{0} == {1}.getEvent(event_id).{2}", (object)this.UserIdKey, (object)this._Parent, (object)this._innerId()), (object)this._Parent);
            this._Handlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onBeforeLightbox, string.Format("return is{0}Editable(event_id);", (object)this._Parent)));
            this._Handlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onBeforeDrag, string.Format("if(mode != 'create') return is{0}Editable(event_id); else return true;", (object)this._Parent)));
            this._Handlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onClick, string.Format("return is{0}Editable(event_id);", (object)this._Parent)));
            this._Handlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onDblClick, string.Format("return is{0}Editable(event_id);", (object)this._Parent)));
            this._Handlers.Events.Add(new DHXSchedulerEvent(DHXSchedulerEvent.Types.onEventCreated, this._addUserIdToNewEvent() + "return true;"));
            return str;
        }

        protected string _innerId()
        {
            string userIdKeyInEvent = this.UserIdKeyInEvent;
            if (string.IsNullOrEmpty(userIdKeyInEvent))
                userIdKeyInEvent = this.UserIdKey.Split('.')[this.UserIdKey.Split('.').Length - 1];
            return userIdKeyInEvent;
        }

        protected string _addUserIdToNewEvent()
        {
            return string.Format("{0}.getEvent(event_id).{1} = {2};", (object)this._Parent, (object)this._innerId(), (object)this.UserIdKey);
        }

        protected string _getCondition()
        {
            return "\n\tvar is{1}Editable = function(event_id){{return {0}}};";
        }

        public SchedulerUserPermissions()
        {
            this.Modes = new EditModes[1];
        }

        protected void clearRepeatedModes()
        {
            List<EditModes> editModesList = new List<EditModes>();
            foreach (EditModes mode in this.Modes)
            {
                if (!editModesList.Contains(mode))
                    editModesList.Add(mode);
            }
            this.Modes = editModesList.ToArray();
        }

        public string Render()
        {
            string str = "";
            this.clearRepeatedModes();
            foreach (EditModes mode in this.Modes)
            {
                switch (mode)
                {
                    case EditModes.AuthenticatedOnly:
                        str += this._authorizedOnly();
                        break;
                    case EditModes.OwnEventsOnly:
                        str += this._ownEventsOnly();
                        break;
                    case EditModes.Forbid:
                        str += this._forbid();
                        break;
                    default:
                        str += this._fullAccess();
                        break;
                }
            }
            return str;
        }
    }
}

