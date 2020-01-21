namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificaciondetipoIdCategoriaenentidadPublicacion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publicacions", "IdCategoria", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publicacions", "IdCategoria", c => c.Int(nullable: false));
        }
    }
}
