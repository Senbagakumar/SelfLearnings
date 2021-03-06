﻿using System;
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


        public BusinessContract()//IOrganizationMapper organizationMapper, IUserBValidation userBValidation, RegistrationSendMail registrationSendMail
        {
            this.organizationMapper = new OrganizationMapper();
            this.userBValidation = new UserBValidation(new ValidationInformation());
            this.registrationSendMail = new RegistrationSendMail();
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
                var user = repository.Filter(q => q.Email == userName && q.IsActive).FirstOrDefault();
                if (user != null)
                {
                    var tpwd = !string.IsNullOrEmpty(user.Password) ? StringCipher.Decrypt(user.Password) : string.Empty;
                    if (user != null && !string.IsNullOrEmpty(user.UserId) && (tpwd == password || user.TempPassword == password))
                    {
                        return user.Id;
                    }
                    else
                        return 0;
                }
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
                if (uIOrganization.Sector.Contains(Utilities.OthersValue))
                {
                    sector = uIOrganization.Sector.Split(Utilities.SplitValue);
                    uIOrganization.Sector = sector[0];
                    other.Sector = sector[1];
                    isOthers = true;
                }
                else
                    other.Sector = string.Empty;

                if (uIOrganization.SubSector.Contains(Utilities.OthersValue))
                {
                    subSector = uIOrganization.SubSector.Split(Utilities.SplitValue);
                    uIOrganization.SubSector = subSector[0];
                    other.SubSector = subSector[1];
                    isOthers = true;
                }
                else
                    other.SubSector = string.Empty;


                using (Repository<Organization> repository = new Repository<Organization>())
                {
                    Organization organization = this.organizationMapper.Registration(uIOrganization);
                    organization.Cities = repository.AssessmentContext.cities.FirstOrDefault(q => q.Id == organization.CityId);
                    organization.States = repository.AssessmentContext.states.FirstOrDefault(q => q.Id == organization.StateId);
                    organization.Revenues = repository.AssessmentContext.revenues.FirstOrDefault(q => q.Id == organization.RevenueId);
                    organization.TypesOfService = repository.AssessmentContext.serviceTypes.FirstOrDefault(q => q.Id == organization.Id);
                    
                    //organization.Sectors = repository.AssessmentContext.sectors.FirstOrDefault(q => q.Id == organization.SectorId);
                    //organization.SubSectors = repository.AssessmentContext.subSectors.FirstOrDefault(q => q.Id == organization.SubSectorId);
                    organization.SectorId = isOthers ? Convert.ToInt16(Utilities.SectorValue) : repository.AssessmentContext.sectors.FirstOrDefault(q => q.Id == organization.SectorId).Id;
                    organization.SubSectorId = isOthers ? Convert.ToInt16(Utilities.SectorValue) : repository.AssessmentContext.subSectors.FirstOrDefault(q => q.Id == organization.SubSectorId).Id;

                    int assessmentid = 0;
                    var listAssesment = new List<Assessment>();
                    int sectorValue = Convert.ToInt16(Utilities.SectorValue);

                    listAssesment = repository.AssessmentContext.assessments.Where(q => q.Sector == organization.SectorId).ToList();
                    
                    if(listAssesment == null || listAssesment.Count == 0)
                        listAssesment = repository.AssessmentContext.assessments.Where(q => q.Sector == sectorValue).ToList();

                    if (listAssesment != null && listAssesment.Count > 0)
                    {
                        var subAssessment = listAssesment.FirstOrDefault(q => q.SubSector == organization.SubSectorId);
                        if (subAssessment != null && subAssessment.Id > 0)
                            assessmentid = subAssessment.Id;
                        else
                        {
                            assessmentid = listAssesment[0].Id;
                        }
                    }                   
                    organization.AssessmentId = assessmentid;

                    organization.TempPassword = password;
                    repository.Create(organization);
                    repository.SaveChanges();
                }

                if (isOthers)
                {
                    other.OrganizationId = uIOrganization.Name.Substring(0, 6);
                    using (Repository<Others> repository = new Repository<Others>())
                    {
                        //other.Organizations = repository.AssessmentContext.UserInfo.FirstOrDefault(q => q.Name == other.OrganizationId);
                        repository.Create(other);
                        repository.SaveChanges();
                    }
                }

                validation.IsSuccess = true;

                Repository<Template> template = new Repository<Template>();
                var registrationTemplate=template.Filter(q => q.Name.StartsWith(Utilities.RegistrationTemplateName)).FirstOrDefault();
                if (registrationTemplate != null && !string.IsNullOrWhiteSpace(registrationTemplate.Description))
                    RegistrationSendMail.SendMail(registrationTemplate.Description, Utilities.RegistrationSubject, uIOrganization.Email,uIOrganization.Name);
                //this.registrationSendMail.Send(new MailConfiguration());
            }
            else
            {
                validation.IsSuccess = false;
            }
            return validation;
        }
    }
}