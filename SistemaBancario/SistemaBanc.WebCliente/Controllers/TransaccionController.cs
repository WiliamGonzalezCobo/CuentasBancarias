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
    public class TransaccionController : BaseController
    {
        // GET: Transaccion
        public ActionResult Index(int? idCuenta, int? idTipoTran,int? idCliente)
        {
            if (System.Web.HttpContext.Current.Session["sessionString"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            TransaccionModelo tranModel = new TransaccionModelo();
            tranModel.id_Cuenta = idCuenta.HasValue ? idCuenta.Value : 0;
            tranModel.tipoTransaccion = idTipoTran.HasValue ? idTipoTran.Value : 0;
            tranModel.id_Cliente = idCliente.HasValue ? idCliente.Value : 0;
            return View(tranModel);
        }

        // GET: Transaccion
        [HttpPost]
        public async Task<ActionResult> Index(TransaccionModelo transaccionModel)
        {
            if (System.Web.HttpContext.Current.Session["sessionString"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            EntityTransaccion entityTrans = null;

            
            try
            {
                entityTrans = new EntityTransaccion()
                {
                    id_Cuenta = transaccionModel.id_Cuenta,
                    id_CuentaDestino = transaccionModel.id_CuentaDestino,
                    valor = transaccionModel.monto,
                    id_tipo_transaccion = transaccionModel.tipoTransaccion
                };

                var respuesta = await ClienteTransaccion.PostCrearTransaccion(entityTrans);

                return RedirectToAction("Index","Cuenta",new { idCliente = transaccionModel.id_Cliente });
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message; 
                return View(transaccionModel);
            }
        }

        // GET: Transaccion / movimientos
        public async Task<ActionResult> Movimientos(int? idCuenta, int? idCliente)
        {
            if (System.Web.HttpContext.Current.Session["sessionString"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            List<TransaccionModelo> listTranModel = new List<TransaccionModelo>();
            List<EntityTransaccion> listEntityTransaccion = new List<EntityTransaccion>();
            if (idCuenta.HasValue && idCliente.HasValue)
            {
                listEntityTransaccion = await ClienteTransaccion.GetTransaccionesByCtaAync(idCuenta.Value);
            }

            foreach (var item in listEntityTransaccion)
            {
                listTranModel.Add(new TransaccionModelo()
                {
                    id_Cliente = idCliente.Value,
                    id_transaccion = item.id_transaccion,
                    fecha = item.fecha,
                    id_CuentaDestino = item.id_CuentaDestino,
                    tipoTransaccion = item.id_tipo_transaccion,
                    monto = item.valor
                });

            }
            return View(listTranModel);
        }
    }
}
