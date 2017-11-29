using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineAvanzada.Services;
using CineAvanzada.Models;

namespace CineAvanzada.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var CarteleraService = new CarteleraService();
			var model = CarteleraService.Cartelera();
			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Promociones()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

	}
}