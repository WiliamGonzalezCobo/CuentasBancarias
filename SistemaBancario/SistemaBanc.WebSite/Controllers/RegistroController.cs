namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using Entidades.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Utilidades.Recursos;
    using Utilidades.Serializer;

    #endregion

    public class RegistroController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public async Task<ActionResult> Registrar(string idPlan)
        {
            try
            {
                RegistroViewModel registro = new RegistroViewModel();

                await CargarPlanes(idPlan);

                CargarTiposTarjeta();
                CargarFranquicias();
                CargarPaisTarjeta();
                CargarMesesTarjeta();
                CargarAniosTarjeta();
                CargarNumeroCuotas();

                return View(registro);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("NotFound", "Error");
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPrecio(string idPlan)
        {
            if (idPlan != null && idPlan != "0")
            {
                string precio = await CargarPrecio(idPlan);
                return Json(precio, JsonRequestBehavior.AllowGet);
            }

            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Registrar(RegistroViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(modelo);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message.ToString();
                return View();
            }
        }

        #endregion

        #region Private Methods

        private SelectListItem Seleccione()
        {
            return new SelectListItem
            {
                Text = "Seleccione...",
                Value = "0",
                Selected = true
            };
        }

        private async Task CargarPlanes(string idPlan)
        {
            List<PlanEncuesta> planesEntidad = await ClientePlanEncuesta.GetPlanesAsync();

            List<SelectListItem> planes = new List<SelectListItem>();

            planes.Add(Seleccione());

            foreach (var item in planesEntidad)
            {
                planes.Add(new SelectListItem
                {
                    Text = item.Nombre,
                    Value = item.IdPlan.ToString(),
                    Selected = item.IdPlan == idPlan
                });
            }

            ViewBag.ListaComboPlanes = planes;
        }

        private async Task<string> CargarPrecio(string idPlan)
        {
            PlanEncuesta planesEncuesta = await ClientePlanEncuesta.GetPlanByIdAsync(idPlan);
            return planesEncuesta.Precio.ToString();
        }

        private void CargarTiposTarjeta()
        {
            string[] tiposTarjetaArray = Mensajes.ListaTipoTarjeta.Split('|');

            List<SelectListItem> tiposTarjeta = new List<SelectListItem>();

            tiposTarjeta.Add(Seleccione());

            foreach (string tipo in tiposTarjetaArray)
            {
                tiposTarjeta.Add(new SelectListItem()
                {
                    Text = tipo,
                    Value = tipo
                });
            }

            ViewBag.ListaComboTipoTarjeta = tiposTarjeta;
        }

        private void CargarFranquicias()
        {
            string[] franquiciasArray = Mensajes.ListaFranquicias.Split('|');

            List<SelectListItem> franquicias = new List<SelectListItem>();

            franquicias.Add(Seleccione());

            foreach (string franquicia in franquiciasArray)
            {
                franquicias.Add(new SelectListItem()
                {
                    Text = franquicia,
                    Value = franquicia
                });
            }

            ViewBag.ListaComboFranquicia = franquicias;
        }

        private void CargarPaisTarjeta()
        {
            List<SelectListItem> paises = new List<SelectListItem>();
            paises.Add(Seleccione());

            List<string> paisesList = Json<string>.JSONStringToList(Mensajes.ListaPaises);

            if (paisesList.Count > 0)
            {
                var entidadPais = from pais in paisesList.OrderBy(co => co.ToString())
                                  select new SelectListItem()
                                  {
                                      Value = pais,
                                      Text = pais
                                  };

                paises.AddRange(entidadPais);
            }

            ViewBag.ListaComboPaisTarjeta = paises;
        }

        private void CargarMesesTarjeta()
        {
            int mesVal = 1;
            string[] mesesArray = Mensajes.ListaMeses.Split('|');

            List<SelectListItem> meses = new List<SelectListItem>();

            meses.Add(Seleccione());

            foreach (string mes in mesesArray)
            {
                meses.Add(new SelectListItem()
                {
                    Text = mes,
                    Value = mesVal.ToString(CultureInfo.CurrentCulture)
                });

                mesVal++;
            }

            ViewBag.ListaComboMesTarjeta = meses;
        }

        private void CargarAniosTarjeta()
        {
            List<SelectListItem> anios = new List<SelectListItem>();
            anios.Add(Seleccione());

            for (int i = DateTime.Now.Year; i <= (DateTime.Now.Year + 14); i++)
            {
                anios.Add(new SelectListItem()
                {
                    Text = i.ToString(CultureInfo.CurrentCulture).Substring(2),
                    Value = i.ToString(CultureInfo.CurrentCulture)
                });
            }

            ViewBag.ListaComboAnioTarjeta = anios;
        }

        private void CargarNumeroCuotas()
        {
            List<SelectListItem> cuotas = new List<SelectListItem>();
            cuotas.Add(Seleccione());

            for (int i = 1; i <= 12; i++)
            {
                cuotas.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            ViewBag.ListaComboNumeroCuotas = cuotas;
        }

        #endregion
    }
}
