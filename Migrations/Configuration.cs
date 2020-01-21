namespace MercadoVentasTP.Migrations
{
    using MercadoVentasTP.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var hasher = new PasswordHasher();
            var user1 = new ApplicationUser
            {
                UserName = "juan@mendoza.com",
                PasswordHash = hasher.HashPassword("juan@7Juan"),
                Apellido = "Mendoza",
                ConfPrecioActual = "Precio base",
                Email = "juan@mendoza.com",
                FechaNacimiento = new DateTime(1990, 03, 12),
                CoordX = 123,
                CoordY = 70,
                Nombre = "Juan",
                Puntaje = 1000,
                SaldoActual = 20000
            };

            var user2 = new ApplicationUser
            {
                UserName = "maria@zarate.com",
                PasswordHash = hasher.HashPassword("maria@7Maria"),
                Apellido = "Zarate",
                ConfPrecioActual = "Precio base",
                Email = "maria@zarate.com",
                FechaNacimiento = new DateTime(1985, 11, 23),
                CoordX = 10,
                CoordY = 56,
                Nombre = "Maria",
                Puntaje = 2000,
                SaldoActual = 20000
            };

            var user3 = new ApplicationUser
            {
                UserName = "josefina@flores.com",
                PasswordHash = hasher.HashPassword("josefina@7flores"),
                Apellido = "Flores",
                ConfPrecioActual = "Precio base",
                Email = "josefina@flores.com",
                FechaNacimiento = new DateTime(2001, 01, 02),
                CoordX = 100,
                CoordY = 156,
                Nombre = "Josefina",
                Puntaje = 2000,
                SaldoActual = 20000
            };

            var user4 = new ApplicationUser
            {
                UserName = "julian@lopez.com",
                PasswordHash = hasher.HashPassword("julian@7Lopez"),
                Apellido = "Lopez",
                ConfPrecioActual = "Precio base",
                Email = "julian@lopez.com",
                FechaNacimiento = new DateTime(2004, 01, 13),
                CoordX = 118,
                CoordY = 17,
                Nombre = "Julian",
                Puntaje = 2000,
                SaldoActual = 20000
            };

            manager.Create(user1); manager.Create(user2);
            manager.Create(user3); manager.Create(user4);
        }
    }
}
