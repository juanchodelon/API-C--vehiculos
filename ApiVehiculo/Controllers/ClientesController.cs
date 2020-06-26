using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiVehiculo.Controllers
{
    public class ClientesController : ApiController
    {
        private DBTestOneEntities dbContext = new DBTestOneEntities();

        [HttpGet]

        public IEnumerable<Cliente> Get()
        {
            using (dbContext)
            {
                return dbContext.Cliente.ToList();
            }
        }

        [HttpGet]

        public Cliente Get(int id)
        {
            using (dbContext)
            {
                return dbContext.Cliente.FirstOrDefault(e => e.ID_cliente == id);
            }
        }

        [HttpPost]

        public IHttpActionResult Agregar([FromBody]Cliente cli)
        {
            if (ModelState.IsValid)
            {
                dbContext.Cliente.Add(cli);
                dbContext.SaveChanges();
                return Ok(cli);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Modificar(int id, [FromBody]Cliente cli)
        {
            if (ModelState.IsValid)
            {
                var existente = dbContext.Cliente.Count(e => e.ID_cliente == id) > 0;
                if (existente)
                {
                    dbContext.Entry(cli).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    return Ok(cli);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            var existente = dbContext.Cliente.Find(id);
            if (existente != null)
            {
                dbContext.Cliente.Remove(existente);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
