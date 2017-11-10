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
    public class PeliculasService
    {
        public List<Pelicula> Peliculas()
        {
         
            SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            conexion.Open();
            string sql = "select * from Peliculas";
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            List<Pelicula> listaPeliculas = new List<Pelicula>();
            adp.Fill(datos, "Pelicula");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                var newPelicula = new Pelicula()
                {
                    idPelicula = Int32.Parse(row[0].ToString()),
                    Nombre = row[1].ToString(),
                    Genero = row[2].ToString(),
                    Director = row[3].ToString(),
                    Duracion = Int32.Parse(row[4].ToString()),
                    FechaEstreno = Convert.ToDateTime(row[5].ToString()),
                    Sinopsis = row[6].ToString()
                };
                listaPeliculas.Add(newPelicula);
            }
            conexion.Close();
            return listaPeliculas;
        }
    }
}