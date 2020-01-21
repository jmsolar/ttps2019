namespace MercadoVentasTP.Models
{
    public class Carrito
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        public int IdPublicacion { get; set; }

        public int Cantidad { get; set; }

        public Carrito()
        {
            Cantidad = 1;
        }

    }
}