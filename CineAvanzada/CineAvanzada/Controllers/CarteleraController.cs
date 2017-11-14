using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineAvanzada.Services;
using CineAvanzada.Models;

namespace CineAvanzada.Controllers
{
    public class CarteleraController : Controller
    {
        public ActionResult Cartelera()
        {
            var CarteleraService = new CarteleraService();
            var model = CarteleraService.Cartelera();
            return View(model);
        }

        public ActionResult Compra(Tanda tanda)
        {
            if (tanda == null)
            {
                return RedirectToAction("Cartelera");
            }
            else
            {
                var CompraService = new CompraService();
                var model = CompraService.Compra(tanda);
                return View(model);
            }
            
        }
    }
}