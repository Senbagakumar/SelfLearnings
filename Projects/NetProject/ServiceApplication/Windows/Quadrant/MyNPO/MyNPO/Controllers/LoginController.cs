using MyNPO.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNPO.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            var loginUser = ConfigurationManager.AppSettings["LoginUser"].ToString();
            var loginPassword = ConfigurationManager.AppSettings["LoginPassword"].ToString();

            if (login.UserName == loginUser && login.Password == MyNPO.Utilities.Helper.Decrypt(loginPassword))
            {
                Session[Constants.UserId] = "99";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}