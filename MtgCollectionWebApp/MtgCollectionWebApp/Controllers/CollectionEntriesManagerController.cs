using System;
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
    public class CollectionEntriesManagerController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();

        // GET: CollectionEntriesManager
        public ActionResult Index()
        {
            var collectionsEntries = db.CollectionsEntries.Include(c => c.CollectionEntryCard);
            return View(collectionsEntries.ToList());
        }

        // GET: CollectionEntriesManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry == null)
            {
                return HttpNotFound();
            }
            return View(collectionEntry);
        }

        // GET: CollectionEntriesManager/Create
        public ActionResult Create()
        {
            ViewBag.CollectionEntryCardId = new SelectList(db.Cards, "CardId", "Name");
            return View();
        }

        // POST: CollectionEntriesManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionEntryId,Quantity,CollectionEntryCardId,CollectionId")] CollectionEntry collectionEntry)
        {
            if (ModelState.IsValid)
            {
                db.CollectionsEntries.Add(collectionEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionEntryCardId = new SelectList(db.Cards, "CardId", "Name", collectionEntry.CollectionEntryCardId);
            return View(collectionEntry);
        }

        // GET: CollectionEntriesManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollectionEntryCardId = new SelectList(db.Cards, "CardId", "Name", collectionEntry.CollectionEntryCardId);
            return View(collectionEntry);
        }

        // POST: CollectionEntriesManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionEntryId,Quantity,CollectionEntryCardId,CollectionId")] CollectionEntry collectionEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collectionEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionEntryCardId = new SelectList(db.Cards, "CardId", "Name", collectionEntry.CollectionEntryCardId);
            return View(collectionEntry);
        }

        // GET: CollectionEntriesManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            if (collectionEntry == null)
            {
                return HttpNotFound();
            }
            return View(collectionEntry);
        }

        // POST: CollectionEntriesManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollectionEntry collectionEntry = db.CollectionsEntries.Find(id);
            db.CollectionsEntries.Remove(collectionEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
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
