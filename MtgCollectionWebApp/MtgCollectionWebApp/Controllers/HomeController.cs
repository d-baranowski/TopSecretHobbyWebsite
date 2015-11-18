using MtgCollectionWebApp.Models;
using System.Web.Mvc;

namespace MtgCollectionWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private readonly MtgCollectionDB _db = new MtgCollectionDB();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_db.Collections.Find(User.Identity.Name.GetHashCode()) == null)
                {
                    _db.Collections.Add(new Collection {CollectionId = User.Identity.Name.GetHashCode()});
                    _db.SaveChanges();
                }
            }
            ViewBag.Message = User.Identity.Name.GetHashCode();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}