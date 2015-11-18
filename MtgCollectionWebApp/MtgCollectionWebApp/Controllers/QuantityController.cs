using System.Data.Entity;
using System.Web.Http;
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    [Authorize]
    public class QuantityController : ApiController
    {
        private readonly MtgCollectionDB _db = new MtgCollectionDB();

        // PUT: api/Quantity/5
        public void Put(int id, int value)
        {
            var collectionEntry = _db.CollectionsEntries.Find(id);
            if (collectionEntry != null) //If there already is an entry for this card modify or remove it
            {
                var entryQ = collectionEntry.Quantity;
                var q = entryQ + value;

                if (q <= 0) //Can't have entries with quantity 0 or less
                {
                    _db.CollectionsEntries.Remove(collectionEntry);
                } else
                {
                    collectionEntry.Quantity = q;
                    _db.Entry(collectionEntry).State = EntityState.Modified;
                }
            } else if (value > 0) //If entry doesn't exist, create one if val is positive
            {
                collectionEntry = new CollectionEntry
                {
                    CollectionEntryId = id,
                    CollectionEntryCardId = id,
                    CollectionId = User.Identity.Name.GetHashCode(),
                    Quantity = value
                };

                _db.CollectionsEntries.Add(collectionEntry);
            }
            _db.SaveChanges();
        }
    }
}
