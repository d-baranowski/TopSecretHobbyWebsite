﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MtgCollectionWebApp.Models;

namespace MtgCollectionWebApp.Controllers
{
    public class CardsController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: Cards
        public ActionResult Index()
        {
            return View(db.Cards.ToList());
        }

        public ActionResult SearchByName(string q)
        {
            var cards = db.Cards
            .Where(a => a.CardName.Contains(q));

            return View(cards);
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
