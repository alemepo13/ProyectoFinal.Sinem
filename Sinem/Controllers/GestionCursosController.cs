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
    public class GestionCursosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();

        // GET: GestionCursos
        public ActionResult Index()
        {
            return View(db.GestionCursos.ToList());
        }

        // GET: GestionCursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);
            if (gestionCurso == null)
            {
                return HttpNotFound();
            }
            return View(gestionCurso);
        }

        // GET: GestionCursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GestionCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idGestionCurso,fechaInicio,fechaFinal,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] GestionCurso gestionCurso)
        {
            if (ModelState.IsValid)
            {
                db.GestionCursos.Add(gestionCurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gestionCurso);
        }

        // GET: GestionCursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);
            if (gestionCurso == null)
            {
                return HttpNotFound();
            }
            return View(gestionCurso);
        }

        // POST: GestionCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idGestionCurso,fechaInicio,fechaFinal,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] GestionCurso gestionCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestionCurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gestionCurso);
        }

        // GET: GestionCursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);
            if (gestionCurso == null)
            {
                return HttpNotFound();
            }
            return View(gestionCurso);
        }

        // POST: GestionCursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GestionCurso gestionCurso = db.GestionCursos.Find(id);
            db.GestionCursos.Remove(gestionCurso);
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
