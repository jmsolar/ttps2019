namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoIdUsuarioagregadoenUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IdUsuario", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Publicacions", "IdUsuario", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publicacions", "IdUsuario", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "IdUsuario");
        }
    }
}
