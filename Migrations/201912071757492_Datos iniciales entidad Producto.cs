namespace MercadoVentasTP.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DatosinicialesentidadProducto : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Productoes VALUES ('Memoria Micro Sd Hc 16 Gb Kingston Clase 10 Tienda', 'Siendo la tarjeta SD m�s peque�a disponible, la microSD Clase 10 es la opci�n de almacenamiento expandible est�ndar para muchas tablets, tel�fonos inteligentes y c�maras de acci�n.', 'MLA1051'), ('Celular samsung j6 plus, linea galaxy J', 'Modelo j6 posee memoria interna de 32 gb uno de los mejores celulares de gama media,pantalla vibrante infinity display', 'MLA1051'), ('Perros Cachorros Caniche Toy', 'Cachorritos caniche toy., a partir de los 45 d�as para la venta', 'MLA1071'), ('Piano cola steinway essex egp 155 negro nuevo', 'Piano de cola steinway essex. Modelo nuevo 155 egp', 'MLA1182'), ('Violonchelo  cello 4/4 custom parquer ce900', 'Violonchelos de diferente tipo', 'MLA1182'),('Cerveza  porroncito 340 ml envase no retornable', 'Cervezas de todo tipo', 'MLA1403'), ('Smart TV Samsung 32� UN32J4290AGXZD', 'Sumergite en la pantalla. Con el Smart TV Samsung UN32J4290, viv� una nueva experiencia visual con la resoluci�n HD, que te presentar� im�genes con gran detalle y de alta calidad. Ahora todo lo que veas cobrar� vida con brillo y colores m�s reales. Gracias a su tama�o de 32, podr�s transformar tu espacio en una sala de cine y transportarte a donde quieras.', 'MLA1000')");
        }
        
        public override void Down()
        {
        }
    }
}
