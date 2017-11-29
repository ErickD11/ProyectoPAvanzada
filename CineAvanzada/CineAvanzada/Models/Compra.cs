using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineAvanzada.Models
{
    public class Compra
    {
        public Tanda Tanda { get; set; }
        public Pelicula Pelicula { get; set; }
        public int EntradasAdulto { get; set; }
        public int EntradasNino { get; set; }
        public int EntradasAdultoMayor { get; set; }
        public int TotalEntradas { get; set; }
        public int PrecioTotal { get; set; }
        public List<Asiento> AsientosReservados { get; set; }
        public List<Asiento> Asientos { get; set; }
        public string CedulaPersona { get; set; }
        public string NombrePersona { get; set; }
    }
}