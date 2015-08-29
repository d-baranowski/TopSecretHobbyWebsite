using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MtgCollectionWebApp.Models;
using System.Threading.Tasks;

namespace MtgCollectionWebApp.Controllers
{
    public class CardsController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: Cards
        [HttpGet]
        public ActionResult Index()
        {
            var model =  GetCardsByName("");
            return View(model);
        }

        [HttpGet]
        public ActionResult GetCardsByName(string q)
        {
            string b = q;
            var model = GetFullAndPartialViewModel(b);
          
            return PartialView("_DisplayCards",model);
        }

        private IEnumerable<CardsViewModel> GetFullAndPartialViewModel(string q)
        {
            var cardsAll = db.Cards.Where(a => a.CardName.Contains(q));
            var cardsViewModel = new List<CardsViewModel>();
            
            foreach (Card i in cardsAll)
            {
                var b = new CardsViewModel();
                b.Card = i;
                b.Quantity = getQuantityOrZero(i.CardId);
                cardsViewModel.Add(b);
                   
            }

            return cardsViewModel;
        }

        private int getQuantityOrZero(int cardId)
        {
            int q = 0;
            var collection = db.Collections.Find(User.Identity.Name.GetHashCode());
           
            if (collection.CollectionEntries != null)
            {
                var entries = collection.CollectionEntries.Where(d => d.CollectionEntryCardId.Equals(cardId));
                if (entries.Count() > 0) {
                   q = entries.First().Quantity;
                }  
            }
            
            return q;
        }

        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


