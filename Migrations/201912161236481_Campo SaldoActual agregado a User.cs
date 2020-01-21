namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CampoSaldoActualagregadoaUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SaldoActual", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SaldoActual");
        }
    }
}
