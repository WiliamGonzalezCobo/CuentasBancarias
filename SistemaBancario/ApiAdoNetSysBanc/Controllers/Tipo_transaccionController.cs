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

namespace ApiAdoNetSysBanc.Controllers
{
    public class Tipo_transaccionController : ApiController
    {
        private SistemaBancarioEntities db = new SistemaBancarioEntities();

        // GET: api/Tipo_transaccion
        public IQueryable<tipo_transaccion> Gettipo_transaccion()
        {
            return db.tipo_transaccion;
        }

        // GET: api/Tipo_transaccion/5
        [ResponseType(typeof(tipo_transaccion))]
        public async Task<IHttpActionResult> Gettipo_transaccion(int id)
        {
            tipo_transaccion tipo_transaccion = await db.tipo_transaccion.FindAsync(id);
            if (tipo_transaccion == null)
            {
                return NotFound();
            }

            return Ok(tipo_transaccion);
        }

        // PUT: api/Tipo_transaccion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttipo_transaccion(int id, tipo_transaccion tipo_transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_transaccion.id_tipo_transaccion)
            {
                return BadRequest();
            }

            db.Entry(tipo_transaccion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_transaccionExists(id))
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

        // POST: api/Tipo_transaccion
        [ResponseType(typeof(tipo_transaccion))]
        public async Task<IHttpActionResult> Posttipo_transaccion(tipo_transaccion tipo_transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipo_transaccion.Add(tipo_transaccion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo_transaccion.id_tipo_transaccion }, tipo_transaccion);
        }

        // DELETE: api/Tipo_transaccion/5
        [ResponseType(typeof(tipo_transaccion))]
        public async Task<IHttpActionResult> Deletetipo_transaccion(int id)
        {
            tipo_transaccion tipo_transaccion = await db.tipo_transaccion.FindAsync(id);
            if (tipo_transaccion == null)
            {
                return NotFound();
            }

            db.tipo_transaccion.Remove(tipo_transaccion);
            await db.SaveChangesAsync();

            return Ok(tipo_transaccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipo_transaccionExists(int id)
        {
            return db.tipo_transaccion.Count(e => e.id_tipo_transaccion == id) > 0;
        }
    }
}