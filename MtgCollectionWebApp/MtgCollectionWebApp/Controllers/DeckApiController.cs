using MtgCollectionWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace MtgCollectionWebApp.Controllers
{
    [RoutePrefix("api/DeckApi")]
    [Authorize]
    public class DeckApiController : ApiController
    {
        private readonly MtgCollectionDB _db = new MtgCollectionDB();

        // GET: api/DeckApi
        public List<DeckViewModel> GetDecks()
        {
            var data = new List<DeckViewModel>();
            var userId = User.Identity.Name.GetHashCode();

            var decks = _db.Decks.Where(d => d.OwnerId == userId);

            foreach (var deck in decks)
            {
                var ratings = _db.DeckRatings.Where(r => r.RatingDeckId == deck.DeckId);
                var ratingsVal = 0;

                if (ratings.Any())
                {
                    foreach (var rating in ratings)
                    {
                        ratingsVal += rating.RatingValue;
                    }

                    ratingsVal = ratingsVal / ratings.Count();
                }

                data.Add(new DeckViewModel { Deck = deck, Rating = ratingsVal });
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
            var userId = User.Identity.Name.GetHashCode();

            var deck = new Deck
            {
                OwnerId = userId,
                DeckName = data.DeckName,
                DeckDesc = data.DeckDesc
            };


            //Success
            _db.Decks.Add(deck);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = deck.DeckId }, deck);
        }

        [HttpGet]
        [Route("getDeckCardViewModels/")]
        public List<DeckCardViewModel> GetMainDeckViewModels(int id)
        {
            var list = new List<DeckCardViewModel>();
            var entries = _db.DeckEntries.Where(e => e.DeckId == id && e.MainDeck);
            var cardIds = new HashSet<int>();

            foreach (var e in entries)
            {
                if (cardIds.Contains(e.CardId)) continue;
                var card = _db.Cards.Find(e.CardId);
                var quantity = entries.Count(entry => entry.CardId == card.CardId);
                list.Add(new DeckCardViewModel { card = card, quantity = quantity });
                cardIds.Add(e.CardId);
            }

            return list;
        }

        [HttpGet]
        [Route("getSideBoardViewModels/")]
        public List<DeckCardViewModel> GetSideBoardViewModels(int id)
        {
            var list = new List<DeckCardViewModel>();
            var entries = _db.DeckEntries.Where(e => e.DeckId == id && e.MainDeck == false);
            var cardIds = new HashSet<int>();

            foreach (var e in entries)
            {
                if (cardIds.Contains(e.CardId)) continue;
                var card = _db.Cards.Find(e.CardId);
                var quantity = entries.Count(entry => entry.CardId == card.CardId);
                list.Add(new DeckCardViewModel { card = card, quantity = quantity });
                cardIds.Add(e.CardId);
            }

            return list;
        }


        [HttpPost]
        [Route("addCardToMainDeck/")]
        public void AddCardToMainDeck(DeckCardEntryModel model)
        {
            for (var i = 0; i < model.quantity; i++)
            {
                _db.DeckEntries.Add(new DeckEntry { CardId = model.cardId, DeckId = model.deckId, MainDeck = true });
            }
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("addCardToSideboard/")]
        public void AddCardToSideboard(DeckCardEntryModel model)
        {
            for (var i = 0; i < model.quantity; i++)
            {
                _db.DeckEntries.Add(new DeckEntry { CardId = model.cardId, DeckId = model.deckId, MainDeck = false });
            }
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("deleteCardFromMainDeck")]
        public void DeleteCardFromMainDeck(DeckCardEntryModel model)
        {
            for (var i = 0; i < model.quantity; i++)
            {
                var entity = _db.DeckEntries.First(e => e.CardId == model.cardId && e.MainDeck);
                _db.DeckEntries.Remove(entity);
            }
            _db.SaveChanges();
        }

        [HttpPost]
        [Route("deleteCardFromSideboard")]
        public void DeleteCardFromSideboard(DeckCardEntryModel model)
        {
            for (var i = 0; i < model.quantity; i++)
            {
                var entity = _db.DeckEntries.First(e => e.CardId == model.cardId && e.MainDeck == false);
                _db.DeckEntries.Remove(entity);
            }
            _db.SaveChanges();
        }

        [HttpGet]
        [Route("getExample")]
        public DeckCardEntryModel GetExample()
        {
            var model = new DeckCardEntryModel
            {
                cardId = 1,
                deckId = 1,
                quantity = 1
            };
            return model;
        }
    }
}
