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
    public class PagoService
    {
        public void RealizarPago(Compra compra)
        {
            using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connection"].ConnectionString))
            {
                foreach (Asiento asiento in compra.Asientos)
                {
                    using (SqlCommand cmd = new SqlCommand("Facturacion", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idFactura",newIdFactura(conexion)));
                        cmd.Parameters.Add(new SqlParameter("@MontoTotal",compra.PrecioTotal));
                        cmd.Parameters.Add(new SqlParameter("@CantidadAsientos",compra.TotalEntradas));
                        cmd.Parameters.Add(new SqlParameter("@CedulaPersona",compra.CedulaPersona));
                        cmd.Parameters.Add(new SqlParameter("@NombrePersona",compra.NombrePersona));
                        cmd.Parameters.Add(new SqlParameter("@idTanda",compra.Tanda.idTanda));
                        cmd.Parameters.Add(new SqlParameter("@idAsiento",asiento.idAsiento));
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                    }
                }  
            }    
        }

        private int newIdFactura (SqlConnection conexion)
        {
            int idFactura = 0;
            conexion.Open();
            string sql = "select isNull(MAX(idFactura),0) from Facturas";
            SqlDataAdapter adp = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            adp.Fill(datos, "idFactura");
            foreach (DataRow row in datos.Tables[0].Rows)
            {
                idFactura = Int32.Parse(row[0].ToString());
            }
            conexion.Close();
            idFactura++;
            return idFactura;
        } 
    }
}