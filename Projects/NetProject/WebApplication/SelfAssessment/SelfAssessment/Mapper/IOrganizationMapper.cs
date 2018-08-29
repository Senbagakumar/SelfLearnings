using SelfAssessment.Models;
using SelfAssessment.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfAssessment.Mapper
{
    public interface IOrganizationMapper
    {
        Organization Registration(UIOrganization uiOrganization);
    }
}