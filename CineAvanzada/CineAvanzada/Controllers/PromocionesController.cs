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
        [Authorize]
        public ActionResult Promociones(string idUsuario)
        {
			var Prom = new PromocionService();
			var Tanda = new TandasService();
            var Promociones = new PromocionesService();
			var Model = new Promociones()
			{
				ListaPromociones = Prom.Promociones(),
				ListaTandas = Tanda.Tandas(),
                PuntosUsuario = Promociones.Puntos(idUsuario)
			};
			return View(Model);
        }
        
    }
}