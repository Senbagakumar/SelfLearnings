using SelfAssessment.Business;
using SelfAssessment.ExceptionHandler;
using SelfAssessment.Mapper;
using SelfAssessment.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace SelfAssessment
{
    public class Bootstrapper
    {
        //public static IUnityContainer Initialise()
        //{
        //    var container = BuildUnityContainer();
        //    DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        //    return container;
        //}
        //private static IUnityContainer BuildUnityContainer()
        //{
        //    //var container = new UnityContainer();

        //    //// register all your components with the container here   
        //    ////This is the important line to edit   
        //    //container.RegisterType<IBusinessContract, BusinessContract>();
        //    //container.RegisterType<IOrganizationMapper, OrganizationMapper>();
        //    //container.RegisterType<IUserBValidation, UserBValidation>();
        //    //container.RegisterInstance<ValidationInformation>(new ValidationInformation());


        //    //RegisterTypes(container);
        //    //return container;
        //}
        //public static void RegisterTypes(IUnityContainer container)
        //{

        //}
    }
}