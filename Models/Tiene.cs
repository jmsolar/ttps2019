using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MercadoVentasTP.Models
{
    public class Tiene
    {
        public int Id { get; set; }

        public int IdCompra { get; set; }

        public int IdPublicacion { get; set; }

        public int Cantidad { get; set; }

        public string Estado { get; set; }

        public float Monto { get; set; }

        public String NombrePublicacion { get; set; }

        public Tiene()
        {
            Estado = "Sin calificar";
        }

    }

}