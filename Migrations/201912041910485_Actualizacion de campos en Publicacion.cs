namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaciondecamposenPublicacion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publicacions", "Precio", c => c.Single(nullable: false));
            AlterColumn("dbo.Publicacions", "PrecioMin", c => c.Single(nullable: false));
            AlterColumn("dbo.Publicacions", "PrecioMax", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publicacions", "PrecioMax", c => c.Int(nullable: false));
            AlterColumn("dbo.Publicacions", "PrecioMin", c => c.Int(nullable: false));
            AlterColumn("dbo.Publicacions", "Precio", c => c.Int(nullable: false));
        }
    }
}
