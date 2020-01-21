namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizaciontipodedatoFechaNacimiento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FechaNacimiento", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "FechaNacimiento", c => c.DateTime(nullable: false));
        }
    }
}
