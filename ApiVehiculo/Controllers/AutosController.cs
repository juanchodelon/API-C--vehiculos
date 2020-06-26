using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiVehiculo.Controllers
{
    public class AutosController : ApiController
    {
        private DBTestOneEntities dbContext = new DBTestOneEntities();

        [HttpGet]

        public IEnumerable<Vehiculo> Get()
        {
            using (dbContext)
            {
                return dbContext.Vehiculo.ToList();
            }
        }

        [HttpGet]

        public Vehiculo Get(int id)
        {
            using (dbContext)
            {
                return dbContext.Vehiculo.FirstOrDefault(e => e.ID_vehiculo == id);
            }
        }

        [HttpPost]

        public IHttpActionResult Agregar([FromBody]Vehiculo cli)
        {
            if (ModelState.IsValid)
            {
                dbContext.Vehiculo.Add(cli);
                dbContext.SaveChanges();
                return Ok(cli);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult Modificar(int id, [FromBody]Vehiculo cli)
        {
            if (ModelState.IsValid)
            {
                var existente = dbContext.Vehiculo.Count(e => e.ID_vehiculo == id) > 0;
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
            var existente = dbContext.Vehiculo.Find(id);
            if (existente != null)
            {
                dbContext.Vehiculo.Remove(existente);
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
