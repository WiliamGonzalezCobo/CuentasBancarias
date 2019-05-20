namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using System.Web.Mvc;

    #endregion

    public class ErrorController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ErrorServer()
        {
            return View();
        }

        #endregion
    }
}