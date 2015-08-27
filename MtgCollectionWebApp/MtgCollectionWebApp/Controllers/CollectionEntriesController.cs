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
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    public class CollectionEntriesController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/CollectionEntries
        public IQueryable<CollectionEntry> GetCollectionsEntries()
        {
            return db.CollectionsEntries;
        }

        // GET: api/CollectionEntries/5
        [ResponseType(typeof(CollectionEntry))]
        public async Task<IHttpActionResult> GetCollectionEntry(int id)
        {
            CollectionEntry collectionEntry = await db.CollectionsEntries.FindAsync(id);
            if (collectionEntry == null)
            {
                return NotFound();
            }

            return Ok(collectionEntry);
        }

        // PUT: api/CollectionEntries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCollectionEntry(int id, CollectionEntry collectionEntry)
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
                await db.SaveChangesAsync();
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

        // POST: api/CollectionEntries
        [ResponseType(typeof(CollectionEntry))]
        public async Task<IHttpActionResult> PostCollectionEntry(CollectionEntry collectionEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CollectionsEntries.Add(collectionEntry);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = collectionEntry.CollectionEntryId }, collectionEntry);
        }

        // DELETE: api/CollectionEntries/5
        [ResponseType(typeof(CollectionEntry))]
        public async Task<IHttpActionResult> DeleteCollectionEntry(int id)
        {
            CollectionEntry collectionEntry = await db.CollectionsEntries.FindAsync(id);
            if (collectionEntry == null)
            {
                return NotFound();
            }

            db.CollectionsEntries.Remove(collectionEntry);
            await db.SaveChangesAsync();

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