using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Business
{
    public interface IBusinessContract
    {
        ValidationInformation UserCreation(UIOrganization uIOrganization);
        void SendRegistrationMail(string userName,string passWord);
    }
}
