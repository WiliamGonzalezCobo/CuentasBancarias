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
using SistemaBanc.Entities;
using SistemaBanc.Utils.Recursos;

namespace ApiAdoNetSysBanc.Controllers
{
    public class TransaccionsController : ApiController
    {
        private SistemaBancarioEntities db = new SistemaBancarioEntities();

        // GET: api/Transaccions
        public IQueryable<transaccion> Gettransaccions()
        {
            return db.transaccions;
        }

        // GET: api/Transaccions/Cuenta/1
        [ResponseType(typeof(List<transaccion>))]
        [HttpGet]
        [Route("api/Transaccions/Cuenta/{idCuenta?}")]
        public HttpResponseMessage GettransaccionsCuenta(int idCuenta)
        {
            var ListTransxCliente = (from cta in db.transaccions
                                     where cta.id_Cuenta == idCuenta
                                     select new
                                     {
                                         idCuenta = cta.id_Cuenta,
                                         id_transaccion = cta.id_transaccion,
                                         valor = cta.valor,
                                         fecha = cta.fecha,
                                         id_tipo_transaccion = cta.id_tipo_transaccion
                                     }).ToList();

            if (ListTransxCliente == null && ListTransxCliente.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            return Request.CreateResponse(HttpStatusCode.OK, ListTransxCliente);
        }

        // GET: api/Transaccions/5
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Gettransaccion(int id)
        {
            transaccion transaccion = await db.transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            return Ok(transaccion);
        }

        // PUT: api/Transaccions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttransaccion(int id, transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaccion.id_transaccion)
            {
                return BadRequest();
            }

            db.Entry(transaccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!transaccionExists(id))
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

        // POST: api/Transaccions
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Posttransaccion(transaccion transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.transaccions.Add(transaccion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = transaccion.id_transaccion }, transaccion);
        }

        // DELETE: api/Transaccions/5
        [ResponseType(typeof(transaccion))]
        public async Task<IHttpActionResult> Deletetransaccion(int id)
        {
            transaccion transaccion = await db.transaccions.FindAsync(id);
            if (transaccion == null)
            {
                return NotFound();
            }

            db.transaccions.Remove(transaccion);
            await db.SaveChangesAsync();

            return Ok(transaccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool transaccionExists(int id)
        {
            return db.transaccions.Count(e => e.id_transaccion == id) > 0;
        }

        // POST: api/Transaccions/crearTran
        [ResponseType(typeof(bool))]
        [Route("api/tramites/crearTran")]
        public HttpResponseMessage PostCrearTran(EntityTransaccion entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Datos Invalidos");
                }

                db.realizarTransaccion(entity.id_Cuenta, entity.valor, entity.id_tipo_transaccion, entity.id_CuentaDestino);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }

        }
    }
}