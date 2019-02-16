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
            
            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            var listaMatriculados = from k in db.DetalleMatriculas.ToList() where k.idEstudiante == Usuario.idUsuario select k.idgestionCurso;
            var listaHorarios = from k in db.DetalleMatriculas.ToList() where k.idEstudiante == Usuario.idUsuario select k.idHorario;

            var l = //from dm in db.DetalleMatriculas.ToList()
                    from gc in db.GestionCursos.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on gc.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc.idAula equals a.idAula
                    join h in db.Horarios.ToList() on gc.idHorario equals h.idHorario
                    join cup in db.Cupos.ToList() on gc.idGestionCurso equals cup.idGestionCurso

                    //where gc.idUsuario == Usuario.idUsuario
                    select new Vista_CursoMatriculable ()
                    {
                        FechaInicio = gc.fechaInicio,
                        FechaFinal = gc.fechaFinal,
                        Aula = a.numeroAula + " " + a.tipoAula,
                        Horario = h.descripcion,
                        Curso = c.nombre,
                        Cupo=cup.cupo,
                        YaMatriculado=listaMatriculados.Contains(gc.idGestionCurso),
                        horarioOcupado=listaHorarios.Contains(gc.idHorario),
                        idGC = gc.idGestionCurso
                    };

            return View(l.ToList());
        }

        [HttpPost]
        public ActionResult Matricula(string[] selectedCourses)
        {
            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            List<int> idDetalleMatricula = new List<int>();
            foreach (var item in selectedCourses)
            {
                var gc = db.GestionCursos.Find(Convert.ToInt32(item));
                var cu = db.Cursos.Find(gc.idCurso);
                var dm = new DetalleMatricula();
                dm.fechaModifica = DateTime.Today;
                dm.fechaRegistro = DateTime.Today;
                dm.usuarioCrea = User.Identity.Name;
                dm.usuarioModifica = User.Identity.Name;
                dm.idgestionCurso = gc.idGestionCurso;
                dm.idEstudiante = Usuario.idUsuario;
                dm.idHorario = gc.idHorario;
                dm.idProfesor = gc.idUsuario;
                dm.idCurso = gc.idCurso;
                dm.costo = cu.costo;
                db.DetalleMatriculas.Add(dm);
                db.SaveChanges();
                 idDetalleMatricula.Add(dm.idDetalleMatricula);
                // buscar el cupo correspindiente 
                Cupo cup = (from cupos in db.Cupos where cupos.idGestionCurso == gc.idGestionCurso select cupos).FirstOrDefault();
                // si no existe lo creamos
                if (cup == null) {
                    cup = new Cupo();
                    cup.fechaModifica = DateTime.Today;
                    cup.fechaRegistro = DateTime.Today;
                    cup.usuarioCrea = User.Identity.Name;
                    cup.usuarioModifica = User.Identity.Name;
                    cup.idGestionCurso = gc.idGestionCurso;
                    cup.cupo = gc.cupo;
                    db.Cupos.Add(cup); ;
                    db.SaveChanges();
                }
                cup.cupo = cup.cupo - 1;
                db.SaveChanges();
            }
            var l = from dm in db.DetalleMatriculas.ToList()
                    join gc1 in db.GestionCursos.ToList() on dm.idgestionCurso equals gc1.idGestionCurso // on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on dm.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc1.idAula equals a.idAula
                    join h in db.Horarios.ToList() on dm.idHorario equals h.idHorario
                    join e in db.Usuario.ToList() on Usuario.idUsuario equals e.idUsuario
                    join p in db.Usuario.ToList() on dm.idProfesor equals p.idUsuario
                    where idDetalleMatricula.Contains(dm.idDetalleMatricula)
                    select new Vista_CursoMatriculable()
                    {
                        FechaInicio = gc1.fechaInicio,
                        FechaFinal = gc1.fechaFinal,
                        Aula = a.numeroAula,
                        Horario = h.descripcion,
                        Curso = c.nombre,
                        fechaActual = dm.fechaRegistro,
                        idGC = dm.idgestionCurso,
                        nombreE = e.nombrecompleto,
                        cedula = e.cedula,
                        profesor = p.nombrecompleto,
                    };

            return new PdfResult(l.ToList(), "Comprobante");

        }
        [Authorize(Roles = "Estudiante")]
        public ActionResult Matricular(int idGestionCurso) {

            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            var gc = db.GestionCursos.Find(idGestionCurso);
            var cu = db.Cursos.Find(gc.idCurso);
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
            dm.costo = cu.costo;
            db.DetalleMatriculas.Add(dm);
            db.SaveChanges();
            var l = //from dm in db.DetalleMatriculas.ToList()
                    from gc1 in db.GestionCursos.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on gc1.idCurso equals c.idCurso
                    join a in db.Aulas.ToList() on gc1.idAula equals a.idAula
                    join h in db.Horarios.ToList() on gc1.idHorario equals h.idHorario
                    join e in db.Usuario.ToList() on Usuario.idUsuario equals e.idUsuario
                    join p in db.Usuario.ToList() on gc1.idUsuario equals p.idUsuario
                    join cup in db.Cupos.ToList() on gc1.idGestionCurso equals cup.idGestionCurso
                    where gc1.idGestionCurso==idGestionCurso
                    select new Vista_CursoMatriculable()
                    {
                        FechaInicio = gc1.fechaInicio,
                        FechaFinal = gc1.fechaFinal,
                        Aula = a.numeroAula,
                        Horario = h.descripcion,
                        Curso = c.nombre,
                        Cupo=cup.cupo,
                        fechaActual = dm.fechaRegistro,
                        idGC = dm.idgestionCurso,
                        nombreE = e.nombrecompleto,
                        cedula = e.cedula,
                        profesor = p.nombrecompleto,
                    };

            return new PdfResult(l.FirstOrDefault(), "Comprobante");
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

        private void ListaDeProfesores(object o = null)
        {
            var l = //from dm in db.DetalleMatriculas.ToList()
                    from u in db.Usuario.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join p in db.Permisos.ToList() on u.idUsuario equals p.idUsuario
                    where p.idUsuario == 3
                    select new Permiso2()
                    {
                        idUsuario = p.idUsuario,
                        idRol = p.idRol,
                        nombrecompleto = u.nombrecompleto,
                    };

            ViewBag.ListaProfesores = new SelectList(l, "idUsuario", "nombrecompleto", o);
        }

        // GET: GestionCursos
        public ActionResult Index()//Metodo que permite visualizar la lista de gestion de cursos desde el menu principal
        {
            var l = //from dm in db.DetalleMatriculas.ToList()
                   from gc in db.GestionCursos.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cursos.ToList() on gc.idCurso equals c.idCurso
                   join a in db.Aulas.ToList() on gc.idAula equals a.idAula
                   join h in db.Horarios.ToList() on gc.idHorario equals h.idHorario
                   join cup in db.Cupos.ToList() on gc.idGestionCurso equals cup.idGestionCurso

                    //where gc.idUsuario == Usuario.idUsuario
                    select new Vista_CursoMatriculable()
                   {
                       FechaInicio = gc.fechaInicio,
                       FechaFinal = gc.fechaFinal,
                       Aula = a.numeroAula + " " + a.tipoAula,
                       Horario = h.descripcion,
                       Curso = c.nombre,
                       Cupo = cup.cupo,
                       idGC = gc.idGestionCurso
                   };
            return View(l);//Vista de los datos de gestion de cursos en forma de lista 
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

            GestionCurso2 gestionCurso2 = new GestionCurso2(gestionCurso.idGestionCurso, gestionCurso.fechaInicio, 
                gestionCurso.fechaFinal, gestionCurso.fechaRegistro, gestionCurso.usuarioCrea, gestionCurso.fechaModifica, 
                gestionCurso.usuarioModifica, db.GestionCursos.Find(gestionCurso.idGestionCurso).cupo,
                db.Aulas.Find(gestionCurso.idAula).numeroAula, db.Cursos.Find(gestionCurso.idCurso).nombre, 
                db.Cursos.Find(gestionCurso.idCurso).descripcion, db.Cursos.Find(gestionCurso.idCurso).costo,
                db.Horarios.Find(gestionCurso.idHorario).dia, db.Horarios.Find(gestionCurso.idHorario).hora,
                db.Horarios.Find(gestionCurso.idHorario).tiempoDuracion, db.Usuario.Find(gestionCurso.idUsuario).nombrecompleto);
            return View(gestionCurso2);//devuelve los datos de la gestion de curso 
        }

        // GET: GestionCursos/Create
        public ActionResult Create()//metodo para crear una nueva pagina con los campos para manejar una nueva gestion de curso
        {
            ListaDeAulas();
            ListaDeCursos();
            ListaDeHorarios();
            ListaDeProfesores();
            return View();//devuelve la vista 
        }

        // POST: GestionCursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//realiza la peticion al servidor
        //Metodo para mostrar los nuevos datos de gestion de curso, usa como parametros los datos ingresados por el usuario
        public ActionResult Create([Bind(Include = "idGestionCurso,idAula,idHorario,idCurso,idUsuario,fechaInicio,fechaFinal,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica,cupo")] GestionCurso gestionCurso)
        {
            var cursos = db.GestionCursos.Where(x => x.idAula == gestionCurso.idAula && x.idHorario == gestionCurso.idHorario).Count();

            if (ModelState.IsValid  && cursos==0)//si el post al servidor se hizo
            {

                db.GestionCursos.Add(gestionCurso);//se agrega la gestion de curso a la bd
                db.SaveChanges();//y guarda los cambios en la db 
                Cupo cu = new Cupo();
                cu.fechaModifica = DateTime.Today;
                cu.fechaRegistro = DateTime.Today;
                cu.usuarioCrea = User.Identity.Name;
                cu.usuarioModifica = User.Identity.Name;
                cu.idGestionCurso = gestionCurso.idGestionCurso;
                cu.cupo = gestionCurso.cupo;
                db.Cupos.Add(cu); ;
                db.SaveChanges();
                
                return RedirectToAction("Index");//devuelve al usuario al inicio 
            }
            ListaDeAulas();
            ListaDeCursos();
            ListaDeHorarios();
            ListaDeProfesores();
            ListaDeProfesores();
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
        public ActionResult Edit([Bind(Include = "idGestionCurso,fechaInicio,fechaFinal,idAula,idCurso,idHorario,idUsuario,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica,cupo")] GestionCurso gestionCur)
        {
            ListaDeAulas(gestionCur.idAula);
            ListaDeCursos(gestionCur.idCurso);
            ListaDeHorarios(gestionCur.idHorario);
            ListaDeUsuarios(gestionCur.idUsuario);
            var cursos = db.GestionCursos.Where(x => x.idAula == gestionCur.idAula && x.idHorario == gestionCur.idHorario).Count();

            if (ModelState.IsValid && cursos == 0)//si el post al servidor se hizo
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
            var cantidaddematricula = db.DetalleMatriculas.Where(x => x.idgestionCurso == id).Count();
            if (cantidaddematricula > 0)
                return View("Noeliminar");
            return View(gestionCurso);//devuelve los datos de la gestion de curso 
        }

        // POST: GestionCursos/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion al servidor para eliminar 
        //metodo que recibe como parametro el id de la gestion de curso ingresada por el usuario para confirmar la eliminacion en la bd
        public ActionResult DeleteConfirmed(int id)
        {
            GestionCurso gestionCurso = db.GestionCursos.Find(id);//busca el numero de la gestion de curso en la bd 

            var l = //from dm in db.DetalleMatriculas.ToList()
                    from g in db.GestionCursos.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join c in db.Cupos.ToList() on g.idGestionCurso equals c.idGestionCurso
                    where c.idGestionCurso == id
                    select new CupoDelete()
                    {
                        idGestionCurso = c.idGestionCurso,
                        idCupo = c.idCupo,
                    };

            Cupo cupo = db.Cupos.Find(l.FirstOrDefault().idCupo);
            db.Cupos.Remove(cupo);
            db.SaveChanges();

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
