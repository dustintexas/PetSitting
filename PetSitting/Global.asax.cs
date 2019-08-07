
namespace PetSitting
{
    using System;
    using System.Security.Principal;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string Username = (string)Session["AUTHUsername"];
            string Sessroles = (string)Session["AUTHRole"];
            if (string.IsNullOrEmpty(Username))
            {
                return;
            }
            GenericIdentity i = new GenericIdentity(Username, "MyCustomType");
            if (Sessroles == null) { Sessroles = ""; }
            string[] roles = Sessroles.Split(',');
            GenericPrincipal p = new GenericPrincipal(i, roles);
            HttpContext.Current.User = p;
        }

        public class MustBeLoggedInAttribute : AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    //Call base class to allow user into the action method
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    string ReturnURL = filterContext.RequestContext.HttpContext.Request.Path.ToString();
                    filterContext.Controller.TempData.Add("Message",
                            $"you must be logged into any account to access this resource, you are not currently logged in at all");
                    filterContext.Controller.TempData.Add("ReturnURL", ReturnURL);
                    System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                    dict.Add("Controller", "Home");
                    dict.Add("Action", "Login");
                    filterContext.Result = new RedirectToRouteResult(dict);
                }
            }
        }

        public class MustBeInRoleAttribute : AuthorizeAttribute

        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                if (this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
                {
                    base.OnAuthorization(filterContext);

                }
                else
                {
                    string ReturnURL = filterContext.RequestContext.HttpContext.Request.Path.ToString();
                    filterContext.Controller.TempData.Add("Message",
                                        $"you must be in at least one of the following roles to access this resource:  {Roles}");
                    filterContext.Controller.TempData.Add("ReturnURL", ReturnURL);
                    System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                    dict.Add("Controller", "Home");
                    dict.Add("Action", "Login");
                    filterContext.Result = new RedirectToRouteResult(dict);

                }

            }
        }
    
    }
}
