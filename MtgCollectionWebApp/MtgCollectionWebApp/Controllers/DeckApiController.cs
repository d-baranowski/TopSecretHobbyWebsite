using MtgCollectionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MtgCollectionWebApp.Controllers
{
    public class DeckApiController : ApiController
    {
        private readonly MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/CardsApi
        public List<DeckViewModel> GetDecks()
        {
            var data = new List<DeckViewModel>();

            var decks = db.Decks.Where(d => d.OwnerId == User.Identity.Name.GetHashCode());

            foreach (Deck d in decks)
            {
                var ratings = db.DeckRatings.Where(r => r.RatingDeckId == d.DeckId);
                int ratingsVal = 0;

                if (ratings.Count() > 0)
                {
                    foreach (DeckRating r in ratings)
                    {
                        ratingsVal += r.RatingValue;
                    }

                    ratingsVal = ratingsVal / ratings.Count();
                }

                data.Add(new DeckViewModel { Deck = d, Rating = ratingsVal });
            }

            return data;
        }
    }
}
