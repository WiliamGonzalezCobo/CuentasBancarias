namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using SistemaBanc.Entities;
    using SistemaBanc.Utils;
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    public class AdministradorController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AutenticationModel autentication)
        {
            try
            {
                EntityUsuario administradorEntity = new EntityUsuario()
                {
                    usuario1 = autentication.Usuario,
                    contrasenia = autentication.Password
                };

                var respuesta = await ClienteUsuario.ValidarAccesoAsync(administradorEntity);

                return RedirectToAction("Dashboard", "MainAdministrador");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(autentication);
            }
        }

        #endregion
    }
}