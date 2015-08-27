using MtgCollectionWebApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MtgCollectionWebApp.Controllers
{
    public class HomeController : Controller
    {
        private MtgCollectionDB db;

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated){
               // getOrCreateUserCollection();
            }
            

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

        private Collection getOrCreateUserCollection()
        {
            Collection collection;

            var test = db.Collections.Where(a => a.CollectionOwner.Equals(User.Identity.Name));
            if (test.Count() > 0)
            {
                collection = test.First();
            }
            else
            {
                collection = db.Collections.Add(new Collection { CollectionOwner = User.Identity.Name, CollectionName = "Hello" });
                db.SaveChanges();

            }

            return collection;
        }
    }
}