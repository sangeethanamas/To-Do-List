using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using To_Do_List.Models;

namespace To_Do_List.Controllers
{
    public class listsController : ApiController
    {
        private ListModels db = new ListModels();

        // GET: api/lists
        public IQueryable<list> Getlists()
        {
            return db.lists;
        }

        // GET: api/lists/5
        [ResponseType(typeof(list))]
        public IHttpActionResult Getlist(int id)
        {
            list list = db.lists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // PUT: api/lists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlist(int id, list list)
        {
            

            if (id != list.Id)
            {
                return BadRequest();
            }

            db.Entry(list).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listExists(id))
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

        // POST: api/lists
        [ResponseType(typeof(list))]
        public IHttpActionResult Postlist(list list)
        {
            

            db.lists.Add(list);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = list.Id }, list);
        }

        // DELETE: api/lists/5
        [ResponseType(typeof(list))]
        public IHttpActionResult Deletelist(int id)
        {
            list list = db.lists.Find(id);
            if (list == null)
            {
                return NotFound();
            }

            db.lists.Remove(list);
            db.SaveChanges();

            return Ok(list);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool listExists(int id)
        {
            return db.lists.Count(e => e.Id == id) > 0;
        }
    }
}