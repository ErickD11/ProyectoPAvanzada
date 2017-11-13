using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CineAvanzada.Services;
using CineAvanzada.Models;

namespace CineAvanzada.Services
{
    public class CarteleraService
    {
        public Cartelera Cartelera()
        {
            var PeliculasService = new PeliculasService();
            var TandasService = new TandasService();
            var Cartelera = new Cartelera()
            {
                Peliculas = PeliculasService.Peliculas(),
                Tandas = TandasService.Tandas()
            };
            return Cartelera;
        }
    }
}