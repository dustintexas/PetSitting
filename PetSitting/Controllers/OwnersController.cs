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
    public class OwnersController : Controller
    {
        private LoggingHandler _loggingHandler;

        public OwnersController()
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

        // GET: Owners
        public ActionResult Index()
        {
            return View();
        }

        // GET: Owners/Choose
        public ActionResult Choose(int id)
        {
            try
            {
                Session["ChosenOwnerID"] = id;
                return RedirectToAction("../Sitters/ListAll");
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var owner = SelectOwnerById(id);
                return View(owner);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }
        
        // GET: Owners/Create
        public ActionResult Create()
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            return View();
        }

        // POST: Owners/Create
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
                InsertOwner(collection["OwnerName"],
                                collection["PetName"],
                                int.Parse(collection["PetAge"]),
                                collection["ContactPhone"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Username"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                collection["Email"],
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
        
        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            try
            {
                var owner = SelectOwnerById(id);
                return View(owner);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Owners/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UpdateOwner(int.Parse(collection["OwnerID"]),
                                collection["OwnerName"],
                                collection["PetName"],
                                int.Parse(collection["PetAge"]),
                                collection["ContactPhone"],
                                collection["FirstName"],
                                collection["LastName"],
                                collection["Username"],
                                collection["Password"],
                                int.Parse(collection["Age"]),
                                collection["Email"],
                                collection["Role"]);

                if ((string)Session["AUTHRole"] == "Admin")
                {
                    return RedirectToAction("ListAll");
                }
                else if ((string)Session["AUTHRole"] == "Owner")
                {
                    return RedirectToAction("../Owners/Details/" + collection["OwnerID"]);
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

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            try
            {
                var owner = SelectOwnerById(id);
                return View(owner);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Owners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            try
            {
                DeleteOwner(id);

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
            if ((string)Session["AUTHRole"] == null || (string)Session["AUTHRole"] != "Admin")
            {
                return RedirectToAction("../Home/Login");
            }
            try
            {
                var owners = from e in ListAllOwners()
                              orderby e.OwnerID
                              select e;
                return View(owners);
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
        // the following code connects with the SelectAll function in the DataAccess logic through the BusinessLogic
        private List<OwnersEntity> ListAllOwners()
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    return owners.SelectAllOwners();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }
        // the following code connects with the selectbyID function the the DataAccess logic through the BusinessLogic
        private OwnersEntity SelectOwnerById(int id)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    return owners.SelectOwnerById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }
        // the following code connects with the insert function in the DataAccess logic through the BusinessLogic
        private void InsertOwner(string ownername, string petname, int petage, string contactphone, string firstname, string lastname, string username, string password, int age, string email, string role)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    var entity = new OwnersEntity();
                    entity.OwnerName = ownername;
                    entity.PetName = petname;
                    entity.PetAge = petage;
                    entity.ContactPhone = contactphone;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Username = username;
                    entity.Password = password;
                    entity.Age = age;
                    entity.Email = email;
                    entity.Role = role;
                    var opSuccessful = owners.InsertOwner(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        // the following code connects with the update function in the DataAccess logic through the BusinessLogic
        private void UpdateOwner(int id, string ownername, string petname, int petage, string contactphone, string firstname, string lastname, string username, string password, int age, string email, string role)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    var entity = new OwnersEntity();
                    entity.OwnerID = id;
                    entity.OwnerName = ownername;
                    entity.PetName = petname;
                    entity.PetAge = petage;
                    entity.ContactPhone = contactphone;
                    entity.FirstName = firstname;
                    entity.LastName = lastname;
                    entity.Username = username;
                    entity.Password = password;
                    entity.Age = age;
                    entity.Email = email;
                    entity.Role = role;
                    var opSuccessful = owners.UpdateOwner(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }
        // the following code connects with the delete function in the DataAccess logic through the BusinessLogic
        private void DeleteOwner(int id)
        {
            try
            {
                using (var owners = new OwnersBusiness())
                {
                    var opSuccessful = owners.DeleteOwnerById(id);
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