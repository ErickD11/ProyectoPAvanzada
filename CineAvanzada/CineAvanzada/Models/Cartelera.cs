using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineAvanzada.Models
{
    public class Cartelera
    {
        public List<Pelicula> Peliculas { get; set; }
        public List<Tanda> Tandas { get; set; }
    }
}