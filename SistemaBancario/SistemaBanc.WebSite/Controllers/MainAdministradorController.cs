namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using System.Web.Mvc;

    #endregion

    public class MainAdministradorController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        #endregion
    }
}