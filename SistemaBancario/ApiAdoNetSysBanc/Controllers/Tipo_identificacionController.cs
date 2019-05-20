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
    public class Tipo_identificacionController : ApiController
    {
        private SistemaBancarioEntities db = new SistemaBancarioEntities();

        // GET: api/Tipo_identificacion
        public IQueryable<tipo_identificacion> Gettipo_identificacion()
        {
            return db.tipo_identificacion;
        }

        // GET: api/Tipo_identificacion/5
        [ResponseType(typeof(tipo_identificacion))]
        public async Task<IHttpActionResult> Gettipo_identificacion(int id)
        {
            tipo_identificacion tipo_identificacion = await db.tipo_identificacion.FindAsync(id);
            if (tipo_identificacion == null)
            {
                return NotFound();
            }

            return Ok(tipo_identificacion);
        }

        // PUT: api/Tipo_identificacion/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttipo_identificacion(int id, tipo_identificacion tipo_identificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_identificacion.id_tipo_identificacion)
            {
                return BadRequest();
            }

            db.Entry(tipo_identificacion).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_identificacionExists(id))
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

        // POST: api/Tipo_identificacion
        [ResponseType(typeof(tipo_identificacion))]
        public async Task<IHttpActionResult> Posttipo_identificacion(tipo_identificacion tipo_identificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipo_identificacion.Add(tipo_identificacion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipo_identificacion.id_tipo_identificacion }, tipo_identificacion);
        }

        // DELETE: api/Tipo_identificacion/5
        [ResponseType(typeof(tipo_identificacion))]
        public async Task<IHttpActionResult> Deletetipo_identificacion(int id)
        {
            tipo_identificacion tipo_identificacion = await db.tipo_identificacion.FindAsync(id);
            if (tipo_identificacion == null)
            {
                return NotFound();
            }

            db.tipo_identificacion.Remove(tipo_identificacion);
            await db.SaveChangesAsync();

            return Ok(tipo_identificacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipo_identificacionExists(int id)
        {
            return db.tipo_identificacion.Count(e => e.id_tipo_identificacion == id) > 0;
        }
    }
}