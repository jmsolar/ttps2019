using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MercadoVentasTP.Models
{
    public class PublicacionEnCarrito
    {

        public Carrito carrito { get; set; }

        public Publicacion publicacion { get; set; }
        
        public PublicacionEnCarrito(Carrito c, Publicacion p)
        {
            this.carrito = c;
            this.publicacion = p;
        }


    }
}