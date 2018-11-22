using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RazorPDF;
using Sinem.Models;

namespace Sinem.Controllers
{
    public class GestionCursosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//Conexion a la base de datos 

        [Authorize(Roles = "Estudiante")]
        public ActionResult Matricula() {
            var Usuario = (from U in db.Usuario where U.usuario == User.Identity.Name select U).FirstOrDefault();
            var l = from dm in db.DetalleMatriculas.ToList()
                    join gc in db.GestionCursos.ToList() on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on gc.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc.idAula equals a.idAula
                    join h in db.Horarios.ToList() on gc.idHorario equals h.idHorario
                    where gc.idUsuario == Usuario.idUsuario
                    select new
                    {
                        FechaInicio = gc.fechaInicio,
                        FechaFinal = gc.fechaFinal,
                        Aula = a.numeroAula + " " + a.tipoAula,
                        Horario = h.descripcion,
                        Curso = c.nombre,
                        idGC = gc.idGestionCurso
                    };

            return View(l.ToList());
        }

        [Authorize(Roles = "Estudiante")]
        public ActionResult Matricular(int idGestionCurso) {

            var Usuario = (from U in db.Usuario where U.usuario == User.Identity.Name select U).FirstOrDefault();
            var gc = db.GestionCursos.Find(idGestionCurso);
            var dm = new DetalleMatricula();
            dm.fechaModifica = DateTime.Today;
            dm.fechaRegistro = DateTime.Today;
            dm.usuarioCrea = User.Identity.Name;
            dm.usuarioModifica = User.Identity.Name;
            dm.idgestionCurso = idGestionCurso;
            dm.idEstudiante = Usuario.idUsuario;
            dm.idHorario = gc.idHorario;
            dm.idProfesor = gc.idUsuario;
            dm.idCurso = gc.idCurso;
            db.DetalleMatriculas.Add(dm);
            db.SaveChanges();
            return new PdfResult(dm, "Comprobante");
            //return RedirectToAction("IndexEstudiante","Cursos");
        }
        private void ListaDeAulas(object o = null)
        {
            ViewBag.ListaAulas = new SelectList(db.Aulas.ToList(), "idAula", "numeroAula", o);
        }

        private void ListaDeHorarios(object o = null)
        {
            ViewBag.ListaHorarios = new SelectList(db.Horarios.ToList(), "idHorario", "descripcion", o);
        }

        private void ListaDeCursos(object o = null)
        {
            ViewBag.ListaCursos = new SelectList(db.Cursos.ToList(), "idCurso", "nombre", o);
        }

        private void ListaDeUsuarios(object o = null)
        {
            ViewBag.ListaUsuarios = new SelectList(db.Usuario.ToList(), "idUsuario", "nombrecompleto", o);
        }

        // GET: GestionCursos
        public ActionResult Index()//Metodo que permite visualizar la lista de gestion de cursos desde el menu principal
        {
            return View(db.GestionCursos.ToList());//Vista de los datos de gestion de cursos en forma de lista 
        }

        // GET: GestionCursos/Details/5
        public ActionResult Details(int? id)//Metodo que recibe como parametro el id de la gestion del curso para buscarlo en la base de datos y visualizarlo
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//devuelve un mensaje si el numero es nulo 
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);//busca el id de la gestion del curso dentro de la BD
            if (gestionCurso == null)
            {
                return HttpNotFound();//devuelve una pantalla de error si el id es null
            }
            return View(gestionCurso);//devuelve los datos de la gestion de curso 
        }

        // GET: GestionCursos/Create
        public ActionResult Create()//metodo para crear una nueva pagina con los campos para manejar una nueva gestion de curso
        {
            ListaDeAulas();
            ListaDeCursos();
            ListaDeHorarios();
            ListaDeUsuarios();
            return View();//devuelve la vista 
        }

        // POST: GestionCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//realiza la peticion al servidor
        //Metodo para mostrar los nuevos datos de gestion de curso, usa como parametros los datos ingresados por el usuario
        public ActionResult Create([Bind(Include = "idGestionCurso,fechaInicio,fechaFinal,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] GestionCurso gestionCurso)
        {
            if (ModelState.IsValid)//si el post al servidor se hizo
            {
                db.GestionCursos.Add(gestionCurso);//se agrega la gestion de curso a la bd
                db.SaveChanges();//y guarda los cambios en la db 
                return RedirectToAction("Index");//devuelve al usuario al inicio 
            }

            return View(gestionCurso);//devuelve los datos de la gestion de curso creada
        }

        // GET: GestionCursos/Edit/5
        public ActionResult Edit(int? id)//metodo para buscar una gestion de curso en la base de datos mediante id para editarla
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//devuelve un mensaje si el id no existe
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);//busca el id de la gestion de curso en la bd
            if (gestionCurso == null)
            {
                return HttpNotFound();//devuelve una pantalla de error si no encuentra la gestion de curso
            }

            ListaDeAulas(gestionCurso.idAula);
            ListaDeCursos(gestionCurso.idCurso);
            ListaDeHorarios(gestionCurso.idHorario);
            ListaDeUsuarios(gestionCurso.idUsuario);
            return View(gestionCurso);//devuelve la vista de los datos de la gestion de curso
        }

        // POST: GestionCursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//realiza la peticion al servidor
        //Metodo para mostrar los datos actualizados de gestion de curso, usa como parametros los datos ingresados por el usuario
        public ActionResult Edit([Bind(Include = "idGestionCurso,fechaInicio,fechaFinal,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] GestionCurso gestionCur)
        {
            if (ModelState.IsValid)//si el post al servidor se hizo
            {
                db.Entry(gestionCur).State = EntityState.Modified;//se modifican los datos de la gestion de curso
                db.SaveChanges();//y se guardan los cambios en la bd
                return RedirectToAction("Index");//devuelve al usuario al inicio
            }
            return View(gestionCur);//devuelve los datos de la gestion de curso editada 
        }

        // GET: GestionCursos/Delete/5
        public ActionResult Delete(int? id)//metodo que recibe como parametro el id de la gestion de curso para buscarlo en la bd
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //devuelve un mensaje si el id de gestion curso no existe 
            }
            GestionCurso gestionCurso = db.GestionCursos.Find(id);//busca el id de la gestion de curso en la bd 
            if (gestionCurso == null)
            {
                return HttpNotFound();//devuelve una pantalla de error si no se encuentra la gestion de curso 
            }
            return View(gestionCurso);//devuelve los datos de la gestion de curso 
        }

        // POST: GestionCursos/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion al servidor para eliminar 
        //metodo que recibe como parametro el id de la gestion de curso ingresada por el usuario para confirmar la eliminacion en la bd
        public ActionResult DeleteConfirmed(int id)
        {
            GestionCurso gestionCurso = db.GestionCursos.Find(id);//busca el numero de la gestion de curso en la bd 
            db.GestionCursos.Remove(gestionCurso);//elimina los datos de dicha gestion de curso de la bd 
            db.SaveChanges();//guarda los cambios en la bd
            return RedirectToAction("Index");//devuelve al usuario al inicio 
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
