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
            compra = new Compra();
            var CarteleraService = new CarteleraService();
            var model = CarteleraService.Cartelera();
            return View(model);
        }

        public ActionResult CantidadEntradas(Tanda tanda, int descuento, bool Promocion, int PuntosMenos)
        {
            if (tanda != null)
            {
                var CompraService = new CompraService();
                compra = CompraService.Compra(tanda, descuento, Promocion, PuntosMenos);
                return View(compra);
            }
            else
            {
                return RedirectToAction("Cartelera");
            }
        }

        [HttpPost]
        public ActionResult CantidadEntradas(string EntradasAdulto, string EntradasNino, string EntradasAdultoMayor, string Total)
        {
            if (compra != null && (EntradasAdulto != null || EntradasNino != null || EntradasAdultoMayor != null))
            {
                int CantidadAdultos = 0;
                int CantidadNinos = 0;
                int CantidadAdultosMayores = 0;
                if (EntradasAdulto != null && EntradasAdulto != "")
                {
                    CantidadAdultos = Int32.Parse(EntradasAdulto);
                }
                if (EntradasNino != null && EntradasNino != "")
                {
                    CantidadNinos = Int32.Parse(EntradasNino);
                }
                if (EntradasAdultoMayor != null && EntradasAdultoMayor != "")
                {
                    CantidadAdultosMayores = Int32.Parse(EntradasAdultoMayor);
                }
                int totalEntradas = CantidadAdultos + CantidadNinos + CantidadAdultosMayores;
                if (totalEntradas > compra.Tanda.AsientosDisponibles)
                {
                    return View(compra);
                }
                else
                {
                    int precioTotal = ((CantidadAdultos * compra.Tanda.PrecioAdulto) + (CantidadNinos * compra.Tanda.PrecioNino) + (CantidadAdultosMayores * compra.Tanda.PrecioAdultoMayor)) - (compra.Descuento * totalEntradas);
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

        public ActionResult Pago(int[] asientos = null)
        {
            if (compra == null)
            {
                return RedirectToAction("Cartelera");
            }
            else
            {
                if (asientos == null)
                {
                    return RedirectToAction("Compra");
                }
                else
                {
                    foreach (int asiento in asientos)
                    {
                        Asiento newAsiento = new Asiento()
                        {
                            idAsiento = asiento
                        };
                        if (compra.Asientos == null)
                        {
                            compra.Asientos = new List<Asiento>();
                        }
                        compra.Asientos.Add(newAsiento);
                    }
                    return View(compra);
                }
            }
        }

        public ActionResult Factura(string nombre, string cedula, string usuario)
        {
            if (compra == null || nombre == null || cedula == null)
            {
                return RedirectToAction("Cartelera");
            }
            else
            {
                compra.CedulaPersona = cedula;
                compra.NombrePersona = nombre;
                var Pago = new PagoService();
                Pago.RealizarPago(compra, usuario);
                return View(compra);
            }
        }
    }
}