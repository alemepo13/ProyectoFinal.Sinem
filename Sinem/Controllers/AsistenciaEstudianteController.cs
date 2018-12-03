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
    [Authorize(Roles = "Administrador")]
    public class AsistenciaEstudianteController : Controller
    {
        private SinemDBContext db = new SinemDBContext(); //conexion a la base de datos

        // GET: AsistenciaEstudiante
        [OverrideAuthorization()]
        [Authorize(Roles = "Profesor")]
        public ActionResult Index(int idGestionCurso) // esta esel metodo en que se muestra en la pagina principal la lista de sistencia del estudiante que esta en la base de datos
        {
            var l = from dm in db.DetalleMatriculas
                    join u in db.Usuario on dm.idEstudiante equals u.idUsuario
                    where dm.idgestionCurso == idGestionCurso
                    select new Vista_Asistencia {
                        nombre = u.nombre, 
                        apellidos = u.apellido,
                        asistio = false,
                        observaciones = " ",
                        idUsuario = u.idUsuario

                    };
            return View(l); 
            //return View(db.AsistenciaEstudiantes.ToList()); //Esta es la vista de la asistencia estudiante en forma de lista
        }

        // GET: AsistenciaEstudiante/Details/5
        public ActionResult Details(int? id) //Este es el metodo que recibe como parametro un numero de asistencia estudiante para buscarlo en la base de datos
             // aparte de  mostrar tambien en una pagina nueva con sus respectivos detalles
        {
            if (id == null) // aqui se puede ver si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id); //busca el numero de asistencia estudiante en la base de datos
            if (asistenciaEstudiante == null) // aqui se puede ver si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante); //devuelve los datos de asistencia estudiante
        }

        // GET: AsistenciaEstudiante/Create
        public ActionResult Create() //Este es el metodo para crear una pagina, con los campos para crear una nueva asistencia estudiante
        { 
            return View(); // devuelve la vista
        
        }

        // POST: AsistenciaEstudiante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //para realizar la peticion al servidor
        public ActionResult Create([Bind(Include = "idAsistenciaEstudiante,idGestionCurso,fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaEstudiante asistenciaEstudiante)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos de la asistencia estudiante,
            //lleva como parametros los datos de la asistencia estudiante, ingresados por un usuario
            if (ModelState.IsValid) //si el post al servidor se hizo 
            {
                db.AsistenciaEstudiantes.Add(asistenciaEstudiante); //agrega la asistencia estudiante a la base de datos
                db.SaveChanges(); //guarda los cambios de la DB
                return RedirectToAction("Index"); //lo devuelve al inicio
            }

            return View(asistenciaEstudiante); // devuelve los datos a de esa asistencia estudiante
        }

        // GET: AsistenciaEstudiante/Edit/5
        public ActionResult Edit(int? id) //Este es el metodo que recibe como parametro un numero la asistencia estudiante para buscarlo en la base de datos
        {
            if (id == null) // si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id); // busca el numero de la asistencia estudiante en la base de datos
            if (asistenciaEstudiante == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante); // decuelve los datos de esa asistencia estudiante
        }

        // POST: AsistenciaEstudiante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] //para realizar la peticion al servidor
        public ActionResult Edit([Bind(Include = "idAsistenciaEstudiante,idGestionCurso,fecha,asistio,observaciones,fechaModifica,usuarioModifica,idUsuario")] AsistenciaEstudiante studentassistance)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados de la asistencia estudiante,
            //lleva como parametros los datos a editar de la asistencia estudiante, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo
            {
                db.Entry(studentassistance).State = EntityState.Modified; //modifica los datos  de la asistencia estudiante a la DB
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index"); //lo devuelve al inicio
            }
            return View(studentassistance); //devuelve los datos de esa asistencia estudiante
        }

        // GET: AsistenciaEstudiante/Delete/5
        public ActionResult Delete(int? id) //metodo que recibe como parametro un numero de la asistencia estudiante para buscarlo en la DB 
        {
            if (id == null) // si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id);//busca el numero de la asistencia estudiante en la DB
            if (asistenciaEstudiante == null) // si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(asistenciaEstudiante); //devuelve los datos de esa asistencia estudiante
        }

        // POST: AsistenciaEstudiante/Delete/5
        [HttpPost, ActionName("Delete")] //para realizar la peticion de borrar al servidor
        public ActionResult DeleteConfirmed(int id) //metodo que recibe como parametro un numero de la asistencia estududiante para confirmar su eliminacion de DB
        {
            AsistenciaEstudiante asistenciaEstudiante = db.AsistenciaEstudiantes.Find(id); //busca el numero de la asistencia estudiante en la DB
            db.AsistenciaEstudiantes.Remove(asistenciaEstudiante); //elimina la asistencia estudiante
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
