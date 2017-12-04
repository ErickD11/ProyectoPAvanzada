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
    public class TandasService
    {
        public List<Tanda> Tandas()
        {
            SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conexion.Open();
            string sql = "select * from Tandas where AsientosDisponibles > 0";
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            List<Tanda> listaTandas = new List<Tanda>();
            adp.Fill(datos, "Tanda");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                var newTanda = new Tanda()
                {
                    idTanda = Int32.Parse(row[0].ToString()),
                    Peliculas_idPelicula = Int32.Parse(row[1].ToString()),
                    Salas_idSala = Int32.Parse(row[2].ToString()),
                    Fecha = Convert.ToDateTime(row[3].ToString().Substring(0, 10) + " " + row[4].ToString()),
                    AsientosDisponibles = Int32.Parse(row[5].ToString()),
                    PrecioAdulto = Int32.Parse(row[6].ToString()),
                    PrecioNino = Int32.Parse(row[7].ToString()),
                    PrecioAdultoMayor = Int32.Parse(row[8].ToString())
                };
                listaTandas.Add(newTanda);
            }
            conexion.Close();
            return listaTandas;
        }
    }
}