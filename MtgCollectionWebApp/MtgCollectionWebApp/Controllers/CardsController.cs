﻿using System;
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
        public async Task<ActionResult> Index()
        {
            var model = await GetCardsByName("");
            return View(model);
        }

        public List<Card> GetCardList(string q)
        {
            return (List<Card>)db.Cards.Where(a => a.CardName.Contains(q));
        }


        public ActionResult SearchByName(string q)
        {
            if (q.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cards = db.Cards.Where(a => a.CardName.Contains(q));

            return PartialView("_DisplayCards", cards);
        }

        [HttpGet]
        public async Task<ActionResult> GetCardsByName(string q)
        {
            string b = q;
            var model = await GetFullAndPartialViewModel(b);
          
            return PartialView("_DisplayCards",model);
        }

        private async Task<CardsViewModel> GetFullAndPartialViewModel(string q)
        {
            var c = db.Cards
            .Where(a => a.CardName.Contains(q));
            var cardsViewModel = new CardsViewModel();
            cardsViewModel.Cards = c;
            return cardsViewModel;
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

