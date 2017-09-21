using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private TodoAppContext db = new TodoAppContext();

        // GET: Todo
        public ActionResult Index()
        {
            return View(db.Feladatok.ToList());
        }

        // GET: Todo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feladat feladat = db.Feladatok.Find(id);
            if (feladat == null)
            {
                return HttpNotFound();
            }
            return View(feladat);
        }

        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Megnevezes")] Feladat feladat)
        {
            if (ModelState.IsValid)
            {
                db.Feladatok.Add(feladat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feladat);
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feladat feladat = db.Feladatok.Find(id);
            if (feladat == null)
            {
                return HttpNotFound();
            }
            return View(feladat);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Megnevezes")] Feladat feladat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feladat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feladat);
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feladat feladat = db.Feladatok.Find(id);
            if (feladat == null)
            {
                return HttpNotFound();
            }
            return View(feladat);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feladat feladat = db.Feladatok.Find(id);
            db.Feladatok.Remove(feladat);
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
