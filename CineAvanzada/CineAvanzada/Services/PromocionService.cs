using CineAvanzada.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CineAvanzada.Services
{
	public class PromocionService
	{

		public List<Promocion> Promociones()
		{
			SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			conexion.Open();
			string sql = "select * from Promociones";
			SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
			DataSet datos = new DataSet();
			List<Promocion> listaPromociones = new List<Promocion>();
			adp.Fill(datos, "Promocion");
			foreach (DataRow row in datos.Tables[0].Rows)
			{
				var newPromocion = new Promocion()
				{
					idPromocion = Int32.Parse(row[0].ToString()),
					Descuento = Int32.Parse(row[1].ToString()),
					Tandas_idTanda = Int32.Parse(row[2].ToString()),
					PuntosRequeridos = Int32.Parse(row[3].ToString()),
					Nombre = row[4].ToString(),
					Descripcion = row[5].ToString(),
					Imagen = row[6].ToString(),
				};
				listaPromociones.Add(newPromocion);
			}
			conexion.Close();
			return listaPromociones;
		}
	

}
}