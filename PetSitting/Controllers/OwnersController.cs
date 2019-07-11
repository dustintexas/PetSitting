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
                                collection["ContactPhone"]);

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
                                collection["ContactPhone"]);

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

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
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

        // POST: Owners/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
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

        private void InsertOwner(string ownername, string petname, int petage, string contactphone)
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
                    var opSuccessful = owners.InsertOwner(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateOwner(int id, string ownername, string petname, int petage, string contactphone)
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
                    var opSuccessful = owners.UpdateOwner(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

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