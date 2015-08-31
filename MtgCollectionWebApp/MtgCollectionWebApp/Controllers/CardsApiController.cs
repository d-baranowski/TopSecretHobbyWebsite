using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MtgCollectionWebApp.Models;
using System.Collections.Generic;

namespace MtgCollectionWebApp.Controllers
{
    public class CardsApiController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/CardsApi
        public List<CardsViewModel> GetCards()
        {
            var data = new List<CardsViewModel>();

            foreach(Card c in db.Cards)
            {
                var entry = db.CollectionsEntries.Find(c.CardId);
                if (entry != null)
                {
                    data.Add(new CardsViewModel { Card = c, Quantity = entry.Quantity });
                } else
                {
                    data.Add(new CardsViewModel { Card = c, Quantity = 0 });
                }
               
            }

            return data;
        }

        // GET: api/CardsApi/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult GetCard(int id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/CardsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCard(int id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.CardId)
            {
                return BadRequest();
            }

            db.Entry(card).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/CardsApi
        [ResponseType(typeof(Card))]
        public IHttpActionResult PostCard(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cards.Add(card);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.CardId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = card.CardId }, card);
        }

        // DELETE: api/CardsApi/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult DeleteCard(int id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(int id)
        {
            return db.Cards.Count(e => e.CardId == id) > 0;
        }
    }
}