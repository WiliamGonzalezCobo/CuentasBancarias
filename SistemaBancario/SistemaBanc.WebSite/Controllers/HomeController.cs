namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespces

    using System.Web.Mvc;

    #endregion

    public class HomeController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}