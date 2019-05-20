namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Utilidades.Correo;
    using Utilidades.Recursos;
    using Utilidades.Settings;

    #endregion

    public class ContactenosController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contacto(ContactoViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                CorreoEntity entity = new CorreoEntity
                {
                    SMTP = SettingsManager.SMTP,
                    Puerto = int.Parse(SettingsManager.Puerto),
                    Usuario = SettingsManager.UsuarioCorreo,
                    Password = SettingsManager.ContrasenaCorreo,
                    EsSSL = true,
                    EsHTML = true
                };

                var para = new List<string>()
                {
                    SettingsManager.CorreoContacto,
                };

                string mensaje = $"Mensaje de {modelo.Nombre} <br /> {modelo.Mensaje}";

                if (Correo.EnviarCorreo(entity, modelo.Email, para, null, modelo.Asunto, mensaje))
                {
                    ViewBag.MensajeSuccess = Mensajes.MensajeCorreoEnviado;
                    return View();
                }
                else
                {
                    ViewBag.MensajeError = Mensajes.MensajeCorreoError;
                    return View(modelo);
                }
            }

            return View(modelo);
        }

        #endregion
    }
}