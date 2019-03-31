using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class DHXBlockTime : DHXTimeSpan
    {
        private string _template = "\n\t{0}.blockTime({{{1}}});";

        public DHXBlockTime()
        {
            this._Template = this._template;
        }
    }
}

