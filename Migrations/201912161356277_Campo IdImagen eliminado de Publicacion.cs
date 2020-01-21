namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoIdImageneliminadodePublicacion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Publicacions", "IdImagen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publicacions", "IdImagen", c => c.Int(nullable: false));
        }
    }
}
