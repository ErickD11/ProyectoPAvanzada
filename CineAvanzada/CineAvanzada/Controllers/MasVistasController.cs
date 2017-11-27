using CineAvanzada.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineAvanzada.Controllers
{
    public class MasVistasController : Controller
    {
        // GET: MasVistas
        public ActionResult MasVistas()
        {
            var masVstas = new MasVistasService();
            var model = masVstas.MasVendidas();
            return View(model);
        }
    }
}