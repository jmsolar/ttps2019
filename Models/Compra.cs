using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoVentasTP.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }


        public DateTime Fecha { get; set; }

        public float Monto { get; set; }


        public Compra()
        {
            Fecha = DateTime.Now;
        }

        
    }
}