using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MyNPO.Controllers
{
    public class BaseController : Controller
    {
        private int userId;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[Constants.UserId] == null || string.IsNullOrEmpty(Session[Constants.UserId].ToString()))
                Response.Redirect(Constants.RedirectToLogin);
            userId = Convert.ToInt16(Session[Constants.UserId].ToString());
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon(); // This will clear the session at the end of request
            return Redirect("/Login/Index");
        }
        public int UserId => userId;
    }
}