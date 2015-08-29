using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MtgCollectionWebApp.Models
{
    public class QuantityController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/Quantity/5
        public int Get(int id)
        {
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry != null)
            {
                return collectionEntry.Quantity;
            } 
            return 0;
        }

        // PUT: api/Quantity/5
        public void Put(int id, int value)
        {
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry != null) //If there already is an entry for this card
            {
                var entryQ = collectionEntry.Quantity;
                var q = entryQ + value; //Calculate new quantity 

                if (q <= 0) //Can't have entries with quantity 0 or less
                {
                    db.CollectionsEntries.Remove(collectionEntry);
                }
                else //Modify
                {
                    collectionEntry.Quantity = q;
                    db.Entry(collectionEntry).State = EntityState.Modified;
                }
            }
            else //If entry doesn't exist, create one if val is positive
            {
                if (value > 0)
                {
                    collectionEntry = new CollectionEntry
                    {
                        CollectionEntryId = id,
                        CollectionEntryCardId = id,
                        CollectionId = User.Identity.Name.GetHashCode(),
                        Quantity = value
                    };

                    db.CollectionsEntries.Add(collectionEntry);
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
        }
    }
}
