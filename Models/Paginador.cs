using System.Web.Routing;

namespace MercadoVentasTP.Models
{
    public class Paginador
    {
        public int PaginaActual { get; set; }

        public int TotalRegistros { get; set; }

        public int RegistrosPorPagina { get; set; }

        public RouteValueDictionary Filtro { get; set; }
    }
}