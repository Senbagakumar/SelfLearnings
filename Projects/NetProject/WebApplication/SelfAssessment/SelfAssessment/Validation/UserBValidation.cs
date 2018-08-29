using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;

namespace SelfAssessment.Validation
{
    public class UserBValidation : IUserBValidation
    {
        private readonly ValidationInformation validation;

        public UserBValidation(ValidationInformation validation)
        {
            this.validation = validation;
            this.validation.ErrorMessages = new List<string>();
        }
        public ValidationInformation RegistrationValidation(UIOrganization uIOrganization)
        {
            if (uIOrganization.Name.Length < 6)
                this.validation.ErrorMessages.Add("OrganizationName length should be 6 or More");

            if (this.validation.ErrorMessages.Count == 0)
                this.validation.IsSuccess = true;

            return this.validation;

        }
    }
}