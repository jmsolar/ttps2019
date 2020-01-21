using System.ComponentModel.DataAnnotations;

namespace MercadoVentasTP.Models
{
    public class Publicacion
    {
        public int Id { get; set; }

        public string IdCategoria { get; set; }

        public int IdProducto { get; set; }

        public int IdUsuario { get; set; }


        [Display(Name = "Título")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Titulo { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Descripcion { get; set; }

        [Display(Name = "Stock disponible")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Stock { get; set; }

        [Display(Name = "Precio base")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float Precio { get; set; }

        [Display(Name = "Precio mínimo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float PrecioMin { get; set; }

        [Display(Name = "Precio máximo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public float PrecioMax { get; set; }

        [Display(Name = "Precio actual")]
        public float PrecioActual { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Estado { get; set; }


    }
}