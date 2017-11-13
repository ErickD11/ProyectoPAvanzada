using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineAvanzada.Models
{
    public class Tanda
    {
        public int idTanda { get; set; }
        public int Peliculas_idPelicula { get; set; }
        public int Salas_idSala { get; set; }
        public DateTime Fecha { get; set; }
        public int AsientosDisponibles { get; set; }
    }
}