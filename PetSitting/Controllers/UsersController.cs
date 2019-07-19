using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSitting.BusinessLogic;
using PetSitting.Common;
using PetSitting.Model;

namespace PetSitting.Controllers
{
    public class UsersController : Controller
    {
        private LoggingHandler _loggingHandler;

        public UsersController()
        {
            _loggingHandler = new LoggingHandler();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_loggingHandler != null)
                {
                    _loggingHandler.Dispose();
                    _loggingHandler = null;
                }
            }

            base.Dispose(disposing);
        }

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if ((string)Session["AUTHRole"] != null)
                {
                    var user = SelectUserById(id);
                    return View(user);
                } else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertUserOwner(collection["Username"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Email"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                bool.Parse("True"),
                                "Owner",
                                collection["OwnerName"],
                                collection["PetName"],
                                int.Parse(collection["PetAge"]),
                                collection["ContactPhone"]);

                return RedirectToAction("../Home/Index");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        
        // GET: Users/Create
        public ActionResult Create()
        {
            if ((string)Session["AUTHRole"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Index");
            }
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertUser(collection["Username"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Email"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                bool.Parse(collection["IsActive"]),
                                collection["Role"], 
                                decimal.Parse(collection["Fee"]),
                                collection["Bio"],
                                collection["HiringDate"].Trim().Length == 0
                                ? (DateTime?)null
                                : DateTime.ParseExact(collection["HiringDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["GrossSalary"]));

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                if ((string)Session["AUTHRole"] != null)
                {
                    var sitter = SelectUserById(id);
                    return View(sitter);
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
                
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UpdateUser(int.Parse(collection["UserID"]),
                                collection["Username"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Email"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                bool.Parse(collection["IsActive"]),
                                collection["Role"]);

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                if ((string)Session["AUTHRole"] != null)
                {
                    var sitter = SelectUserById(id);
                    return View(sitter);
                } else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Sitters/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DeleteUser(id);

                return RedirectToAction("ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        public ActionResult ListAll()
        {
            try
            {
                if ((string)Session["AUTHRole"] != null)
                {
                    var users = from e in ListAllUsers()
                                orderby e.UserID
                                select e;
                    return View(users);
                }
                else
                {
                    return RedirectToAction("../Home/Index");
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        #region Private Methods
        // Builds a list of all users
        private List<UsersEntity> ListAllUsers()
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    return users.SelectAllUsers();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }
        private UsersEntity SelectUserById(int id)
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    return users.SelectUserById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }
        // Creates record in Users table AND Sitters table
        private void InsertUser(string username, string firstname, string lastname, string email, string password, int age, bool isactive, string role, decimal fee, string bio, DateTime? hiringdate, decimal grosssalary)
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    var entity = new UsersEntity();
                    entity.Username = username;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Email = email;
                    entity.Password = password;
                    entity.Age = age;
                    entity.IsActive = isactive;
                    entity.Role = role;
                    entity.Name = firstname + " " + lastname;
                    entity.Fee = fee;
                    entity.Bio = bio;
                    entity.HiringDate = hiringdate;
                    entity.GrossSalary = grosssalary;
                    var opSuccessful = users.InsertUser(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        // Creates record in Users table AND Owners table
        private void InsertUserOwner(string username, string firstname, string lastname, string email, string password, int age, bool isactive, string role, string ownername, string petname, int petage, string contactphone)
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    var entity = new UsersEntity();
                    entity.Username = username;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Email = email;
                    entity.Password = password;
                    entity.Age = age;
                    entity.IsActive = isactive;
                    entity.Role = role;
                    entity.OwnerName = ownername;
                    entity.PetName = petname;
                    entity.PetAge = petage;
                    entity.ContactPhone = contactphone;
                    var opSuccessful = users.InsertUser(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        private void UpdateUser(int id, string username, string firstname, string lastname, string email, string password, int age, bool isactive, string role)
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    var entity = new UsersEntity();
                    entity.UserID = id;
                    entity.Username = username;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Email = email;
                    entity.Password = password;
                    entity.Age = age;
                    entity.IsActive = isactive;
                    entity.Role = role;
                    var opSuccessful = users.UpdateUser(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        private void DeleteUser(int id)
        {
            try
            {
                using (var users = new UsersBusiness())
                {
                    var opSuccessful = users.DeleteUserById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        #endregion
    }
}