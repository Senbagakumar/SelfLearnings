using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.Validation
{
    public interface IUserBValidation
    {
        ValidationInformation RegistrationValidation(UIOrganization uIOrganization);
    }
}
