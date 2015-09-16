using MtgCollectionWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MtgCollectionWebApp.Controllers
{
    [RoutePrefix("api/DeckApi")]
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

                data.Add(new DeckViewModel { Deck = d, Rating = ratingsVal });
            }

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

        [HttpGet]
        [Route("getDeckCardViewModels/")]
        public List<DeckCardViewModel> getMainDeckViewModels(int id)
        {
            var list = new List<DeckCardViewModel>();
            var entries = db.DeckEntries.Where(e => e.DeckId == id && e.MainDeck == true);
            HashSet<int> cardIds = new HashSet<int>();

            foreach (var e in entries)
            {
                if (!cardIds.Contains(e.CardId))
                {
                    var _card = db.Cards.Find(e.CardId);
                    int _quantity = db.DeckEntries.Where(entry => entry.CardId == _card.CardId).Count();
                    list.Add(new DeckCardViewModel { card = _card, quantity = _quantity });
                    cardIds.Add(e.CardId);
                }
            }

            return list;
        }

        [HttpGet]
        [Route("getSideBoardViewModels/")]
        public List<DeckCardViewModel> getSideBoardViewModels(int id)
        {
            var list = new List<DeckCardViewModel>();
            var entries = db.DeckEntries.Where(e => e.DeckId == id && e.MainDeck == false);
            HashSet<int> cardIds = new HashSet<int>();

            foreach (var e in entries)
            {
                if (!cardIds.Contains(e.CardId))
                {
                    var _card = db.Cards.Find(e.CardId);
                    int _quantity = db.DeckEntries.Where(entry => entry.CardId == _card.CardId).Count();
                    list.Add(new DeckCardViewModel { card = _card, quantity = _quantity });
                    cardIds.Add(e.CardId);
                }
            }

            return list;
        }


        [HttpPost]
        [Route("addCardToMainDeck/")]
        public void AddCardToMainDeck(DeckCardEntryModel model)
        {
            for (int i = 0; i < model.quantity; i++)
            {
                db.DeckEntries.Add(new DeckEntry { CardId = model.cardId, DeckId = model.deckId, MainDeck = true });
            }
            db.SaveChanges();
        }

        [HttpPost]
        [Route("addCardToSideboard/")]
        public void AddCardToSideboard(DeckCardEntryModel model)
        {
            for (int i = 0; i < model.quantity; i++)
            {
                db.DeckEntries.Add(new DeckEntry { CardId = model.cardId, DeckId = model.deckId, MainDeck = false });
            }
            db.SaveChanges();
        }

        [HttpGet]
        [Route("getExample")]
        public DeckCardEntryModel getExample()
        {
            var model = new DeckCardEntryModel();
            model.cardId = 1;
            model.deckId = 1;
            model.quantity = 1;
            return model;
        }
    }
}
