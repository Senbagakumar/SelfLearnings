using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.DataAccess;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;

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
            Repository<Organization> repository = new Repository<Organization>();
            if (uIOrganization.Name.Length < 6)
            {
                UserException.LogInformation("OrganizationName length should be 6 or More");
                this.validation.ErrorMessages.Add("OrganizationName length should be 6 or More");
            }
            else
            {
                var IsOrganization = repository.Find(t => t.IsActive && t.Name == uIOrganization.Name);
                if(IsOrganization!=null && !string.IsNullOrEmpty(IsOrganization.Name))
                {
                    UserException.LogInformation("OrganizationName name is already present"+ uIOrganization.Name);
                    this.validation.ErrorMessages.Add("OrganizationName is already present, Can you try some other name");
                }
                else
                {
                    IsOrganization = repository.Find(t => t.IsActive && t.Email == uIOrganization.Email);
                    if(IsOrganization!=null && !string.IsNullOrEmpty(IsOrganization.Email))
                    {
                        UserException.LogInformation("OrganizationEmail is already present" + uIOrganization.Name);
                        this.validation.ErrorMessages.Add("OrganizationEmail is already present, Can you try some other E-Mail");
                    }
                }
            }

            if (this.validation.ErrorMessages.Count == 0)
                this.validation.IsSuccess = true;

            return this.validation;

        }
    }
}