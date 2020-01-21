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
    public class VentaPublicacionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VentaPublicacion
        public ActionResult Index(int idVenta)
        {
            return View(db.VentaPublicacions.Where(m => m.IdVenta == idVenta).ToList());
        }

        // GET: VentaPublicacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaPublicacion ventaPublicacion = db.VentaPublicacions.Find(id);
            if (ventaPublicacion == null)
            {
                return HttpNotFound();
            }
            return View(ventaPublicacion);
        }

        // GET: VentaPublicacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VentaPublicacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdVenta,IdPublicacion,Cantidad,Monto,NombrePublicacion")] VentaPublicacion ventaPublicacion)
        {
            if (ModelState.IsValid)
            {
                db.VentaPublicacions.Add(ventaPublicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ventaPublicacion);
        }

        // GET: VentaPublicacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaPublicacion ventaPublicacion = db.VentaPublicacions.Find(id);
            if (ventaPublicacion == null)
            {
                return HttpNotFound();
            }
            return View(ventaPublicacion);
        }

        // POST: VentaPublicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdVenta,IdPublicacion,Cantidad,Monto,NombrePublicacion")] VentaPublicacion ventaPublicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventaPublicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ventaPublicacion);
        }

        // GET: VentaPublicacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VentaPublicacion ventaPublicacion = db.VentaPublicacions.Find(id);
            if (ventaPublicacion == null)
            {
                return HttpNotFound();
            }
            return View(ventaPublicacion);
        }

        // POST: VentaPublicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VentaPublicacion ventaPublicacion = db.VentaPublicacions.Find(id);
            db.VentaPublicacions.Remove(ventaPublicacion);
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
