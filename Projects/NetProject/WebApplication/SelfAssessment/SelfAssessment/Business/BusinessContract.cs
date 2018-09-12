using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelfAssessment.Models;
using SelfAssessment.Mapper;
using SelfAssessment.Validation;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Models.DBModel;
using SelfAssessment.DataAccess;
using System.Web.Security;

namespace SelfAssessment.Business
{
    public class BusinessContract : IBusinessContract
    {
        private readonly IOrganizationMapper organizationMapper;
        private readonly IUserBValidation userBValidation;
        private readonly RegistrationSendMail registrationSendMail;
        private readonly string host;
        private readonly string networkUserName;
        private readonly string networkUserPassword;
        private readonly string fromId;
        private readonly string toId;

        public BusinessContract(IOrganizationMapper organizationMapper, IUserBValidation userBValidation, RegistrationSendMail registrationSendMail)
        {
            this.organizationMapper = organizationMapper;
            this.userBValidation = userBValidation;
            this.registrationSendMail = registrationSendMail;

            host = "";
            networkUserName = "";
            networkUserPassword = "";
            fromId = "";
            toId = "";
        }

        public string DeleteState(int stateId)
        {
            throw new NotImplementedException();
        }

        public string ForGetPassword(string email)
        {
            string response = "";
            using (Repository<Organization> repository = new Repository<Organization>())
            {
                var organization = repository.Filter(q => q.Email == email).FirstOrDefault();
                if (organization != null && !string.IsNullOrEmpty(organization.Email))
                {
                    organization.TempPassword = GenerateTempPassword(8, 2);
                    repository.Update(organization);
                    repository.SaveChanges();

                    response = "Send Temp password to your mail id";
                    //this.registrationSendMail.Send();
                }
                else
                    response = "Sorry! there is no record";

            }
            return response;
        }

        public string GenerateTempPassword(int length, int numberOfNonAlphanumericCharacters)
        {
            return Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
        }

        public int LoginVerfication(string userName, string password)
        {
            using (Repository<Organization> repository = new Repository<Organization>())
            {
               var user=repository.Filter(q => q.Email == userName && (q.TempPassword == password || q.Password == password)).FirstOrDefault();
                if (user != null && !string.IsNullOrEmpty(user.UserId))
                    return user.Id;
                else
                    return 0;
            }
        }

        public string SaveState(string stateName)
        {
            throw new NotImplementedException();
        }

        public string UpdateState(int stateId, string stateName)
        {
            throw new NotImplementedException();
        }

        public ValidationInformation UserCreation(UIOrganization uIOrganization)
        {
            ValidationInformation validation= this.userBValidation.RegistrationValidation(uIOrganization);
            if (validation.IsSuccess)
            {
                string password = GenerateTempPassword(8, 2);
                string[] sector = new string[2];
                string[] subSector = new string[2];
                Others other = new Others();

                bool isOthers = false;

                if (uIOrganization.Sector.Contains("1000"))
                {
                    sector = uIOrganization.Sector.Split('-');
                    uIOrganization.Sector = sector[0];
                    other.Sector = sector[1];
                    isOthers = true;
                }
                else
                    other.Sector = string.Empty;

                if (uIOrganization.SubSector.Contains("1000"))
                {
                    subSector = uIOrganization.SubSector.Split('-');
                    uIOrganization.SubSector = subSector[0];
                    other.SubSector = subSector[1];
                    isOthers = true;
                }
                else
                    other.SubSector = string.Empty;
                if(isOthers)
                {
                    other.OrganizationId = uIOrganization.Name.Substring(0, 6);
                    using (Repository<Others> repository = new Repository<Others>())
                    {
                        repository.Create(other);
                        repository.SaveChanges();
                    }
                }

                using (Repository<Organization> repository = new Repository<Organization>())
                {
                    Organization organization = this.organizationMapper.Registration(uIOrganization);
                    organization.TempPassword = password;
                    repository.Create(organization);
                    repository.SaveChanges();
                }             
                //this.registrationSendMail.Send(new MailConfiguration());
            }

            return validation;             
        }
    }
}