using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CineAvanzada.Services
{
    public class PromocionesService
    {
        public int Puntos(string idUsuario)
        {
            SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            conexion.Open();
            string sql = "select * from UsuarioPuntos where UserID = '" + idUsuario + "'";
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            int Puntos = 0;
            adp.Fill(datos, "Puntos");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                Puntos = Int32.Parse(row[1].ToString());
            }
            conexion.Close();
            return Puntos;
        }
    }
}