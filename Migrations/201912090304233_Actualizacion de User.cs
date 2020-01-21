namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaciondeUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Apellido", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "Nombre");
        }
    }
}
