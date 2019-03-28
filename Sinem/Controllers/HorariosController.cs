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
    public class HorariosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos
        private void ListaDeTipos(string tipo = null)
        {
            ViewBag.ListaDeTipos = new SelectList(new List<string> { "min", "h" }, "", "", tipo);
        }

        private void ListaDeDias(object eldia = null)
        {
            var l = new List<string> { "Lunes","Martes","Miercoles","Jueves","Viernes","Sabado"};
            ViewBag.Listadias = new SelectList(l, eldia);
        }

        // GET: Horarios
        public ActionResult Index()//metodo que muestra en la pagina principal una la lista de horarios que estan en la DB
        {
            return View(db.Horarios.ToList());//vista de los horarios en forma de lista
        }

        // GET: Horarios/Details/5
        public ActionResult Details(int? id)//metodo que recibe como parametro un numero de horario para buscarlo en la DB
            //y mostrar en una pagina nueva sus detalles
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horario horario = db.Horarios.Find(id);//busca el numero de horario en la DB
            if (horario == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(horario);//devuelve los datos de ese horario
        }

        // GET: Horarios/Create
        public ActionResult Create()//metodo para crear una pagina, con los campos para crear un nuevo horario
        {
            ListaDeDias();
            ListaDeTipos();
            return View();//devuelve la vista
        }

        // POST: Horarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Create([Bind(Include = "idHorario,dia,hora,tiempoDuracion,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Horario horario)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos del nuevo horario,
            //lleva como parametros los datos del nuevo horario, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Horarios.Add(horario);//agrega el horario a la DB
                horario.estado = "activo";
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }

            return View(horario);//devuelve los datos de ese horario
        }

        // GET: Horarios/Edit/5
        public ActionResult Edit(int? id)//metodo que recibe como parametro un numero de horario para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horario horario = db.Horarios.Find(id);//busca el numero de horario en la DB
            ListaDeDias(horario.dia);
            ListaDeTipos(horario.tipo);
            if (horario == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(horario);//devuelve los datos de ese horario
        }

        // POST: Horarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Edit([Bind(Include = "estado,idHorario,dia,hora,tipo,tiempoDuracion,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Horario schedule)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados del horario,
            //lleva como parametros los datos a editar del horario, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Entry(schedule).State = EntityState.Modified;//modifica los datos  del horario a la DB
                //schedule.estado = "activo";
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }
            ListaDeDias(schedule.dia);
            ListaDeTipos(schedule.tipo);
            return View(schedule);//devuelve los datos de ese horario
        }

        // GET: Horarios/Delete/5
        public ActionResult Delete(int? id)//metodo que recibe como parametro un numero de horario para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horario horario = db.Horarios.Find(id);//busca el numero de horario en la DB
            if (horario == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            var gestion = db.GestionCursos.Where(x => x.idHorario == id).Count();
            if (gestion > 0)
                return View("Noeliminar");
            if (horario.estado == "activo")
                horario.estado = "inactivo";
            else horario.estado = "activo";
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion de borrar al servidor
        public ActionResult DeleteConfirmed(int id)//metodo que recibe como parametro un numero de horario para confirmar su eliminacion
            //de la DB
        {
            Horario horario = db.Horarios.Find(id);//busca el numero de horario en la DB
            db.Horarios.Remove(horario);//elimina el horario
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
