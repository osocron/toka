using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Proyecto.Models.EntityModel;
using BitacoraDeSolicitud = Proyecto.Models.EntityModel.BitacoraDeSolicitud;

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

        // GET: BitacoraDeSolicituds/Create/5
        public ActionResult CreateFromSolicitud(int? idSolicitud)
        {
            var autos = db.Autos.ToList();
            var bitacoraDeSolicitud = new BitacoraConAutos
            {
                IdSolicitud = idSolicitud.GetValueOrDefault(0),
                AutosConSeleccion = autos.Select(a => 
                    new AutoConSeleccion {Auto = a, Seleccionado = false}).ToList()
            };
            return View(bitacoraDeSolicitud);
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

        // POST: BitacoraDeSolicituds/CreateFromSolicitud
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFromSolicitud(
            BitacoraConAutos bitacoraDeSolicitud)
        {
            if (ModelState.IsValid)
            {
                var seleccionados = bitacoraDeSolicitud.AutosConSeleccion
                    .Where(a => a.Seleccionado).ToList();
                foreach (var autoConSeleccion in seleccionados)
                {
                    db.CrearBitacora(0, bitacoraDeSolicitud.IdSolicitud, autoConSeleccion.Auto.IdAuto);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
