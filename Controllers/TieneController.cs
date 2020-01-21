using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MercadoVentasTP.Models;

namespace MercadoVentasTP.Controllers
{
    public class TieneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tiene
        [Authorize]
        public ActionResult Index(int idCompra)
        {

        //   var publicacionesDeLaCompra = db.Publicaciones.Join(db.Tiene, p => p.Id,
                                      //  t => t.IdPublicacion, (p,t) => new {p,t }).Where(x => x.t.IdCompra == idCompra).ToList();
          
         var publicacionesDeLaCompra = db.Tiene.Where(m => m.IdCompra == idCompra).Where(m => m.Estado == "Sin calificar").OrderByDescending(m => m.Estado).ToList();

         return View(publicacionesDeLaCompra);
        }

        // GET: Tiene/Details/5
        public ActionResult Details(int idCompra)
        {

            var publicacionesDeLaCompra = db.Tiene.Where(m => m.IdCompra == idCompra).OrderByDescending(m => m.Estado).ToList();

            return View(publicacionesDeLaCompra);
        }

        // GET: Tiene/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tiene/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdCompra,IdPublicacion,Cantidad,Estado,Monto")] Tiene tiene)
        {
            if (ModelState.IsValid)
            {
                db.Tiene.Add(tiene);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiene);
        }

        // GET: Tiene/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tiene tiene = db.Tiene.Find(id);
            if (tiene == null)
            {
                return HttpNotFound();
            }
            return View(tiene);
        }

        // POST: Tiene/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdCompra,IdPublicacion,Cantidad,Estado,Monto")] Tiene tiene)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiene).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiene);
        }

        // GET: Tiene/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tiene tiene = db.Tiene.Find(id);
            if (tiene == null)
            {
                return HttpNotFound();
            }
            return View(tiene);
        }

        // POST: Tiene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tiene tiene = db.Tiene.Find(id);
            db.Tiene.Remove(tiene);
            db.SaveChanges();
            return RedirectToAction("Index");
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
