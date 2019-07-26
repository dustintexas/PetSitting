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
    public class SittersController : Controller
    {
        private LoggingHandler _loggingHandler;

        public SittersController()
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

        // GET: Sitters/Select
        [PetSitting.MvcApplication.MustBeLoggedIn]
        public ActionResult SelectSitter()
        {
                var sitters = from e in ListAllSitters()
                              orderby e.SitterID
                              select e;
                return View(sitters);
        }
        [PetSitting.MvcApplication.MustBeLoggedIn]
        public ActionResult Choose(int id, decimal fee)
        {
            try
            {
                Session["ChosenSitterID"] = id;
                Session["ChosenSitterFee"] = fee;
                return RedirectToAction("../Sessions/CreateByOwner/" + Session["ChosenOwnerID"]);

            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        [PetSitting.MvcApplication.MustBeLoggedIn]
        public ActionResult Chosen(int id, decimal fee)
        {
            
            Session["ChosenSitterID"] = id;
            Session["ChosenSitterFee"] = fee;
            return RedirectToAction("../Sessions/CreateByOwner/" + Session["AUTHOwnerID"]);
        }
        // GET: Sitters
        public ActionResult Index()
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            return View();
        }

        // GET: Sitters/Details/5
        [PetSitting.MvcApplication.MustBeLoggedIn]
        public ActionResult Details(int id)
        {
            try
            {
                var sitter = SelectSitterById(id);
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

        // GET: Sitters/Create
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sitters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin")]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertSitter(collection["Name"],
                                int.Parse(collection["Age"]),
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

        // GET: Sitters/Edit/5
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter")]
        public ActionResult Edit(int id)
        {
            try
            {
                var sitter = SelectSitterById(id);
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

        // POST: Sitters/Edit/5
        [HttpPost]
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UpdateSitter(int.Parse(collection["SitterID"]),
                                collection["Name"],
                                int.Parse(collection["Age"]),
                                decimal.Parse(collection["Fee"]),
                                collection["Bio"],
                                collection["HiringDate"].Trim().Length == 0
                                ? (DateTime?)null
                                : DateTime.ParseExact(collection["HiringDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["GrossSalary"]), 
                                collection["Email"],
                                collection["Username"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Password"],
                                collection["Role"]);

                if ((string)Session["AUTHRole"] == "Admin")
                {
                    return RedirectToAction("ListAll");
                }
                else if ((string)Session["AUTHRole"] == "Sitter")
                {
                    return RedirectToAction("../Sitters/Details/" + collection["SitterID"]);
                }
                return View();
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Sitters/Delete/5
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                var sitter = SelectSitterById(id);
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
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DeleteSitter(id);

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
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin")]
        public ActionResult ListAll()
        {
            try
            {
                var sitters = from e in ListAllSitters()
                              orderby e.SitterID
                              select e;
                return View(sitters);
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

        private List<SittersEntity> ListAllSitters()
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    return sitters.SelectAllSitters();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private SittersEntity SelectSitterById(int id)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    return sitters.SelectSitterById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private void InsertSitter(string name, int age, decimal fee, string bio, DateTime? hiringDate, decimal grossSalary)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    var entity = new SittersEntity();
                    entity.Name = name;
                    entity.Age = age;
                    entity.Fee = fee;
                    entity.Bio = bio;
                    entity.HiringDate = hiringDate;
                    entity.GrossSalary = grossSalary;
                    var opSuccessful = sitters.InsertSitter(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateSitter(int id, string name, int age, decimal fee, string bio, DateTime? hiringDate, decimal grossSalary, string email, string username, string firstname, string lastname, string password, string role)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    var entity = new SittersEntity();
                    entity.SitterID = id;
                    entity.Name = name;
                    entity.Age = age;
                    entity.Fee = fee;
                    entity.Bio = bio;
                    entity.HiringDate = hiringDate;
                    entity.GrossSalary = grossSalary;
                    entity.Username = username;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Email = email;
                    entity.Password = password;
                    entity.Role = role;
                    var opSuccessful = sitters.UpdateSitter(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void DeleteSitter(int id)
        {
            try
            {
                using (var sitters = new SittersBusiness())
                {
                    var opSuccessful = sitters.DeleteSitterById(id);
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
