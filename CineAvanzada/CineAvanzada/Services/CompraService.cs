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
    public class CompraService
    {
        public Compra Compra(Tanda tanda)
        {
            SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            conexion.Open();
            string sql = "select * from Peliculas where idPelicula = " + tanda.Peliculas_idPelicula;
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            var newPelicula = new Pelicula();
            adp.Fill(datos, "Pelicula");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                newPelicula = new Pelicula()
                {
                    idPelicula = Int32.Parse(row[0].ToString()),
                    Nombre = row[1].ToString(),
                    Genero = row[2].ToString(),
                    Director = row[3].ToString(),
                    Duracion = Int32.Parse(row[4].ToString()),
                    FechaEstreno = Convert.ToDateTime(row[5].ToString()),
                    Sinopsis = row[6].ToString(),
                    Imagen = row[7].ToString()
                };
            }
            conexion.Close();
            return CompraAsientos(tanda, newPelicula, conexion);
        }

        public Compra CompraAsientos(Tanda tanda, Pelicula pelicula,SqlConnection conexion)
        {
            conexion.Open();
            string sql = "select * from DetalleFactura where Tandas_idTanda = " + tanda.idTanda;
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            List<Asiento> Asientos = new List<Asiento>();
            adp.Fill(datos, "Asiento");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                var newAsiento = new Asiento()
                {
                    idAsiento = Int32.Parse(row[2].ToString())
                };
                Asientos.Add(newAsiento);
            }
            conexion.Close();
            Compra Compra = new Compra()
            {
                Tanda = tanda,
                Pelicula = pelicula,
                EntradasAdulto = 0,
                EntradasNino = 0,
                EntradasAdultoMayor = 0,
                TotalEntradas = 0,
                PrecioTotal = 0,
                AsientosReservados = Asientos
            };
            return Compra;
        }

        public Compra CompraEntradas(Compra compra, int entradasAdulto, int entradasNino, int entradasAdultoMayor)
        {
            compra.EntradasAdulto = entradasAdulto;
            compra.EntradasNino = entradasNino;
            compra.EntradasAdultoMayor = entradasAdultoMayor;
            return compra;
        }

        public Compra CompraEntradas(Compra compra, List<int> asientos)
        {
            List<Asiento> MisAsientos = new List<Asiento>();
            foreach (int asiento in asientos)
            {
                Asiento newAsiento = new Asiento()
                {
                    idAsiento = asiento
                };
                MisAsientos.Add(newAsiento);
            }
            compra.Asientos = MisAsientos;
            return compra;
        }
    }
}