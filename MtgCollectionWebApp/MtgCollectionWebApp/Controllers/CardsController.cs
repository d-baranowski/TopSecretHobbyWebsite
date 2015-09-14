using System.Web.Mvc;
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    public class CardsController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: Cards
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            } else
            {
                return RedirectToAction("Login", "Account");
            } 
        }

        // GET: Cards
        public ActionResult DeckBuilder()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult deckList()
        {
            return PartialView("~/Views/Cards/_deckList.cshtml");
        }

        public ActionResult createDeck()
        {
            return PartialView("~/Views/Cards/_createDeck.cshtml");
        }

        public ActionResult deckBox()
        {
            return PartialView("~/Views/Cards/_deckBox.cshtml");
        }
    }
}


