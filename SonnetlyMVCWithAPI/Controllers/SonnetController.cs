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
using SonnetlyMVCWithAPI.Models;
using Microsoft.AspNet.Identity;

namespace SonnetlyMVCWithAPI.Controllers
{
    public class SonnetController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Sonnet
        public IQueryable<Sonnet> GetSonnets()
        {
            return db.Sonnets;
        }

        // GET: api/Sonnet/5
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult GetSonnet(int id)
        {
            Sonnet sonnet = db.Sonnets.Find(id);
            if (sonnet == null)
            {
                return NotFound();
            }

            return Ok(sonnet);
        }

        // PUT: api/Sonnet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSonnet(int id, Sonnet sonnet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sonnet.Id)
            {
                return BadRequest();
            }

            db.Entry(sonnet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SonnetExists(id))
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

        // POST: api/Sonnet
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult PostSonnet(Sonnet sonnet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            sonnet.OwnerId = User.Identity.GetUserId();
            sonnet.Created = DateTime.Now;
            sonnet.NewUrl = Helpers.EncryptionHelp.AndGo();

            db.Sonnets.Add(sonnet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sonnet.Id }, sonnet);
        }

        // DELETE: api/Sonnet/5
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult DeleteSonnet(int id)
        {
            Sonnet sonnet = db.Sonnets.Find(id);
            if (sonnet == null)
            {
                return NotFound();
            }

            db.Sonnets.Remove(sonnet);
            db.SaveChanges();

            return Ok(sonnet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SonnetExists(int id)
        {
            return db.Sonnets.Count(e => e.Id == id) > 0;
        }
    }
}