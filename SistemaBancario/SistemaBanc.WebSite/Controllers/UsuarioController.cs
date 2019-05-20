namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespace


    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using SistemaBanc.Entities;


    #endregion

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

                var respuesta = await ClienteUsuario.ValidarAccesoAsync(usuarioEntity);

                //EntityUsuario dataUser = new EntityUsuario
                //{
                //    usuario1 = respuesta.Usuario,
                //     = string.Concat(respuesta.Nombre, " ", respuesta.PrimerApellido, " ", respuesta.SegundoApellido),
                //    IdEmpresa = respuesta.Empresa.IdEmpresa,
                //    NombreEmpresa = respuesta.Empresa.Nombre,
                //    IdRol = respuesta.RolEmpresa.IdRol,
                //    NombreRol = respuesta.RolEmpresa.Descripcion
                //};

                Session["_SessionUser"] = respuesta;

                //return RedirectToAction("MisEncuestas", "Encuesta", new { idEmpresa = respuesta.Empresa.IdEmpresa });

                return View();
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