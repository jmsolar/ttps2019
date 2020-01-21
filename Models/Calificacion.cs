using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoVentasTP.Models
{
    public class Calificacion
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Display(Name = "Puntaje")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Puntaje { get; set; }
        // El puntaje no hace falta validarlo. Por defecto es un RadioButton que va a tener siempre uno seleccionado

        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Comentario { get; set; }

        public int IdUsuarioCalificador { get; set; }

        public int IdPublicacion { get; set; }

        public int IdCompra { get; set; }

        public Calificacion()
        {
            Fecha = DateTime.Now;
        }
    }
}