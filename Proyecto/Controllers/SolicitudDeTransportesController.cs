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
    public class SolicitudDeTransportesController : Controller
    {
        private TransportesABCEntities db = new TransportesABCEntities();

        // GET: SolicitudDeTransportes
        public ActionResult Index()
        {
            return View(db.SolicitudDeTransporte.ToList());
        }

        // GET: SolicitudDeTransportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDeTransporte solicitudDeTransporte = db.SolicitudDeTransporte.Find(id);
            var bitacora = db.BitacoraDeSolicitud
                .Where(b => b.IdSolicitudDeTransporte == id)
                .Map(bi => bi.Autos)
                .ToList();
            if (solicitudDeTransporte == null)
            {
                return HttpNotFound();
            }
            return View(Tuple.Create(solicitudDeTransporte, bitacora));
        }

        // GET: SolicitudDeTransportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SolicitudDeTransportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Fecha,NumeroDeLote")] SolicitudDeTransporte solicitudDeTransporte)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudDeTransporte.Add(solicitudDeTransporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(solicitudDeTransporte);
        }

        // GET: SolicitudDeTransportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDeTransporte solicitudDeTransporte = db.SolicitudDeTransporte.Find(id);
            if (solicitudDeTransporte == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDeTransporte);
        }

        // POST: SolicitudDeTransportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitudDeTransporte,Fecha,NumeroDeLote")] SolicitudDeTransporte solicitudDeTransporte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudDeTransporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solicitudDeTransporte);
        }

        // GET: SolicitudDeTransportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudDeTransporte solicitudDeTransporte = db.SolicitudDeTransporte.Find(id);
            if (solicitudDeTransporte == null)
            {
                return HttpNotFound();
            }
            return View(solicitudDeTransporte);
        }

        // POST: SolicitudDeTransportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudDeTransporte solicitudDeTransporte = db.SolicitudDeTransporte.Find(id);
            db.SolicitudDeTransporte.Remove(solicitudDeTransporte);
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
