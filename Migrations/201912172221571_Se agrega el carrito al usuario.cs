namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seagregaelcarritoalusuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarritoDeCompras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Publicacions", "CarritoDeCompras_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "CarritoDeCompras_Id", c => c.Int());
            CreateIndex("dbo.Publicacions", "CarritoDeCompras_Id");
            CreateIndex("dbo.AspNetUsers", "CarritoDeCompras_Id");
            AddForeignKey("dbo.Publicacions", "CarritoDeCompras_Id", "dbo.CarritoDeCompras", "Id");
            AddForeignKey("dbo.AspNetUsers", "CarritoDeCompras_Id", "dbo.CarritoDeCompras", "Id");
            DropColumn("dbo.Calificacions", "IdCompra");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Calificacions", "IdCompra", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "CarritoDeCompras_Id", "dbo.CarritoDeCompras");
            DropForeignKey("dbo.Publicacions", "CarritoDeCompras_Id", "dbo.CarritoDeCompras");
            DropIndex("dbo.AspNetUsers", new[] { "CarritoDeCompras_Id" });
            DropIndex("dbo.Publicacions", new[] { "CarritoDeCompras_Id" });
            DropColumn("dbo.AspNetUsers", "CarritoDeCompras_Id");
            DropColumn("dbo.Publicacions", "CarritoDeCompras_Id");
            DropTable("dbo.CarritoDeCompras");
        }
    }
}
