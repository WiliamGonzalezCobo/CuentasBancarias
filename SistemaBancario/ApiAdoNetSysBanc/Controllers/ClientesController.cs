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
    public class ClientesController : ApiController
    {
        private SistemaBancarioEntities db = new SistemaBancarioEntities();

        // GET: api/Clientes
        public IQueryable<cliente> Getclientes()
        {
            return db.clientes;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Getcliente(int id)
        {
            cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putcliente(int id, cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.id_cliente)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Postcliente(cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.clientes.Add(cliente);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cliente.id_cliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(cliente))]
        public async Task<IHttpActionResult> Deletecliente(int id)
        {
            cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.clientes.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clienteExists(int id)
        {
            return db.clientes.Count(e => e.id_cliente == id) > 0;
        }
    }
}