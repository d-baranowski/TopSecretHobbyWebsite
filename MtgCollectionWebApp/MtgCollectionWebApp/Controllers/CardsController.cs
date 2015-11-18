using System.Web.Mvc;

namespace MtgCollectionWebApp.Controllers
{
    [Authorize]
    public class CardsController : Controller
    {
        // GET: Cards
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeckList()
        {
            return PartialView("~/Views/Cards/_deckList.cshtml");
        }

        public ActionResult CreateDeck()
        {
            return PartialView("~/Views/Cards/_createDeck.cshtml");
        }

        public ActionResult DeckBox()
        {
            return PartialView("~/Views/Cards/_deckBox.cshtml");
        }
    }
}


