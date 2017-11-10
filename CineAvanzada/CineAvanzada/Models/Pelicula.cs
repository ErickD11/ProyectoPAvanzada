using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineAvanzada.Models
{
    public class Pelicula
    {
        public int idPelicula { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Director { get; set; }
        public int Duracion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string Sinopsis { get; set; }
    }
}