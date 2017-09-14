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

            //Az így előállított adatokat (==model) átadjuk a nézetnek
            return View(bevasarloLista);
        }

    }
}