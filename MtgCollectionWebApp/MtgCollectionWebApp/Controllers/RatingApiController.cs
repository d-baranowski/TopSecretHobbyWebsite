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
    public class RatingApiController : ApiController
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/RatingApi
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/RatingApi/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
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

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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
            
            Rating rating = null;
            int userId = User.Identity.Name.GetHashCode();

            var userRatings = db.Ratings.Where(r => r.UserId == userId);
            int test = userRatings.Count();
            
            if (test > 0)
            {
                var cardRating = userRatings.Where(r => r.RatingCardName == data.RatingCardName);
                int test2 = cardRating.Count();
                if (test2 > 0) //If there exist a rating from this user for this card 
                {
                    rating = cardRating.First();
                    rating.RatingValue = data.RatingValue;
                    db.Entry(rating).State = EntityState.Modified;
                    db.SaveChanges();
                    return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating);
                }
            }

            rating = new Rating();
            rating.RatingValue = data.RatingValue;
            rating.RatingCardName = data.RatingCardName;
            rating.UserId = userId; //This might throw an exception 

            //Success
            db.Ratings.Add(rating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating); 
        }

        // DELETE: api/RatingApi/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = db.Ratings.Find(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            db.SaveChanges();

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