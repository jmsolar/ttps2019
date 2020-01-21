namespace MercadoVentasTP.Models
{
    public class ConfiguracionDelSistema
    {
        public int Id { set; get; }

        public float ComisionPorVenta { get; set; }

        public int CantPalabras { get; set; }

        public int CantOpinionesEnPublicacion { get; set; }
    }
}