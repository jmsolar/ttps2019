namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeagregaPrecioActualalusuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publicacions", "PrecioActual", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publicacions", "PrecioActual");
        }
    }
}
