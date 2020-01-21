using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MercadoVentasTP.Models
{
    public class Venta
    {
        public int Id { get; set; }

        public int IdVendedor { get; set; }

        public int IdVentaProducto { get; set; }

        public DateTime Fecha { get; set; }

        public float Monto { get; set; }


        public Venta()
        {
            Fecha = DateTime.Now;
        }



    }
}