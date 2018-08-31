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
        bool LoginVerfication(string userName, string password);
        string GenerateTempPassword(int length, int numberOfNonAlphanumericCharacters);
        string ForGetPassword(string email);
        string SaveState(string stateName);
        string UpdateState(int stateId, string stateName);
        string DeleteState(int stateId);
    }
}
