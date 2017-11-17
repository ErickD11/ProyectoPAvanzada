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
                int CantidadAdultos = Int32.Parse(EntradasAdulto);
                int CantidadNinos = 0;
                int CantidadAdultosMayores = Int32.Parse(EntradasAdultoMayor);
                if (EntradasNino != null)
                {
                    CantidadNinos = Int32.Parse(EntradasNino);
                }
                int totalEntradas = CantidadAdultos + CantidadNinos + CantidadAdultosMayores;
                if (totalEntradas > compra.Tanda.AsientosDisponibles)
                {
                    return View(compra);
                }
                else
                {
                    int precioTotal = (CantidadAdultos * compra.Tanda.PrecioAdulto) + (CantidadNinos * compra.Tanda.PrecioNino) + (CantidadAdultosMayores * compra.Tanda.PrecioAdultoMayor);
                    compra.TotalEntradas = totalEntradas;
                    compra.EntradasAdulto = CantidadAdultos;
                    compra.EntradasNino = CantidadNinos;
                    compra.EntradasAdultoMayor = CantidadAdultosMayores;
                    compra.PrecioTotal = precioTotal;
                    return RedirectToAction("Compra");
                }
            }
            else
            {
                return RedirectToAction("Cartelera");
            }
            
        }

        public ActionResult Compra()
        {
            if (compra == null)
            {
                return RedirectToAction("Cartelera");
            }
            else
            {
                return View(compra);
            }
        }
    }
}