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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int id)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
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
        public ActionResult CreateByOwner()
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            return View();
        }

        // POST: Session/CreateByOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                                ? (DateTime?)null : DateTime.ParseExact(collection["Date"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
                                decimal.Parse(collection["Fee"]));

                return RedirectToAction("../Owners/Details/" + Session["AUTHOwnerID"]);
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
        public ActionResult Create()
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
            return View();
        }

        // POST: Sessions/Create
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
                InsertSession(int.Parse(collection["SitterID"]),
                                int.Parse(collection["OwnerID"]),
                                collection["Status"],
                                collection["Date"].Trim().Length == 0
                                ? (DateTime?)null
                                : DateTime.ParseExact(collection["Date"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
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
        public ActionResult Edit(int id)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
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
                                : DateTime.ParseExact(collection["Date"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None),
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
        public ActionResult Delete(int id)
        {
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                DeleteSession(id);

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
            if ((string)Session["AUTHRole"] == null)
            {
                return RedirectToAction("../Home/Login");
            }
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
