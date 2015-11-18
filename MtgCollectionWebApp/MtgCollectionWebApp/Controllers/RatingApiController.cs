using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    [Authorize]
    public class RatingApiController : ApiController
    {
        private readonly MtgCollectionDB _db = new MtgCollectionDB();

        // GET: api/RatingApi
        public IQueryable<Rating> GetRatings()
        {
            return _db.Ratings;
        }

        // GET: api/RatingApi/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            var rating = _db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/RatingApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            _db.Entry(rating).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

        // POST: api/RatingApi
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating data)
        {
            if (data == null)
            {
                return BadRequest();
            }

            Rating rating;
            var userId = User.Identity.Name.GetHashCode();
            var userRatings = _db.Ratings.Where(r => r.UserId == userId);
            
            if (userRatings.Any())
            {
                //If there exist a rating from this user for this card 
                var cardRating = userRatings.Where(r => r.RatingCardName == data.RatingCardName);
                if (cardRating.Any()) 
                {
                    rating = cardRating.First();
                    rating.RatingValue = data.RatingValue;
                    _db.Entry(rating).State = EntityState.Modified;
                    _db.SaveChanges();
                    return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating);
                }
            }

            rating = new Rating
            {
                RatingValue = data.RatingValue,
                RatingCardName = data.RatingCardName,
                UserId = userId
            };
            
            //Success
            _db.Ratings.Add(rating);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating); 
        }

        // DELETE: api/RatingApi/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            var rating = _db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            _db.Ratings.Remove(rating);
            _db.SaveChanges();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(int id)
        {
            return _db.Ratings.Count(e => e.RatingId == id) > 0;
        }
    }
}