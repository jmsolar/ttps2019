using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MercadoVentasTP.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public int CoordX { get; set; }

        public int CoordY { get; set; }

        public int Puntaje { get; set; }

        public string ConfPrecioActual { get; set; }

        public string RolUsuario { get; set; }

        public string Apellido { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'-'MM'-'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        public string Nombre { get; set; }

        public float SaldoActual { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authupdate-database -Force enticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("TTPSConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Movimiento> Movimientos { get; set; }

        public DbSet<Calificacion> Calificaciones { get; set; }

        public DbSet<Publicacion> Publicaciones { get; set; }

        public DbSet<ConfiguracionDelSistema> ConfiguracionDelSistema { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Carrito> Carrito { get; set; }

        public DbSet<Tiene> Tiene { get; set; }
        
        public DbSet<Palabra> Palabras { get; set; }
        
        public DbSet<Venta> Ventas { get; set; }

        public DbSet<VentaPublicacion> VentaPublicacions { get; set; }
    }
}