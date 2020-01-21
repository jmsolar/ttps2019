namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatosinicialesentidadCategoria : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categorias VALUES ('MLA1000', 'Electrónica, Audio y Video'), ('MLA1039', 'Cámaras y Accesorios'), ('MLA1051', 'Celulares y Teléfonos'), ('MLA1071', 'Animales y Mascotas'), ('MLA1132', 'Juegos y Juguetes'), ('MLA1144', 'Consolas y Videojuegos'), ('MLA1168', 'Música, Películas y Series'), ('MLA1182', 'Instrumentos Musicales'), ('MLA1246', 'Belleza y Cuidado Personal'), ('MLA1276', 'Deportes y Fitness'), ('MLA1367', 'Antigüedades y Colecciones'), ('MLA1368', 'Arte, Librería y Mercería'), ('MLA1384', 'Bebés'), ('MLA1403', 'Alimentos y Bebidas'), ('MLA1430', 'Ropa y Accesorios'), ('MLA1459', 'Inmuebles'), ('MLA1499', 'Industrias y Oficinas'), ('MLA1540', 'Servicios'), ('MLA1574', 'Hogar, Muebles y Jardín'), ('MLA1648', 'Computación'), ('MLA1743', 'Autos, Motos y Otros'), ('MLA1953', 'Otras categorías'), ('MLA2547', 'Entradas para Eventos'), ('MLA3025', 'Libros Revistas y Comics'), ('MLA3937', 'Joyas y Relojes'), ('MLA407134', 'Herramientas y Construcción'), ('MLA409431', 'Salud y Equipamiento Médico'), ('MLA5725', 'Accesorios para Vehículos'), ('MLA5726', 'Electrodomésticos y Aires Ac.'), ('MLA9304', 'Souvenirs, Cotillón y Fiestas')");
        }
        
        public override void Down()
        {
        }
    }
}
