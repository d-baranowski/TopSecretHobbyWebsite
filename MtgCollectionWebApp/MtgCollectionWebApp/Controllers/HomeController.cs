using MtgCollectionWebApp.Models;
using System.Web.Mvc;

namespace MtgCollectionWebApp.Controllers
{
    public class HomeController : Controller
    {
        
        private MtgCollectionDB db = new MtgCollectionDB();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (db.Collections.Find(User.Identity.Name.GetHashCode()) == null) { 
                    var a = db.Collections.Add(new Collection {CollectionId = User.Identity.Name.GetHashCode()});
                    db.SaveChanges();
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