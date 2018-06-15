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
    public class BitacoraDeSolicitudsController : Controller
    {
        private TransportesABCEntities db = new TransportesABCEntities();

        // GET: BitacoraDeSolicituds
        public ActionResult Index()
        {
            var bitacoraDeSolicitud = db.BitacoraDeSolicitud.Include(b => b.Autos).Include(b => b.SolicitudDeTransporte);
            return View(bitacoraDeSolicitud.ToList());
        }

        // GET: BitacoraDeSolicituds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraDeSolicitud bitacoraDeSolicitud = db.BitacoraDeSolicitud.Find(id);
            if (bitacoraDeSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraDeSolicitud);
        }

        // GET: BitacoraDeSolicituds/Create
        public ActionResult Create()
        {
            ViewBag.IdAuto = new SelectList(db.Autos, "IdAuto", "Folio");
            ViewBag.IdSolicitudDeTransporte = new SelectList(db.SolicitudDeTransporte, "IdSolicitudDeTransporte", "NumeroDeLote");
            return View();
        }

        // POST: BitacoraDeSolicituds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBitacoraDeSolicitud,IdSolicitudDeTransporte,IdAuto")] BitacoraDeSolicitud bitacoraDeSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.BitacoraDeSolicitud.Add(bitacoraDeSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAuto = new SelectList(db.Autos, "IdAuto", "Folio", bitacoraDeSolicitud.IdAuto);
            ViewBag.IdSolicitudDeTransporte = new SelectList(db.SolicitudDeTransporte, "IdSolicitudDeTransporte", "NumeroDeLote", bitacoraDeSolicitud.IdSolicitudDeTransporte);
            return View(bitacoraDeSolicitud);
        }

        // GET: BitacoraDeSolicituds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraDeSolicitud bitacoraDeSolicitud = db.BitacoraDeSolicitud.Find(id);
            if (bitacoraDeSolicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAuto = new SelectList(db.Autos, "IdAuto", "Folio", bitacoraDeSolicitud.IdAuto);
            ViewBag.IdSolicitudDeTransporte = new SelectList(db.SolicitudDeTransporte, "IdSolicitudDeTransporte", "NumeroDeLote", bitacoraDeSolicitud.IdSolicitudDeTransporte);
            return View(bitacoraDeSolicitud);
        }

        // POST: BitacoraDeSolicituds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBitacoraDeSolicitud,IdSolicitudDeTransporte,IdAuto")] BitacoraDeSolicitud bitacoraDeSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bitacoraDeSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAuto = new SelectList(db.Autos, "IdAuto", "Folio", bitacoraDeSolicitud.IdAuto);
            ViewBag.IdSolicitudDeTransporte = new SelectList(db.SolicitudDeTransporte, "IdSolicitudDeTransporte", "NumeroDeLote", bitacoraDeSolicitud.IdSolicitudDeTransporte);
            return View(bitacoraDeSolicitud);
        }

        // GET: BitacoraDeSolicituds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BitacoraDeSolicitud bitacoraDeSolicitud = db.BitacoraDeSolicitud.Find(id);
            if (bitacoraDeSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(bitacoraDeSolicitud);
        }

        // POST: BitacoraDeSolicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BitacoraDeSolicitud bitacoraDeSolicitud = db.BitacoraDeSolicitud.Find(id);
            db.BitacoraDeSolicitud.Remove(bitacoraDeSolicitud);
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
