using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ApiAdoNetSysBanc.Models;
using SistemaBanc.Utils.Recursos;

namespace ApiAdoNetSysBanc.Controllers
{
    public class CuentasController : ApiController
    {
        private SistemaBancarioEntities db = new SistemaBancarioEntities();

        // GET: api/Cuentas
        public IQueryable<cuenta> Getcuentas()
        {
            return db.cuentas;
        }

        // GET: api/Cuentas/Cliente/1
        [ResponseType(typeof(List<cuenta>))]
        [HttpGet]
        [Route("api/Cuentas/Cliente/{idCliente?}")]
        public HttpResponseMessage GetCuentasxCliente(int idCliente)
        {
            var ListCuentasxCliente = (from cta in db.cuentas
                           where cta.id_Cliente == idCliente
                           select new
                           {
                               id_Cliente = cta.id_Cliente,
                               id_Cuenta = cta.id_Cuenta,
                               saldo = cta.saldo,
                               fecha_creacion = cta.fecha_creacion
                           }).ToList();


            if (ListCuentasxCliente == null && ListCuentasxCliente.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ListCuentasxCliente);
        }

        // GET: api/Cuentas/5
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Getcuenta(int id)
        {
            cuenta cuenta = await db.cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            return Ok(cuenta);
        }

        // PUT: api/Cuentas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcuenta(int id, cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.id_Cuenta)
            {
                return BadRequest();
            }

            db.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cuentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cuentas
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Postcuenta(cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.cuentas.Add(cuenta);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cuenta.id_Cuenta }, cuenta);
        }

        // DELETE: api/Cuentas/5
        [ResponseType(typeof(cuenta))]
        public async Task<IHttpActionResult> Deletecuenta(int id)
        {
            cuenta cuenta = await db.cuentas.FindAsync(id);
            if (cuenta == null)
            {
                return NotFound();
            }

            db.cuentas.Remove(cuenta);
            await db.SaveChangesAsync();

            return Ok(cuenta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cuentaExists(int id)
        {
            return db.cuentas.Count(e => e.id_Cuenta == id) > 0;
        }
    }
}