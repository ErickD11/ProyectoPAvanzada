using CineAvanzada.Models;
using CineAvanzada.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineAvanzada.Controllers
{
    public class PromocionesController : Controller
    {
        // GET: Promociones
        public ActionResult Promociones()
        {

			var Prom = new PromocionService();
			var Tanda = new TandasService();
			var Model = new Promociones()
			{
				ListaPromociones = Prom.Promociones(),
				ListaTandas = Tanda.Tandas()
			};
			return View(Model);
        }

    }
}