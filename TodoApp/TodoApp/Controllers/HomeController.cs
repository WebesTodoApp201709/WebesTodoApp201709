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

            var bevasarloLista = new List<string>();
            bevasarloLista.Add("Hagyma");
            bevasarloLista.Add("Pirospaprika");
            bevasarloLista.Add("Olaj");
            bevasarloLista.Add("Marhahús");

            //Az így előállított adatokat (==model) átadjuk a nézetnek
            return View(bevasarloLista);
        }

    }
}