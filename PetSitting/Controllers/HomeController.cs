using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSitting.Model;

namespace PetSitting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            // displays empty login screen with predefined returnURL
            LoginModel m = new LoginModel(); 
            m.message   = TempData["Message"]?.ToString()??"";
            m.ReturnURL = TempData["ReturnURL"]?.ToString()??@"~/Home";
            m.Username  = "genericuser";
            m.Password  = "genericpassword";
 
            return View(m);
        }

        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            using (BusinessLogic.UsersBusiness ctx = new BusinessLogic.UsersBusiness())
            {
                BusinessLogic.UserBLL user = ctx.FindUserByUsername(info.Username);
                if (user == null) 
                { 
                    info.message = $"The Username '{info.Username}' does not exist in the database";
                    return View(info);
                }
                string actual = user.Password;
                //string potential = user.Salt + info.Password;
                    string potential = info.Password;
                //bool validateduser = System.Web.Helpers.Crypto.VerifyHashedPassword(actual,potential);
                    bool validateduser = potential == actual;
                if (validateduser)
                {
                    Session["AUTHUsername"] = user.Username;
                    Session["AUTHRoles"] = user.Roles;
                    return Redirect(info.ReturnURL);
                }
                info.message = "The password was incorrect";  
                return View(info);           
            }
        }
    }
}