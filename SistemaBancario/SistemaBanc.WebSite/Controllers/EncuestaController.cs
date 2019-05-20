namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using Entidades.Comun;
    using Entidades.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Utilidades.Correo;
    using Utilidades.Settings;

    #endregion

    public class EncuestaController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public async Task<ActionResult> MisEncuestas(string idEmpresa)
        {
            try
            {
                if (Session["_SessionUser"] == null)
                {
                    return RedirectToAction("Login", "Usuario");
                }

                List<EncuestaViewModel> modelo = new List<EncuestaViewModel>();
                List<Encuesta> listEncuestas = await ClienteEncuesta.GetEncuestasByIdEmpresaAsync(idEmpresa);

                foreach (var item in listEncuestas)
                {
                    var encuestaModel = new EncuestaViewModel
                    {
                        Id = item.IdEncuesta,
                        NombreEncuesta = item.Nombre,
                        DescripcionEncuesta = item.Descripcion,
                        EsInterno = item.Interno,
                        IdEmpresa = item.IdEmpresa,
                        Url = item.Url
                    };

                    modelo.Add(encuestaModel);
                }

                LoadUserData();

                return View(modelo);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("ErrorServer", "Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Crear(string idEmpresa)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var encuestaModel = new EncuestaViewModel
            {
                Id = new Guid().ToString(),
                IdEmpresa = idEmpresa
            };

            LoadUserData();
            await LoadMetadata();

            return View(encuestaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(EncuestaViewModel encuestaModel)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            if (ModelState.IsValid)
            {
                encuestaModel = RemoverEliminados(encuestaModel);

                var encuestaEntity = new Encuesta
                {
                    IdEncuesta = Guid.NewGuid().ToString(),
                    Nombre = encuestaModel.NombreEncuesta,
                    Descripcion = encuestaModel.DescripcionEncuesta,
                    Interno = encuestaModel.EsInterno,
                    IdEmpresa = encuestaModel.IdEmpresa,
                    Activo = true
                };

                encuestaEntity.Url = $"http://{Request.Url.Host}:60999/Encuesta/Responder?idEncuesta={encuestaEntity.IdEncuesta}&idEmpresa={encuestaEntity.IdEmpresa}";

                encuestaEntity.Preguntas = new List<Pregunta>();
                encuestaModel.Preguntas.ForEach(p =>
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = p.Id,
                        IdEncuesta = encuestaEntity.IdEncuesta,
                        IdMetadata = p.IdMetadata,
                        Descripcion = p.Descripcion,
                        Requerido = p.Requerido
                    };

                    pregunta.ItemsPreguntas = new List<ItemPregunta>();
                    p.ItemsPregunta.ForEach(ip =>
                    {
                        pregunta.ItemsPreguntas.Add(new ItemPregunta
                        {
                            IdItemPregunta = ip.Id,
                            IdPregunta = pregunta.IdPregunta,
                            Valor = ip.Valor
                        });
                    });

                    encuestaEntity.Preguntas.Add(pregunta);
                });

                await ClienteEncuesta.AddEncuestaAsync(encuestaEntity);

                LoadUserData();
                await LoadMetadata();

                return RedirectToAction("MisEncuestas", "Encuesta", new { idEmpresa = encuestaModel.IdEmpresa });
            }

            return View(encuestaModel);
        }

        [HttpGet]
        public ActionResult VistaPrevia(string idEncuesta, string idEmpresa)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            LoadUserData();

            ViewBag.IdEncuesta = idEncuesta;
            ViewBag.IdEmpresa = idEmpresa;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> VistaPreviaJSON(string idEncuesta, string idEmpresa)
        {
            Encuesta encuesta = await ClienteEncuesta.GetEncuestaByIdEmpresaAndIdEncuestaAsync(idEmpresa, idEncuesta);

            LoadUserData();

            return Json(encuesta);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string idEncuesta)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            Encuesta encuesta = await ClienteEncuesta.GetEncuestaByIdAsync(idEncuesta);
            EncuestaViewModel modelEncuesta = new EncuestaViewModel
            {
                Id = encuesta.IdEncuesta,
                NombreEncuesta = encuesta.Nombre,
                DescripcionEncuesta = encuesta.Descripcion,
                IdEmpresa = encuesta.IdEmpresa
            };

            LoadUserData();

            return View(modelEncuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EncuestaViewModel encuestaModel)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            ViewBag.MensajeEliminacion = await ClienteEncuesta.DeleteEncuestaByIdAsync(encuestaModel.Id);
            return RedirectToAction("MisEncuestas", new { idEmpresa = encuestaModel.IdEmpresa });
        }

        [HttpGet]
        public ActionResult Responder(string idEncuesta, string idEmpresa)
        {
            ViewBag.IdEncuesta = idEncuesta;
            ViewBag.IdEmpresa = idEmpresa;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ResponderJSON(string idEncuesta, string idEmpresa)
        {
            Encuesta encuesta = await ClienteEncuesta.GetEncuestaByIdEmpresaAndIdEncuestaAsync(idEmpresa, idEncuesta);
            return Json(encuesta);
        }

        [HttpPost]
        public async Task<string> Responder(Encuesta jsonOriginal)
        {
            var referencia = DateTime.Now.Ticks.ToString();

            foreach (var pregunta in jsonOriginal.Preguntas)
            {
                foreach (var respuesta in pregunta.Respuestas)
                {
                    respuesta.IdRespuesta = Guid.NewGuid().ToString();
                    respuesta.Referencia = referencia;
                    await ClienteRespuesta.AddRespuestaAsync(respuesta);
                }
            }

            return "Correcto.";
        }

        [HttpGet]
        public ActionResult EncuestaFinalizada()
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            LoadUserData();

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Editar(string idEncuesta, string idEmpresa)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            Encuesta encuesta = await ClienteEncuesta.GetEncuestaByIdEmpresaAndIdEncuestaAsync(idEmpresa, idEncuesta);
            int index = 0;

            var encuestaModel = new EncuestaViewModel
            {
                Id = encuesta.IdEncuesta,
                NombreEncuesta = encuesta.Nombre,
                DescripcionEncuesta = encuesta.Descripcion,
                EsInterno = encuesta.Interno,
                IdEmpresa = encuesta.IdEmpresa,
                Activo = encuesta.Activo
            };

            List<PreguntaViewModel> preguntasModel = new List<PreguntaViewModel>();

            foreach (var pregunta in encuesta.Preguntas)
            {
                PreguntaViewModel preguntaModel = new PreguntaViewModel
                {
                    Id = pregunta.IdPregunta,
                    Descripcion = pregunta.Descripcion,
                    Requerido = pregunta.Requerido,
                    IdMetadata = pregunta.IdMetadata,
                    Eliminado = false,
                    Index = index
                };

                foreach (var itemPregunta in pregunta.ItemsPreguntas)
                {
                    if (itemPregunta != null)
                    {
                        ItemPreguntaViewModel itemPreguntaModel = new ItemPreguntaViewModel
                        {
                            Id = itemPregunta.IdItemPregunta,
                            Valor = itemPregunta.Valor,
                            Eliminado = false
                        };

                        preguntaModel.ItemsPregunta.Add(itemPreguntaModel);
                    }
                }

                preguntasModel.Add(preguntaModel);
                index++;
            }

            encuestaModel.Preguntas = preguntasModel;

            LoadUserData();
            await LoadMetadata();

            return View(encuestaModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(EncuestaViewModel encuestaModel)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            if (ModelState.IsValid)
            {
                var encuestaEntity = new Encuesta
                {
                    IdEncuesta = encuestaModel.Id,
                    Nombre = encuestaModel.NombreEncuesta,
                    Descripcion = encuestaModel.DescripcionEncuesta,
                    Interno = encuestaModel.EsInterno,
                    IdEmpresa = encuestaModel.IdEmpresa,
                    Activo = encuestaModel.Activo
                };

                encuestaEntity.Preguntas = new List<Pregunta>();
                encuestaModel.Preguntas.ForEach(p =>
                {
                    Pregunta pregunta = new Pregunta
                    {
                        IdPregunta = p.Id,
                        IdEncuesta = encuestaEntity.IdEncuesta,
                        IdMetadata = p.IdMetadata,
                        Descripcion = p.Descripcion,
                        Requerido = p.Requerido,
                        Eliminado = p.Eliminado
                    };

                    pregunta.ItemsPreguntas = new List<ItemPregunta>();
                    p.ItemsPregunta.ForEach(ip =>
                    {
                        pregunta.ItemsPreguntas.Add(new ItemPregunta
                        {
                            IdItemPregunta = ip.Id,
                            IdPregunta = pregunta.IdPregunta,
                            Valor = ip.Valor,
                            Eliminado = ip.Eliminado
                        });
                    });

                    encuestaEntity.Preguntas.Add(pregunta);
                });

                await ClienteEncuesta.EditEncuestaAsync(encuestaEntity);

                LoadUserData();
                await LoadMetadata();

                return RedirectToAction("MisEncuestas", "Encuesta", new { idEmpresa = encuestaModel.IdEmpresa });
            }

            return View(encuestaModel);
        }

        [HttpPost]
        public async Task<ActionResult> CrearPregunta(int index)
        {
            await LoadMetadata();

            var nuevaPregunta = new PreguntaViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Eliminado = false,
                Index = index
            };

            return PartialView("~/Views/Encuesta/EditorTemplates/PreguntaViewModel.cshtml", nuevaPregunta);
        }

        [HttpPost]
        public async Task<ActionResult> CrearOpcion(int index)
        {
            await LoadMetadata();

            var nuevaOpcion = new ItemPreguntaViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Eliminado = false
            };

            return PartialView("~/Views/Encuesta/EditorTemplates/ItemPreguntaViewModel.cshtml", nuevaOpcion);
        }

        [HttpGet]
        public PartialViewResult Compartir(string url, string idEmpresa)
        {
            CompartirViewModel modelo = new CompartirViewModel
            {
                Url = url,
                IdEmpresa = idEmpresa
            };

            return PartialView("~/Views/Encuesta/EditorTemplates/_Compartir.cshtml", modelo);
        }

        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Compartir(CompartirViewModel compartirModel)
        {
            if (Session["_SessionUser"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            if (ModelState.IsValid)
            {
                List<string> correos = compartirModel.Correos.Split(',').ToList();

                CorreoEntity entity = new CorreoEntity
                {
                    SMTP = SettingsManager.SMTP,
                    Puerto = int.Parse(SettingsManager.Puerto),
                    Usuario = SettingsManager.UsuarioCorreo,
                    Password = SettingsManager.ContrasenaCorreo,
                    EsSSL = true,
                    EsHTML = true
                };

                string mensaje = System.IO.File.ReadAllText(Server.MapPath("~/Template/MailTemplate.txt"));
                mensaje = mensaje.Replace("{0}", compartirModel.Url);

                Correo.EnviarCorreo(entity, "juanc.pedrazad@gmail.com", correos, null, compartirModel.Asunto, mensaje);

                return RedirectToAction("MisEncuestas", "Encuesta", new { idEmpresa = compartirModel.IdEmpresa });
            }

            return PartialView("~/Views/Encuesta/EditorTemplates/_Compartir.cshtml", compartirModel);
        }

        [HttpGet]
        public ActionResult Reportes(string idEncuesta, string idEmpresa)
        {
            ViewBag.IdEncuesta = idEncuesta;
            ViewBag.IdEmpresa = idEmpresa;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RespuestasCountJson(string idEncuesta)
        {
            List<RespuestasEncuesta> listCountRespuestas = await ClienteEncuesta.GetRespuestasPorEncuestaAsync(idEncuesta);

            return Json(listCountRespuestas);
        }

        #endregion

        #region Private Methods

        private void LoadUserData()
        {
            DataUser dataUser = Session["_SessionUser"] as DataUser;

            ViewBag.Usuario = dataUser.Usuario;
            ViewBag.NombreUsuario = dataUser.Nombre;
            ViewBag.IdEmpresa = dataUser.IdEmpresa;
            ViewBag.NombreEmpresa = dataUser.NombreEmpresa;
            ViewBag.IdRol = dataUser.IdRol;
            ViewBag.NombreRol = dataUser.NombreRol;
        }

        private async Task LoadMetadata()
        {
            var listaComboMetadata = new List<SelectListItem>();
            listaComboMetadata.Add(Seleccione());

            var listMetadata = await ClienteMetadata.GetMetadataAsync();

            foreach (var item in listMetadata)
            {
                listaComboMetadata.Add(new SelectListItem
                {
                    Text = item.Nombre,
                    Value = item.IdMetadata
                });
            }

            ViewBag.ListaMetadata = listaComboMetadata;
        }

        private SelectListItem Seleccione()
        {
            return new SelectListItem
            {
                Text = "Seleccione...",
                Value = "0",
                Selected = true
            };
        }

        private EncuestaViewModel RemoverEliminados(EncuestaViewModel modelo)
        {
            modelo.Preguntas.RemoveAll(p => p.Eliminado);

            foreach (var item in modelo.Preguntas)
            {
                item.ItemsPregunta.RemoveAll(o => o.Eliminado);
            }

            return modelo;
        }

        #endregion
    }
}
