namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Configuracioninicial : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ConfiguracionDelSistemas (ComisionPorVenta, CantPalabras, CantOpinionesEnPublicacion) VALUES (5, 10, 5)");
        }
        
        public override void Down()
        {
        }
    }
}
