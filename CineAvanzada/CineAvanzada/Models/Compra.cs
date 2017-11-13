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
        public List<Asiento> Asientos { get; set; }
    }
}