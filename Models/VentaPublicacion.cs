using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MercadoVentasTP.Models
{
    public class VentaPublicacion
    {
        public int Id { get; set; }

        public int IdVenta { get; set; }

        public int IdPublicacion { get; set; }

        public int Cantidad { get; set; }       

        public int Monto { get; set; }

        public String NombrePublicacion { get; set; }

    }
}