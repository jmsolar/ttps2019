namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoApellidoobligatorioalcrearunusuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String());
        }
    }
}
