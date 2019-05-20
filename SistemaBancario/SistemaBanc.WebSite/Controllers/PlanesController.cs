namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using Entidades.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    public class PlanesController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public async Task<ActionResult> Precios()
        {
            try
            {
                List<PlanesPreciosViewModel> modelo = new List<PlanesPreciosViewModel>();

                List<PlanEncuesta> listaPlanes = await ClientePlanEncuesta.GetPlanesAsync();

                foreach (var item in listaPlanes)
                {
                    var planesModel = new PlanesPreciosViewModel();

                    planesModel.Id = item.IdPlan;
                    planesModel.Nombre = item.Nombre;
                    planesModel.Descripcion = item.Descripcion;
                    planesModel.Precio = item.Precio;
                    planesModel.Promocion = item.Promocion;
                    planesModel.Items = item.ItemPlanes.AsEnumerable()
                        .Where(x => x.Activo == true)
                        .OrderBy(x => x.Descripcion)
                        .Select(x => x.Descripcion).ToList();

                    modelo.Add(planesModel);
                }

                modelo = modelo.AsEnumerable().OrderBy(x => x.Precio).ToList();

                return View(modelo);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        #endregion
    }
}