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
    public class MasVistasService
    {
        public List<MasVendida> MasVendidas()
        {
            List<MasVendida> masVendiadas = MasVendiasBase();
            List<MasVendida> listaAux = new List<MasVendida>();
            foreach (var MasVendida in masVendiadas)
            {
                listaAux.Add(MasVendida);
                if (listaAux.Count > 3)
                {
                    MasVendida menorVendida = null;
                    foreach (var masVendida in listaAux)
                    {
                        if (menorVendida == null)
                        {
                            menorVendida = masVendida;
                        }
                        else
                        {
                            if (masVendida.EntradasVendidas < menorVendida.EntradasVendidas)
                            {
                                menorVendida = masVendida;
                            }
                        }
                    }
                    listaAux.Remove(menorVendida);
                }
            }
            return ordenarLista(listaAux);
        }

        private List<MasVendida> MasVendiasBase()
        {
            SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString);
            conexion.Open();
            string sql = "select p.idPelicula,p.Nombre,p.Genero,p.Director,p.Duracion,p.FechaEstreno,p.Sinopsis,p.Imagen,count(d.Asientos_idAsiento)"
                        + " from Peliculas p, Tandas t, DetalleFactura d where d.Tandas_idTanda = t.idTanda AND t.Peliculas_idPelicula = p.idPelicula"
                        + " group by idPelicula,Nombre,Director,Duracion,FechaEstreno,Genero,Imagen,Sinopsis";
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            List<MasVendida> masVendidas = new List<MasVendida>();
            adp.Fill(datos, "MasVendida");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                var masVendida = new MasVendida()
                {
                    idPelicula = Int32.Parse(row[0].ToString()),
                    Nombre = row[1].ToString(),
                    Genero = row[2].ToString(),
                    Director = row[3].ToString(),
                    Duracion = Int32.Parse(row[4].ToString()),
                    FechaEstreno = Convert.ToDateTime(row[5].ToString()),
                    Sinopsis = row[6].ToString(),
                    Imagen = row[7].ToString(),
                    EntradasVendidas = Int32.Parse(row[8].ToString())
                };
                masVendidas.Add(masVendida);
            }
            conexion.Close();
            return masVendidas;
        }
        private List<MasVendida> ordenarLista(List<MasVendida> lista)
        {
            var primera = lista[0];
            var segunda = lista[1];
            var tercera = lista[2];
            if (segunda.EntradasVendidas > primera.EntradasVendidas)
            {
                lista[0] = segunda;
                lista[1] = primera;
            }
            if(lista[0].EntradasVendidas < tercera.EntradasVendidas)
            {
                lista[2] = lista[1];
                lista[1] = lista[0];
                lista[0] = tercera;
            }
            else
            {
                if (lista[1].EntradasVendidas < tercera.EntradasVendidas)
                {
                    lista[2] = lista[1];
                    lista[1] = tercera;
                }
            }
            return lista;
        }
    }
}