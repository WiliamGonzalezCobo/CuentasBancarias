using SistemaBanc.Entities;
using SistemaBanc.WebCliente.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaBanc.WebCliente.Controllers
{
    public class UsuarioController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            Session.Abandon();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AutenticationModel autentication)
        {
            try
            {
                EntityUsuario usuarioEntity = new EntityUsuario
                {
                    usuario1 = autentication.Usuario,
                    contrasenia = autentication.Password
                };

                var usuario = await ClienteUsuario.ValidarAccesoAsync(usuarioEntity);

                if (usuario != null) {
                    System.Web.HttpContext.Current.Session["sessionString"] = usuario;
                }

                return RedirectToAction("Index", "Cuenta", new { idCliente = usuario.id_cliente });
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(autentication);
            }
        }

        [HttpGet]
        public ActionResult Salir()
        {
            Session.Clear();
            Session.Abandon();
            System.Web.HttpContext.Current.Session["sessionString"] = null;
            return RedirectToAction("Login", "Usuario");
        }

        #endregion
    }
}