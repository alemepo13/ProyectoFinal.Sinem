using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sinem.Models;

namespace Sinem.Controllers
{
    public class AsistenciaEstudianteController : Controller
    {
        private SinemDBContext db = new SinemDBContext();

        // GET: AsistenciaEstudiante
        public ActionResult Index()
        {
            return View(db.AsistenciaEstudiantes.ToList());
        }

        // GET: AsistenciaEstudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id);
            if (asistenciaEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante);
        }

        // GET: AsistenciaEstudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsistenciaEstudiante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAsistenciaEstudiante,idGestionCurso,fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaEstudiante asistenciaEstudiante)
        {
            if (ModelState.IsValid)
            {
                db.AsistenciaEstudiantes.Add(asistenciaEstudiante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asistenciaEstudiante);
        }

        // GET: AsistenciaEstudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id);
            if (asistenciaEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante);
        }

        // POST: AsistenciaEstudiante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAsistenciaEstudiante,idGestionCurso,fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaEstudiante studentassistance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistenciaEstudiante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asistenciaEstudiante);
        }

        // GET: AsistenciaEstudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id);
            if (asistenciaEstudiante == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante);
        }

        // POST: AsistenciaEstudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id);
            db.AsistenciaEstudiantes.Remove(asistenciaEstudiante);
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
