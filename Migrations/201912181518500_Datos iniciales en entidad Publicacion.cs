namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatosinicialesenentidadPublicacion : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Publicacions (IdCategoria, IdProducto, IdUsuario, Titulo, Descripcion, Estado, Precio, PrecioMin, PrecioMax, Stock, PrecioActual, Cantidad) VALUES ('MLA1051', 6, 3, 'Celular 100% original', 'Sellado,ensamblado en tierra del fuego. Varios colores, poseeo mucho stock', 'Activa', 10000, 8500, 15000, 24, 10000, 1), ('MLA1051', 6, 6, 'celular condicionado', 'ningun detalle ensamblado en estados unidos. Compra hoy de regalo una micro sd 32 gb', 'Sin Stock', 8000, 6500, 12000, 0, 8000, 1), ('MLA1071', 14, 1, 'Desparasitados,vacunados y listo para retirar', 'Todos muy mimosos son', 'Activa', 9200, 9000, 9500, 9, 9200, 1), ('MLA1182', 21, 2, 'Violonchelo excelente estado anio 2006 tenemos stock. Estilo clasico con microafinador', '...', 'Inactiva', 50000, 48000, 52000, 1, 50000, 1), ('MLA1182', 17, 2, 'Piano Ideal Estudio 1/4 de cola de contado hacemos un 5% de descuento, tengo stock tambien en acabado marrón', '...', 'Activa', 15100, 9500, 18100, 1, 15100, 1), ('MLA1403', 26, 7, 'quilmes cristal fria  hasta las 4 am. Precio de costo hasta las 22:00', 'con pizza', 'Activa', 220, 220, 260, 125, 220, 1), ('MLA1403', 26, 7, 'Quilmes Bock', 'muy buena', 'Activa', 250, 250, 310, 63, 250, 1), ('MLA1403', 26, 7, 'Imperial Stout', 'ideal para el sábado!', 'Inactiva', 280, 280, 345, 45, 280, 1), ('MLA1403', 26, 7, 'Cerveza Andes roja artesanal bien dulce', 'rica', 'Sin Stock', 300, 350, 400, 0, 300, 1), ('MLA1000', 45, 4, 'Sumergite en la pantalla. Comprando el televisor tenes un 5% de descuento si lo compras en los últimos días del mes y de contando te llevas de regalo un decodificador flow.', 'Con el Smart TV Samsung UN50MU6100, viví una nueva experiencia visual con la resolución 4K, que te presentará imágenes con gran detalle y de alta calidad', 'Activa', 39900, 28000, 65000, 10, 39900, 1)");
        }

        public override void Down()
        {
        }
    }
}
