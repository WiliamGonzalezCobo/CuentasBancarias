using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaBanc.WebCliente.Models;
using SistemaBanc.Entities;
using System.Threading.Tasks;

namespace SistemaBanc.WebCliente.Controllers
{
    public class CuentaController : BaseController
    {
        // GET: Cuenta
        public async Task<ActionResult> Index(int? idCliente)
        {


            if (System.Web.HttpContext.Current.Session["sessionString"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            List<CuentaModel> listModelCuentas = new List<CuentaModel>();
            List<EntityCuenta> listCuentas = await ClienteCuenta.GetCuentasByClienteAsync(idCliente.HasValue ? idCliente.Value : 0);

            foreach (var item in listCuentas)
            {
                var cuentaModel = new CuentaModel
                {
                    id_Cuenta = item.id_Cuenta,
                    fecha_creacion = item.fecha_creacion,
                    id_Cliente = item.id_Cliente,
                    saldo = item.saldo
                };

                listModelCuentas.Add(cuentaModel);
            }
            return View(listModelCuentas);
        }
        
    }
}
