namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class CampoIdUsuarioactualizadoenPublicacioninteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publicacions", "IdUsuario", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publicacions", "IdUsuario", c => c.String());
        }
    }
}
