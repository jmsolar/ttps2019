using System.Collections.Generic;

namespace MercadoVentasTP.Models
{
    public class MovimientoVM : Paginador
    {
        public List<Movimiento> Movimientos { get; set; }
    }
}