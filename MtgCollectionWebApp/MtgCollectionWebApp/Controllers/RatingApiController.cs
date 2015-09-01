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
    public class RatingApiController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/Rating
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/Rating/5
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> GetRating(int id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Rating/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Rating
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> PostRating(String cardName, int grade)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest(ModelState);

            var rating = new Rating();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            rating.RatingId = cardName.GetHashCode();
            rating.Cards = db.Cards.Where( a => a.CardName == cardName).ToList();
            rating.RatingValue = grade;
            rating.UserId = User.Identity.Name.GetHashCode();

            db.Ratings.Add(rating);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating);
        }

        // DELETE: api/Rating/5
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> DeleteRating(int id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            await db.SaveChangesAsync();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return db.Ratings.Count(e => e.RatingId == id) > 0;
        }
    }
}