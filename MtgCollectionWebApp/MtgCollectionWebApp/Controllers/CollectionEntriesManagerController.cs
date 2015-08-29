using System.Linq;
using System.Web.Mvc;
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    public class CollectionEntriesManagerController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: CollectionEntriesManager
        public ActionResult Index()
        {
            var collectionsEntries = db.CollectionsEntries;
            return View(collectionsEntries.ToList());
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
