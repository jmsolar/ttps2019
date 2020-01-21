using System.ComponentModel.DataAnnotations;

namespace MercadoVentasTP.Models
{
    public class Categoria
    {
        [Key]
        public string Id { get; set; }

        public string Nombre { get; set; }
    }
}