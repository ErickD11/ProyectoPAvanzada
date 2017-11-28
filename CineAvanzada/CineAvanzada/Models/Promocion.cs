using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineAvanzada.Models
{
	public class Promocion
	{
		public int idPromocion { get; set; }
		public int Descuento { get; set; }
		public int Tandas_idTanda { get; set; }
		public int PuntosRequeridos { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public string Imagen { get; set; }
	}
}