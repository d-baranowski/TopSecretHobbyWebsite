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

            foreach (Card c in db.Cards)
            {
                var ratings = db.Ratings.Where(r => r.RatingCardName == c.CardName);
                var entry = db.CollectionsEntries.Find(c.CardId);
                int quantity = 0;
                int ratingVal = 0; 

                if (ratings.Count() > 0)
                {
                    int sum = ratings.Sum(r => r.RatingValue);
                    ratingVal = sum / ratings.Count();
                }

                if (entry != null)
                {
                    quantity = entry.Quantity;
                }

                data.Add(new CardsViewModel { Card = c, Quantity = quantity, Rating = ratingVal });

            }

            return data;
        }
    }
}