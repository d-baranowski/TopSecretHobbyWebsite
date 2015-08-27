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
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    public class EntriesController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/Entries
        public IQueryable<CollectionEntry> GetCollectionsEntries()
        {
            return db.CollectionsEntries;
        }

        // GET: api/Entries/5
        [ResponseType(typeof(CollectionEntry))]
        public IHttpActionResult GetCollectionEntry(int id)
        {
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry == null)
            {
                return NotFound();
            }

            return Ok(collectionEntry);
        }

        // PUT: api/Entries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCollectionEntry(int id, CollectionEntry collectionEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != collectionEntry.CollectionEntryId)
            {
                return BadRequest();
            }

            db.Entry(collectionEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionEntryExists(id))
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

        // POST: api/Entries
        [ResponseType(typeof(CollectionEntry))]
        public IHttpActionResult PostCollectionEntry(CollectionEntry collectionEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CollectionsEntries.Add(collectionEntry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = collectionEntry.CollectionEntryId }, collectionEntry);
        }

        // DELETE: api/Entries/5
        [ResponseType(typeof(CollectionEntry))]
        public IHttpActionResult DeleteCollectionEntry(int id)
        {
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry == null)
            {
                return NotFound();
            }

            db.CollectionsEntries.Remove(collectionEntry);
            db.SaveChanges();

            return Ok(collectionEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CollectionEntryExists(int id)
        {
            return db.CollectionsEntries.Count(e => e.CollectionEntryId == id) > 0;
        }
    }
}