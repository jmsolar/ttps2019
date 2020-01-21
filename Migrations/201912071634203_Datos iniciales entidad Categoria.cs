namespace MercadoVentasTP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatosinicialesentidadCategoria : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categorias VALUES ('MLA1000', 'Electr�nica, Audio y Video'), ('MLA1039', 'C�maras y Accesorios'), ('MLA1051', 'Celulares y Tel�fonos'), ('MLA1071', 'Animales y Mascotas'), ('MLA1132', 'Juegos y Juguetes'), ('MLA1144', 'Consolas y Videojuegos'), ('MLA1168', 'M�sica, Pel�culas y Series'), ('MLA1182', 'Instrumentos Musicales'), ('MLA1246', 'Belleza y Cuidado Personal'), ('MLA1276', 'Deportes y Fitness'), ('MLA1367', 'Antig�edades y Colecciones'), ('MLA1368', 'Arte, Librer�a y Mercer�a'), ('MLA1384', 'Beb�s'), ('MLA1403', 'Alimentos y Bebidas'), ('MLA1430', 'Ropa y Accesorios'), ('MLA1459', 'Inmuebles'), ('MLA1499', 'Industrias y Oficinas'), ('MLA1540', 'Servicios'), ('MLA1574', 'Hogar, Muebles y Jard�n'), ('MLA1648', 'Computaci�n'), ('MLA1743', 'Autos, Motos y Otros'), ('MLA1953', 'Otras categor�as'), ('MLA2547', 'Entradas para Eventos'), ('MLA3025', 'Libros Revistas y Comics'), ('MLA3937', 'Joyas y Relojes'), ('MLA407134', 'Herramientas y Construcci�n'), ('MLA409431', 'Salud y Equipamiento M�dico'), ('MLA5725', 'Accesorios para Veh�culos'), ('MLA5726', 'Electrodom�sticos y Aires Ac.'), ('MLA9304', 'Souvenirs, Cotill�n y Fiestas')");
        }
        
        public override void Down()
        {
        }
    }
}
