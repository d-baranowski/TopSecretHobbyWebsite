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
            return View();
        }
    }
}


