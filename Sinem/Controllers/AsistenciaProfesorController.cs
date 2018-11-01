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
        private SinemDBContext db = new SinemDBContext(); //conexion a la base de datos

        // GET: AsistenciaProfesor
        public ActionResult Index() // esta esel metodo en que se muestra en la pagina principal la lista de asistencia profesor que esta en la base de datos
        {
            return View(db.AsistenciaProfesores.ToList());  //Esta es la vista de la asistencia profesor en forma de lista
        }

        // GET: AsistenciaProfesor/Details/5
        public ActionResult Details(int? id) //Este es el metodo que recibe como parametro un numero de asistencia profesor para buscarlo en la base de datos
        // aparte de  mostrar tambien en una pagina nueva con sus respectivos detalles
        {
            if (id == null) // aqui se puede ver si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesores.Find(id); //busca el numero de asistencia profesor en la base de datos
            if (asistenciaProfesor == null) // aqui se puede ver si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor); //devuelve los datos de asistencia profesor
        }

        // GET: AsistenciaProfesor/Create
        public ActionResult Create()  //Este es el metodo para crear una pagina, con los campos para crear una nueva asistencia profesor
        {
            return View(); // Devuelve la vista
        }

        // POST: AsistenciaProfesor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //para realizar la peticion al servidor
        public ActionResult Create([Bind(Include = "idAsistenciaProfesor,idGestionCurso,Fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaProfesor asistenciaProfesor)
        { //metodo para crear una pagina nueva en donde se van a mostrar los datos de la asistencia profesor,
            //lleva como parametros los datos de la asistencia profesor, ingresados por un usuario
            if (ModelState.IsValid) //si el post al servidor se hizo 
            {
                db.AsistenciaProfesores.Add(asistenciaProfesor); //agrega la asistencia profesor a la base de datos
                db.SaveChanges(); //guarda los cambios de la DB
                return RedirectToAction("Index"); //lo devuelve al inicio
            }

            return View(asistenciaProfesor); // devuelve los datos a de esa asistencia profesor
        }

        // GET: AsistenciaProfesor/Edit/5
        public ActionResult Edit(int? id) //Este es el metodo que recibe como parametro un numero la asistencia profesor para buscarlo en la base de datos
        {
            if (id == null) // si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesores.Find(id); // busca el numero de la asistencia profesor en la base de datos
            if (asistenciaProfesor == null) // si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor); // devuelve los datos de esa asistencia profesor
        }

        // POST: AsistenciaProfesor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //para realizar la peticion al servidor
        public ActionResult Edit([Bind(Include = "idAsistenciaProfesor,idGestionCurso,Fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaProfesor profesorassistance)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados de la asistencia profesor,
            //lleva como parametros los datos a editar de la asistencia profesor, ingresados por un usuario
            if (ModelState.IsValid) //si el post al servidor se hizo
            {
                db.Entry(profesorassistance).State = EntityState.Modified; //modifica los datos  de la asistencia profesor a la DB
                db.SaveChanges(); //guarda los cambios de la DB
                return RedirectToAction("Index"); //lo devuelve al inicio
            }
            return View(profesorassistance); //devuelve los datos de esa asistencia profesor
        }

        // GET: AsistenciaProfesor/Delete/5
        public ActionResult Delete(int? id) //metodo que recibe como parametro un numero de la asistencia profesor para buscarlo en la DB 
        {
            if (id == null) // si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesores.Find(id); //busca el numero de la asistencia profesor en la DB
            if (asistenciaProfesor == null) //si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaProfesor); //devuelve los datos de esa asistencia profesor
        }

        // POST: AsistenciaProfesor/Delete/5
        [HttpPost, ActionName("Delete")] //para realizar la peticion de borrar al servidor
        public ActionResult DeleteConfirmed(int id) //metodo que recibe como parametro un numero de la asistencia profesor para confirmar su eliminacion de DB
        {
            AsistenciaProfesor asistenciaProfesor = db.AsistenciaProfesores.Find(id);  //busca el numero de la asistencia profesor en la DB
            db.AsistenciaProfesores.Remove(asistenciaProfesor); //elimina la asistencia profesor
            db.SaveChanges(); //guarda los cambios de la DB
            return RedirectToAction("Index"); //lo devuelve al inicio
        }

        protected override void Dispose(bool disposing) //Metodo generado para liberar recursos no utilizados
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
