using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace MercadoVentasTP.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        
        public int IdUsuario { get; set; }
        
        public DateTime Fecha { get; set; }

        [Display(Name = "Monto de la operación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float Monto { get; set; }

        [Display(Name = "Operación a realizar")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Operacion { get; set; }


        public Movimiento()
        {
            Fecha = DateTime.Now;
        }





    }
}