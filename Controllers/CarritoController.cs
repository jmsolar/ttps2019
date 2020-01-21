using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MercadoVentasTP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MercadoVentasTP.Controllers
{
	[Authorize]
	public class CarritoController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();
		private ApplicationUserManager _userManager;


		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

        [Authorize]
        [HttpGet]
        public ActionResult AgregarPublicacion(int? Id)
        {
            var publicacion = db.Publicaciones.Where(p => p.Id == Id).FirstOrDefault();
            if (publicacion == null)
            {
                return HttpNotFound();
            }

            var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
            var IdUser = userProp.IdUsuario;



			var publicacionEnCarrito = db.Carrito.Where(item => item.IdPublicacion == publicacion.Id).FirstOrDefault();
			if (publicacionEnCarrito != null)
			{					
				
				if (publicacionEnCarrito.Cantidad + 1 <= publicacion.Stock)
				{
					publicacionEnCarrito.Cantidad = publicacionEnCarrito.Cantidad + 1;
					db.Entry(publicacionEnCarrito).State = EntityState.Modified;
					TempData["Info"] = "El producto ha sido agregado correctamente al carrito";					
				}
				else
				{
					TempData["Error"] = "El producto no pudo ser agregado al carrito, porque se supero el limite de stock";
				}
			}
			else
			{
				Carrito nuevoItemCarrito = new Carrito { IdUsuario = IdUser, IdPublicacion = publicacion.Id };
				db.Carrito.Add(nuevoItemCarrito);
				Session["itemsCarrito"] = (db.Carrito.Where(c => c.IdUsuario == IdUser).Count()) + 1;
				TempData["Info"] = "El producto ha sido agregado correctamente al carrito";				
			}
			db.SaveChanges();


			return RedirectToAction("Detail", "Publicacion", new { id = Id });
  
        }

			
		
		// GET: Carrito
			public ActionResult Index()
		{
			ViewBag.Title = "Mi Carrito | MercadoVentas";
			ViewBag.Titulo = "Mi Carrito!";
			ViewBag.Subtitulo = "Desde acá vas a poder gestionar tu carrito y realizar la compra de tus productos seleccionados.";
			var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
			var IdUser = userProp.IdUsuario;

			var ItemsCarrito = db.Carrito.Where(c => c.IdUsuario == IdUser).Join(db.Publicaciones, c => c.IdPublicacion, p => p.Id, (carrito, publicacion) => new { carrito, publicacion }).ToArray();
			//List<Tuple<Carrito, Publicacion>> items = new List<Tuple<Carrito, Publicacion>>();
			List<PublicacionEnCarrito> items = new List<PublicacionEnCarrito>();

			foreach (var subItem in ItemsCarrito)
			{
				var itemData = subItem.publicacion;
				var carritoData = subItem.carrito;
				PublicacionEnCarrito publiCarrito = new PublicacionEnCarrito(carritoData, itemData);
				items.Add(publiCarrito);
			}

			return View(items);

		}



		// GET: Carrito/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Carrito carrito = db.Carrito.Find(id);
			db.Carrito.Remove(carrito);
			Session["itemsCarrito"] = Int32.Parse(Session["itemsCarrito"].ToString()) - 1;
			db.SaveChanges();

			return RedirectToAction("Index", "Carrito");

		}


		// GET: Carrito/Vaciar
		public ActionResult Vaciar()
		{
			var userProp = UserManager.FindByEmail(User.Identity.GetUserName());
			var IdUser = userProp.IdUsuario;
			var items = db.Carrito.Where(item => item.IdUsuario == IdUser);
			db.Carrito.RemoveRange(items);
			db.SaveChanges();
			Session["itemsCarrito"] = 0;
			return RedirectToAction("Index", "Carrito");

		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}

