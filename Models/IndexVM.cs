using System.Collections.Generic;

namespace MercadoVentasTP.Models
{
    public class IndexVM : Paginador
    {
        public List<Publicacion> Publicaciones { get; set; }
    }
}