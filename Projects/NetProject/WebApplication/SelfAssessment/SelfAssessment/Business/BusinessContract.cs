using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.Models;
using SelfAssessment.Mapper;
using SelfAssessment.Validation;
using SelfAssessment.ExceptionHandler;

namespace SelfAssessment.Business
{
    public class BusinessContract : IBusinessContract
    {
        private readonly IOrganizationMapper organizationMapper;
        private readonly IUserBValidation userBValidation;
        private readonly string host;
        private readonly string networkUserName;
        private readonly string networkUserPassword;
        private readonly string fromId;
        private readonly string toId;

        public BusinessContract(IOrganizationMapper organizationMapper, IUserBValidation userBValidation)
        {
            this.organizationMapper = organizationMapper;
            this.userBValidation = userBValidation;
            host = "";
            networkUserName = "";
            networkUserPassword = "";
            fromId = "";
            toId = "";
        }

        public void SendRegistrationMail(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public ValidationInformation UserCreation(UIOrganization uIOrganization)
        {
            ValidationInformation validation= this.userBValidation.RegistrationValidation(uIOrganization);
            if(validation.IsSuccess)
                this.organizationMapper.Registration(uIOrganization);

            return validation;             
        }
    }
}