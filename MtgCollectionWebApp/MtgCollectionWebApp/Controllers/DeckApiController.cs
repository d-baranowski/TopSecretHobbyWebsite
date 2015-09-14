using MtgCollectionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MtgCollectionWebApp.Controllers
{
    public class DeckApiController : ApiController
    {
        private readonly MtgCollectionDB db = new MtgCollectionDB();

        // GET: api/DeckApi
        public List<DeckViewModel> GetDecks()
        {
            var data = new List<DeckViewModel>();
            int userId = User.Identity.Name.GetHashCode();

            var decks = db.Decks.Where(d => d.OwnerId == userId);

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

                List<CardsViewModel> _mainDeck = new List<CardsViewModel>();
                List<CardsViewModel> _sideboard = new List<CardsViewModel>();

                foreach (DeckEntry entry in d.DeckEntries)
                {
                    var card = db.Cards.Find(entry.DeckEntryCardId);
                    var quantity = entry.Quantity;
                    
                    _mainDeck.Add(new CardsViewModel { Card = card, Quantity = quantity,Rating = 0 });
                }

                foreach (SideboardEntry entry in d.SiedeboardEntries)
                {
                    var card = db.Cards.Find(entry.SideboardEntryCardId);
                    var quantity = entry.Quantity;

                    _sideboard.Add(new CardsViewModel { Card = card, Quantity = quantity, Rating = 0 });
                }

                data.Add(new DeckViewModel { Deck = d, Rating = ratingsVal, mainDeck = _mainDeck , sideboard = _sideboard});
            }

            return data;
        }

        // GET: api/RatingApi/5
        [ResponseType(typeof(DeckViewModel))]
        [HttpGet]
        public DeckViewModel GetDeck(int id)
        {
            DeckViewModel data = new DeckViewModel();
            data.Deck = db.Decks.Find(id);

            var ratings = db.DeckRatings.Where(r => r.RatingDeckId == id);
            int ratingsVal = 0;

            if (ratings.Count() > 0)
            {
                foreach (DeckRating r in ratings)
                {
                    ratingsVal += r.RatingValue;
                }

                ratingsVal = ratingsVal / ratings.Count();
            }

            data.Rating = ratingsVal; 

            return data;
        }


        // POST: api/DeckApi
        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult AddDeck(Deck data)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("User is not logged in");
            }
            Deck deck = null;
            int userId = User.Identity.Name.GetHashCode();

            deck = new Deck();

            deck.OwnerId = userId;
            deck.DeckName = data.DeckName;
            deck.DeckDesc = data.DeckDesc;

            //Success
            db.Decks.Add(deck);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deck.DeckId }, deck);
        }

    }
}
