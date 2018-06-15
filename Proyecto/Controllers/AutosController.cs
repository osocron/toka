using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models.EntityModel;

namespace Proyecto.Controllers
{
    public class AutosController : Controller
    {
        private TransportesABCEntities db = new TransportesABCEntities();

        // GET: Autos
        public ActionResult Index()
        {
            var autos = db.Autos.Include(a => a.Marcas).Include(a => a.Modelos).Include(a => a.TiposTransmision);
            return View(autos.ToList());
        }

        // GET: Autos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }
            return View(autos);
        }

        // GET: Autos/Create
        public ActionResult Create()
        {
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion");
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion");
            ViewBag.IdTipoTransmision = new SelectList(db.TiposTransmision, "IdTipoTransmision", "Descripcion");
            return View();
        }

        // POST: Autos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Folio,IdMarca,IdModelo,Color,IdTipoTransmision,DescripcionEstetica")] Autos autos)
        {
            if (ModelState.IsValid)
            {
                db.Autos.Add(autos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", autos.IdMarca);
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", autos.IdModelo);
            ViewBag.IdTipoTransmision = new SelectList(db.TiposTransmision, "IdTipoTransmision", "Descripcion", autos.IdTipoTransmision);
            return View(autos);
        }

        // GET: Autos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", autos.IdMarca);
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", autos.IdModelo);
            ViewBag.IdTipoTransmision = new SelectList(db.TiposTransmision, "IdTipoTransmision", "Descripcion", autos.IdTipoTransmision);
            return View(autos);
        }

        // POST: Autos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAuto,Folio,IdMarca,IdModelo,Color,IdTipoTransmision,DescripcionEstetica")] Autos autos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdMarca = new SelectList(db.Marcas, "IdMarca", "Descripcion", autos.IdMarca);
            ViewBag.IdModelo = new SelectList(db.Modelos, "IdModelo", "Descripcion", autos.IdModelo);
            ViewBag.IdTipoTransmision = new SelectList(db.TiposTransmision, "IdTipoTransmision", "Descripcion", autos.IdTipoTransmision);
            return View(autos);
        }

        // GET: Autos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autos autos = db.Autos.Find(id);
            if (autos == null)
            {
                return HttpNotFound();
            }
            return View(autos);
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autos autos = db.Autos.Find(id);
            db.Autos.Remove(autos);
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
