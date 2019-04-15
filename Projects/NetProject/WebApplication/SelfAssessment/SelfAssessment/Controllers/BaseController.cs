using SelfAssessment.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SelfAssessment.Controllers
{
    public class BaseController : Controller
    {
        private int userId;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (Session[Utilities.UserId] == null || string.IsNullOrWhiteSpace(Session[Utilities.UserId].ToString()))
                Response.Redirect(Utilities.RedirectToLogin);
            userId = Convert.ToInt16(Session[Utilities.UserId].ToString());
        }

        public int UserId => userId;

    }

    public class AdminBaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["Admin"] == null || string.IsNullOrEmpty(Session["Admin"].ToString()))
                Response.Redirect(Utilities.RedirectToLogin);            
        }

    }
}