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
        public void RealizarPago(Compra compra, string usuario)
        {
            if (compra.Promocion)
            {
                QuitarPuntos(usuario, compra.PuntosMenos);
            }
            using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                foreach (Asiento asiento in compra.Asientos)
                {
                    using (SqlCommand cmd = new SqlCommand("Facturacion", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idFactura", newIdFactura(conexion)));
                        cmd.Parameters.Add(new SqlParameter("@MontoTotal", compra.PrecioTotal));
                        cmd.Parameters.Add(new SqlParameter("@CantidadAsientos", compra.TotalEntradas));
                        cmd.Parameters.Add(new SqlParameter("@CedulaPersona", compra.CedulaPersona));
                        cmd.Parameters.Add(new SqlParameter("@NombrePersona", compra.NombrePersona));
                        cmd.Parameters.Add(new SqlParameter("@idTanda", compra.Tanda.idTanda));
                        cmd.Parameters.Add(new SqlParameter("@idAsiento", asiento.idAsiento));
                        cmd.Parameters.Add(new SqlParameter("@idUser", usuario));
                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                        usuario = "Ninguno";
                    }
                }
            }
        }

        private int newIdFactura(SqlConnection conexion)
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

        private void QuitarPuntos(string idUsuario, int puntosMenos)
        {
            using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("QuitarPuntos", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@userId", idUsuario));
                    cmd.Parameters.Add(new SqlParameter("@puntos", puntosMenos));
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
            }
        }
    }
}