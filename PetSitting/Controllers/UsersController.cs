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
                var user = SelectUserById(id);
                return View(user);
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
                InsertUser(collection["Username"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Email"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                bool.Parse("True"));

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
            return View();
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
                                bool.Parse(collection["IsActive"]));

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
                var sitter = SelectUserById(id);
                return View(sitter);
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
                                bool.Parse(collection["IsActive"]));

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
                var sitter = SelectUserById(id);
                return View(sitter);
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
                var users = from e in ListAllUsers()
                              orderby e.UserID
                              select e;
                return View(users);
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
        private void InsertUser(string username, string firstname, string lastname, string email, string password, int age, bool isactive)
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
                    var opSuccessful = users.InsertUser(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        private void UpdateUser(int id, string username, string firstname, string lastname, string email, string password, int age, bool isactive)
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