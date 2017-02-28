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
            var userId = User.Identity.GetUserId();
            return db.Sonnets
                .Where(
                    s => s.Public == true
                    || s.OwnerId == userId
                    );
        }

        // GET: api/Sonnet/5
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult GetSonnet(int id)
        {
            var userId = User.Identity.GetUserId();
            Sonnet sonnet = db.Sonnets
                .Where(
                    s => s.Id == id 
                    && (s.Public == true || s.OwnerId == userId)
                    )
                .FirstOrDefault();

            if (sonnet == null)
            {
                return NotFound();
            }

            return Ok(sonnet);
        }

        // POST: api/Sonnet
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult PostSonnet(Sonnet sonnet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();

            if (userId == null)
            {
                sonnet.OwnerId = null;
                sonnet.Public = true;
            }
            else
            {
                sonnet.OwnerId = userId;
            }

            sonnet.Created = DateTime.Now;
            sonnet.NewUrl = Helpers.EncryptionHelp.AndGo();

            db.Sonnets.Add(sonnet);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sonnet.Id }, sonnet);
        }

        // PUT: api/Sonnet/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSonnet(int id, Sonnet sonnet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();

            sonnet.Id = id;           
            
            //How to get OwnerId in from Form
            if (sonnet.OwnerId != userId)
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

        // DELETE: api/Sonnet/5
        [Authorize]
        [ResponseType(typeof(Sonnet))]
        public IHttpActionResult DeleteSonnet(int id)
        {
            var userId = User.Identity.GetUserId();

            Sonnet sonnet = db.Sonnets
                .Where(
                    s => s.Id == id
                    && s.OwnerId == userId
                    )
                .FirstOrDefault();

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