using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        //biztosítjuk az adatbázis hozzáférést a vezérlőnek
        TodoAppContext db = new TodoAppContext();

        public ActionResult Tesztoldal()
        {
            return View();
        }


        // GET: Index
        public ActionResult Index()
        {
            var bevasarloLista = db.Feladatok.ToList();
            //Az így előállított adatokat (==model) átadjuk a nézetnek
            return View(bevasarloLista);
        }

        /// <summary>
        /// Új elem hozzáadásához a beviteli képernyő megjelenítése
        /// Egy GET kérés fogja keresni
        /// </summary>
        /// <returns></returns>
        [HttpGet] //Csak GET kérésre reagál
        public ActionResult Add()
        {
            //modell létrehozása és kiküldése a felületre (=nézet)
            var model = new Feladat();
            return View(model);
        }


        /// <summary>
        /// A felhasználó által bevitt adatok ellenőrzése és rögzítése
        /// Egy POST kérés fogja keresni
        /// </summary>
        /// <returns></returns>
        [HttpPost] //csak POST kérésre reagál
        public ActionResult Add(Feladat feladat)
        {

            //adatok ellenőrzése
            if (!ModelState.IsValid)
            { //az adatok nincsenek rendben: vissza kell küldeni őket módosításra
                return View(feladat);
            }

            //Az adatok rendben vannak, akkor új elem felvitele
            //perzisztens adattárolás
            db.Feladatok.Add(feladat);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Feladatok.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Feladat feladat)
        {
            if (!ModelState.IsValid)
            {
                return View(feladat);
            }

            var model = db.Feladatok.Find(feladat.Id);

            model.Megnevezes = feladat.Megnevezes;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = db.Feladatok.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Feladat feladat)
        {
            //nincs szükség validálásra, mert csak az id utazik fel, nem a teljes model

            var model = db.Feladatok.Find(feladat.Id);
            db.Feladatok.Remove(model);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}