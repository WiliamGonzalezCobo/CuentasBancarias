namespace SistemaEncuestas.WebSite
{
    #region Namespaces

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    #endregion

    public class MvcApplication : HttpApplication
    {
        #region Protected Methods

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("_SessionUser", null);
        }

        #endregion
    }
}
