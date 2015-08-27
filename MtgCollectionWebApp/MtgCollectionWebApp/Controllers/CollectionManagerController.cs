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
    public class CollectionManagerController : Controller
    {
        private MtgCollectionDB db = new MtgCollectionDB();
        

        // GET: CollectionManager
        public async Task<ActionResult> Index()
        {
            var model = await GetEntryViewModel();
            return View(model);
        }

        private async Task<IEnumerable<EntryViewModel>> GetEntryViewModel()
        {
            Collection collection;
            IEnumerable<CollectionEntry> entries;
            List<EntryViewModel> entryViewModelList;
            var test = db.Collections.Where(a => a.CollectionOwner.Equals(User.Identity.Name));
            var entryViewModel = new EntryViewModel();

            collection = test.First();
            entries = collection.CollectionEntries;
            entryViewModelList = new List<EntryViewModel>();
            foreach (CollectionEntry e in entries)
            {

                entryViewModel.Card = await db.Cards.FindAsync(e.CollectionEntryCardId);
                entryViewModel.Quantity = e.Quantity;
                entryViewModelList.Add(entryViewModel);
                entryViewModel.collectionID = collection.CollectionId;
            }
            entryViewModelList.Add(entryViewModel);
            return entryViewModelList;
        }

        // GET: CollectionManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // GET: CollectionManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollectionManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionId,CollectionOwner,CollectionName")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collection);
        }

        // GET: CollectionManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: CollectionManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollectionId,CollectionOwner,CollectionName")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        // GET: CollectionManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: CollectionManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
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
