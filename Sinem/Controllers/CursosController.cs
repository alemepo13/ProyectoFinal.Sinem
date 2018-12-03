using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sinem.Models;
//importaciones

namespace Sinem.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CursosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos
        [OverrideAuthorization()]
        [Authorize(Roles = "Profesor")]
        public ActionResult IndexProfesor() {
            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            var l = from gc in db.GestionCursos.ToList()
                    join c in db.Cursos.ToList() on gc.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc.idAula equals a.idAula
                    join h in db.Horarios.ToList() on gc.idHorario equals h.idHorario
                    where gc.idUsuario== Usuario.idUsuario
                    select new Vista_CursoMatriculado { FechaInicio = gc.fechaInicio.ToString(),
                        FechaFinal = gc.fechaFinal.ToString(),
                        Aula = a.numeroAula + " " + a.tipoAula,
                        Horario = h.descripcion,
                        Curso = c.nombre, idGC = gc.idGestionCurso };

            return View(l.ToList());
        }
        [OverrideAuthorization()]
        [Authorize(Roles = "Estudiante")]
        public ActionResult IndexEstudiante()
        {
            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            var l = from dm in db.DetalleMatriculas.ToList()
                    join gc in db.GestionCursos.ToList() on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on gc.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc.idAula equals a.idAula
                    join h in db.Horarios.ToList() on gc.idHorario equals h.idHorario
                    where dm.usuarioCrea == User.Identity.Name
                    select new Vista_CursoMatriculado
                    {
                        FechaInicio = gc.fechaInicio.ToString(),
                        FechaFinal = gc.fechaFinal.ToString(),
                        Aula = a.numeroAula + " " + a.tipoAula,
                        Horario = h.descripcion,
                        Curso = c.nombre,
                        idGC = gc.idGestionCurso
                    };

            return View(l.ToList());
        }
        // GET: Cursos
        public ActionResult Index()//metodo que muestra en la pagina principal una la lista de cursos que estan en la DB
        {
            return View(db.Cursos.ToList());//vista de los cursos en forma de lista
        }

        // GET: Cursos/Details/5
        public ActionResult Details(int? id)//metodo que recibe como parametro un numero de curso para buscarlo en la DB
            //y mostrar en una pagina nueva sus detalles
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);//busca el numero de curso en la DB
            if (curso == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(curso);//devuelve los datos de ese curso
        }

        // GET: Cursos/Create
        public ActionResult Create()//metodo para crear una pagina, con los campos para crear un nuevo curso
        {
            return View();//devuelve la vista
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Create([Bind(Include = "idCurso,nombre,descripcion,costo,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Curso curso)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos del nuevo curso,
            //lleva como parametros los datos del nuevo curso, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Cursos.Add(curso);//agrega el curso a la DB
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }

            return View(curso);//devuelve los datos de ese curso
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)//metodo que recibe como parametro un numero de curso para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);//busca el numero de curso en la DB
            if (curso == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(curso);//devuelve los datos de esa curso
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Edit([Bind(Include = "idCurso,nombre,descripcion,costo,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Curso course)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados del curso,
            //lleva como parametros los datos a editar del curso, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo
            {
                db.Entry(course).State = EntityState.Modified;//modifica los datos  del curso a la DB
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }
            return View(course);//devuelve los datos de ese curso
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int? id)//metodo que recibe como parametro un numero de curso para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);//busca el numero de curso en la DB
            if (curso == null)// si el numero es nulo
            {

                return HttpNotFound();
            }
            return View(curso);//devuelve los datos de ese curso
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion de borrar al servidor
        public ActionResult DeleteConfirmed(int id)//metodo que recibe como parametro un numero de curso para confirmar su eliminacion
            //de la DB
        {
            Curso curso = db.Cursos.Find(id);//busca el numero de curso en la DB
            db.Cursos.Remove(curso);//elimina el curso
            db.SaveChanges();//guarda los cambios de la DB
            return RedirectToAction("Index");//lo devuelve al inicio
        }

        protected override void Dispose(bool disposing)//Metodo generado para liberar recursos no utilizados 
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
