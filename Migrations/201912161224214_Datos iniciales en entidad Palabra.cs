namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DatosinicialesenentidadPalabra : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Palabras VALUES ('bueno'), ('buena'), ('buenisimo'), ('buenisima'), ('genial'), ('excelente'), ('perfecto'), ('perfecta'), ('ok'), ('malo'), ('mala'), ('mal�simo'), ('malisima'), ('p�simo'), ('p�sima'), ('regular'), ('lindo'), ('linda'), ('hermoso'), ('hermosa'), ('feo'), ('fea'), ('horrible'), ('responsable'), ('irresponsable'), ('dedicado'), ('dedicada'), ('confianza'), ('desconfianza'), ('amabilidad'), ('lentitud'), ('atencion'), ('lento'), ('lenta'), ('lentisimo'), ('r�pido'), ('r�pida'), ('rapid�simo'), ('original'), ('verdadero'), ('verdadera'), ('completo'), ('completa'), ('sano'), ('sana'), ('falso'), ('falsa'), ('imitaci�n'), ('copia'), ('chino'), ('chinos'), ('roto'), ('rota'), ('agujereado'), ('agujereada'), ('abierto'), ('abierta'), ('vacio'), ('vacia'), ('fraude'), ('estafa'), ('amable'), ('atento'), ('atenta'), ('honesto'), ('honesta'), ('mentiroso'), ('mentirosa'), ('estafador'), ('estafadora'), ('caro'), ('cara'), ('barato'), ('barata'), ('raro'), ('rara')");
        }
        
        public override void Down()
        {
        }
    }
}
