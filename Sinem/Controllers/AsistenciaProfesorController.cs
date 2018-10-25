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
    public class AsistenciaProfesorController : Controller
    {
        private SinemDBContext db = new SinemDBContext();

        // GET: AsistenciaProfesor
        public ActionResult Index()
        {
            return View(db.AsistenciaProfesors.ToList());
        }

        // GET: AsistenciaProfesor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesors.Find(id);
            if (asistenciaProfesor == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor);
        }

        // GET: AsistenciaProfesor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AsistenciaProfesor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAsistenciaProfesor,idGestionCurso,Fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaProfesor asistenciaProfesor)
        {
            if (ModelState.IsValid)
            {
                db.AsistenciaProfesors.Add(asistenciaProfesor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asistenciaProfesor);
        }

        // GET: AsistenciaProfesor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesors.Find(id);
            if (asistenciaProfesor == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor);
        }

        // POST: AsistenciaProfesor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAsistenciaProfesor,idGestionCurso,Fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaProfesor asistenciaProfesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistenciaProfesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asistenciaProfesor);
        }

        // GET: AsistenciaProfesor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesors.Find(id);
            if (asistenciaProfesor == null)
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor);
        }

        // POST: AsistenciaProfesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesors.Find(id);
            db.AsistenciaProfesors.Remove(asistenciaProfesor);
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
