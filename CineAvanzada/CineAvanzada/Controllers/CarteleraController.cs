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
        public static Compra compra = new Compra();

        public ActionResult Cartelera()
        {
            var CarteleraService = new CarteleraService();
            var model = CarteleraService.Cartelera();
            return View(model);
        }

        public ActionResult CantidadEntradas(Tanda tanda)
        {
            if(tanda != null)
            {
                var CompraService = new CompraService();
                compra = CompraService.Compra(tanda);
                return View(compra);
            }
            else
            {
                return RedirectToAction("Cartelera");
            }
        }
        [HttpPost]
        public ActionResult CantidadEntradas(string EntradasAdulto, string EntradasNino, string EntradasAdultoMayor)
        {
            if (compra != null)
            {
                return View(compra);
            }
            else
            {
                return RedirectToAction("Cartelera");
            }
            
        }

        //public ActionResult Compra()
        //{
        //    if (compra == null)
        //    {
        //        return RedirectToAction("Cartelera");
        //    }
        //    else
        //    {
        //        return View(compra);
        //    }
            
        //}
    }
}