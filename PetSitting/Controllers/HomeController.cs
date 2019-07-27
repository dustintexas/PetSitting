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

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("../Home/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "We love pets!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contact information:";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            // displays empty login screen with predefined returnURL
            LoginModel m = new LoginModel(); 
            m.message   = TempData["Message"]?.ToString()??"";
            m.ReturnURL = TempData["ReturnURL"]?.ToString()??@"~/Home";
            m.Username  = "";
            m.Password  = "";
 
            return View(m);
        }

        [HttpPost]
        public ActionResult Login(LoginModel info)
        {
            using (BusinessLogic.UsersBusiness ctx = new BusinessLogic.UsersBusiness())
            {
                UsersEntity user = ctx.FindUserByUsername(info.Username);
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
                    Session["AUTHRole"] = user.Role;
                    Session["AUTHUserID"] = user.UserID;
                    Session["ChosenOwnerID"] = 0;
                    if(user.Role == "Owner")
                    {
                        using (BusinessLogic.OwnersBusiness ctx2 = new BusinessLogic.OwnersBusiness())
                        {
                            OwnersEntity owner = ctx2.FindOwnerByUserId(user.UserID);
                            Session["AUTHOwnerID"] = owner.OwnerID;
                            return Redirect("~/Owners/Details/" + owner.OwnerID);
                        }
                    }
                    else if(user.Role == "Sitter")
                    {
                        using (BusinessLogic.SittersBusiness ctx2 = new BusinessLogic.SittersBusiness())
                        {
                            SittersEntity sitter = ctx2.FindSitterByUserId(user.UserID);
                            Session["AUTHSitterID"] = sitter.SitterID;
                            return Redirect("~/Sitters/Details/" + sitter.SitterID);
                        }
                    }
                    else if(user.Role == "Admin")
                    {
                        return Redirect("~/Users/ListAll");
                    }
                    
                }
                info.message = "The password was incorrect";  
                return View(info);           
            }
        }
    }
}