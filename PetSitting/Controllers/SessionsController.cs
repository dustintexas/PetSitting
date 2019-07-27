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
    public class SessionsController : Controller
    {
        private LoggingHandler _loggingHandler;
        

        public SessionsController()
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

        // GET: Sessions
        [PetSitting.MvcApplication.MustBeLoggedIn]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sessions/Details/5
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter,Owner")]
        public ActionResult Details(int id)
        {
            try
            {
                var session = SelectSessionById(id);
                return View(session);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // GET: Sessions/CreateByOwner
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Owner")]
        public ActionResult CreateByOwner()
        {
            return View();
        }

        // POST: Session/CreateByOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Owner")]
        public ActionResult CreateByOwner(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                InsertSession(int.Parse(collection["SitterID"]),
                                int.Parse(collection["OwnerID"]),
                                collection["Status"],
                                collection["Date"].Trim().Length == 0
                                ? (DateTime?)null : DateTime.ParseExact(collection["Date"], "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["Fee"]));

                Session["ChosenOwnerID"] = 0;
                if ((string)Session["AUTHRole"] == "Owner")
                {
                    return RedirectToAction("../Owners/Details/" + Session["AUTHOwnerID"]);
                } else
                {
                    return RedirectToAction("../Sessions/ListAll");
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

        // GET: Sessions/Create
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Owner,Sitter")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Owner,Sitter")]
        public ActionResult Create(FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                InsertSession(int.Parse(collection["SitterID"]),
                                int.Parse(collection["OwnerID"]),
                                collection["Status"],
                                collection["Date"].Trim().Length == 0
                                ? (DateTime?)null
                                : DateTime.ParseExact(collection["Date"], "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["Fee"]));

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

        // GET: Sessions/Edit/5
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter,Owner")]
        public ActionResult Edit(int id)
        {
            try
            {
                
                var session = SelectSessionById(id);
                var list = new List<SelectListItem>
                {
                        new SelectListItem{ Text="Requested", Value = "Requested" },
                        new SelectListItem{ Text="Scheduled", Value = "Scheduled" },
                        new SelectListItem{ Text="Completed", Value = "Completed" },
                        new SelectListItem{ Text="Cancelled", Value = "Cancelled" }
                };
                    ViewData["StatusList"] = list;

                return View(session);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter,Owner")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                UpdateSession(int.Parse(collection["SessionID"]),
                                int.Parse(collection["SitterID"]),
                                int.Parse(collection["OwnerID"]),
                                collection["Status"],
                                collection["Date"].Trim().Length == 0
                                ? (DateTime?)null
                                : DateTime.ParseExact(collection["Date"], "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["Fee"]));
                if((string)Session["AUTHRole"] == "Sitter")
                {
                    return RedirectToAction("../Sitters/Details/"+Session["AUTHSitterID"].ToString());
                } 
                else
                {
                    return RedirectToAction("ListAll");
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

        // GET: Sessions/Delete/5
        [PetSitting.MvcApplication.MustBeInRole(Roles="Admin,Sitter,Owner")]
        public ActionResult Delete(int id)
        {
            try
            {
                var session = SelectSessionById(id);
                return View(session);
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
                ViewBag.Message = Server.HtmlEncode(ex.Message);
                return View("Error");
            }
        }


        // POST: Sessions/Delete/5
        [HttpPost]
        [PetSitting.MvcApplication.MustBeInRole(Roles = "Admin,Sitter,Owner")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DeleteSession(id);

                if ((string)Session["AUTHRole"] == "Admin")
                {
                    return RedirectToAction("ListAll");
                } else if ((string)Session["AUTHRole"] == "Owner")
                {
                    return RedirectToAction("../Owners/Details/" + (int)Session["AUTHOwnerID"]);
                } else if ((string)Session["AUTHRole"] == "Sitter")
                {
                    return RedirectToAction("../Sitters/Details/" + (int)Session["AUTHSitterID"]);
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
        [PetSitting.MvcApplication.MustBeInRole(Roles = "Admin")]
        public ActionResult ListAll()
        {
            try
            {
                var sessions = from e in ListAllSessions()
                                orderby e.SessionID
                                select e;
                return View(sessions);
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

        private List<SessionsEntity> ListAllSessions()
        {
            try
            {
                using (var sessions = new SessionsBusiness())
                {
                    return sessions.SelectAllSessions();
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private SessionsEntity SelectSessionById(int id)
        {
            try
            {
                using (var sessions = new SessionsBusiness())
                {
                    return sessions.SelectSessionById(id);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
            return null;
        }

        private void InsertSession(int sitterid, int ownerid, string status, DateTime? date, decimal fee)
        {
            try
            {
                using (var sessions = new SessionsBusiness())
                {
                    var entity = new SessionsEntity();
                    entity.SitterID = sitterid;
                    entity.OwnerID = ownerid;
                    entity.Status = status;
                    entity.Date = date;
                    entity.Fee = fee;
                    var opSuccessful = sessions.InsertSession(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void UpdateSession(int id, int sitterid, int ownerid, string status, DateTime? date, decimal fee)
        {
            try
            {
                using (var sessions = new SessionsBusiness())
                {
                    var entity = new SessionsEntity();
                    entity.SessionID = id;
                    entity.SitterID = sitterid;
                    entity.OwnerID = ownerid;
                    entity.Status = status;
                    entity.Fee = fee;
                    entity.Date = date;
                    var opSuccessful = sessions.UpdateSession(entity);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);
            }
        }

        private void DeleteSession(int id)
        {
            try
            {
                using (var sessions = new SessionsBusiness())
                {
                    var opSuccessful = sessions.DeleteSessionById(id);
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
