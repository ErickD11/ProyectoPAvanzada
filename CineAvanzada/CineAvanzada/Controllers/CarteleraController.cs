using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineAvanzada.Services;

namespace CineAvanzada.Controllers
{
    public class CarteleraController : Controller
    {
        // GET: Cartelera
        public ActionResult Cartelera()
        {
            var PeliculasService = new PeliculasService();
            var model = PeliculasService.Peliculas();
            return View(model);
        }
    }
}