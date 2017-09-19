using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {

            var bevasarloLista = new List<Feladat>();
            bevasarloLista.Add(
                new Feladat
                {
                    Megnevezes = "Hagyma",
                    Elvegezve = true
                });

            bevasarloLista.Add(
                new Feladat
                {
                    Megnevezes = "Pirospaprika",
                    Elvegezve = true
                });

            bevasarloLista.Add(
                new Feladat
                {
                    Megnevezes = "Olaj",
                    Elvegezve = false
                });

            bevasarloLista.Add(
                new Feladat
                {
                    Megnevezes = "Marhahús",
                    Elvegezve = false
                }
            );

            if (Request.QueryString.AllKeys.Contains("Megnevezes"))
            {
                bevasarloLista.Add(
                    new Feladat
                    {
                        Megnevezes = Request.QueryString["Megnevezes"],
                        Elvegezve = false
                    }
                );
            }


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
        public ActionResult Add(string megnevezes)
        {
            //todo: perzisztens adattárolás

            //adatok ellenőrzése
            //ha nem jó, akkor újra bekérni
            //ha rendben vannak, akkor új elem felvitele


            return RedirectToAction("Index");
        }

    }
}