using System.Linq;
using System.Web.Http;
using MtgCollectionWebApp.Models;
using System.Collections.Generic;

namespace MtgCollectionWebApp.Controllers
{
    [Authorize]
    public class CardsApiController : ApiController
    {
        private readonly MtgCollectionDB _db = new MtgCollectionDB();

        // GET: api/CardsApi
        public List<CardsViewModel> GetCards()
        {
            var data = new List<CardsViewModel>();

            foreach (var card in _db.Cards)
            {
                var ratings = _db.Ratings.Where(r => r.RatingCardName == card.CardName);
                var entry = _db.CollectionsEntries.Find(card.CardId);
                var quantity = 0;
                var ratingVal = 0; 

                if (ratings.Any())
                {
                    var sum = ratings.Sum(r => r.RatingValue);
                    ratingVal = sum / ratings.Count();
                }

                if (entry != null)
                {
                    quantity = entry.Quantity;
                }

                data.Add(new CardsViewModel { Card = card, Quantity = quantity, Rating = ratingVal });
            }

            return data;
        }
    }
}