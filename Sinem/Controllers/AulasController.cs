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
    public class AulasController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos

        // GET: Aulas
        public ActionResult Index()//metodo que muestra en la pagina principal una la lista de aulas que estan en la DB
        {
            return View(db.Aulas.ToList());//vista de las aulas en forma de lista
        }

        // GET: Aulas/Details/5
        public ActionResult Details(int? id)//metodo que recibe como parametro un numero de aula para buscarlo en la DB
            //y mostrar en una pagina nueva sus detalles
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aula aula = db.Aulas.Find(id);//busca el numero de aula en la DB
            if (aula == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(aula);//devuelve los datos de esa aula
        }

        // GET: Aulas/Create
        public ActionResult Create()//metodo para crear una pagina, con los campos para crear una nueva aula
        {
            return View();//devuelve la vista
        }

        //En esta seccion es donde está desarrollado los metodos de cada una de las clases con los respectivos atributos 

        public JsonResult NumAula(string numeroAula)
        {
            return Json(!db.Aulas.Any(x => x.numeroAula == numeroAula),
                                                 JsonRequestBehavior.AllowGet);
        }


        // POST: Aulas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Create([Bind(Include = "idAula,numeroAula,tipoAula,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Aula aula)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos e la nueva aula,
            //lleva como parametros los datos de la nueva aula, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Aulas.Add(aula);//agrega el aula a la DB
                aula.estado = "activo";
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }

            return View(aula);//devuelve los datos de esa aula
        }

        // GET: Aulas/Edit/5
        public ActionResult Edit(int? id)//metodo que recibe como parametro un numero de aula para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aula aula = db.Aulas.Find(id);//busca el numero de aula en la DB
            if (aula == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            return View(aula);//devuelve los datos de esa aula
        }

        // POST: Aulas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        public ActionResult Edit([Bind(Include = "estado,idAula,numeroAula,tipoAula,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Aula classes)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados del aula,
            //lleva como parametros los datos a editar del aula, ingresados por un usuario
            //var NumerosAula = db.Aulas.Where(x => x.numeroAula == classes.numeroAula).Count();

            if (ModelState.IsValid /*&& NumerosAula==1*/)//si el post al servidor se hizo 
            {
                db.Entry(classes).State = EntityState.Modified;//modifica los datos  del aula a la DB
                //classes.estado = "activo";
                db.SaveChanges();//guarda los cambios de la DB
                //aqui se debe agregar un manejo de error para los numeros de aula repetidos
                return RedirectToAction("Index");//lo devuelve al inicio
            }
            return View(classes);//devuelve los datos de esa aula
        }

        // GET: Aulas/Delete/5
        public ActionResult Delete(int? id)//metodo que recibe como parametro un numero de aula para buscarlo en la DB
        {
            if (id == null)// si el numero es nulo
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aula aula = db.Aulas.Find(id);//busca el numero de aula en la DB
            if (aula == null)// si el numero es nulo
            {
                return HttpNotFound();
            }
            var gestion = db.GestionCursos.Where(x => x.idAula == id).Count();
            if (gestion > 0)
                return View("Noeliminar");
            if (aula.estado == "activo")
                aula.estado = "inactivo";
            else aula.estado = "activo";
            db.SaveChanges();
            return RedirectToAction("Index");
            //devuelve los datos de esa aula
        }

        // POST: Aulas/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion de borrar al servidor
        public ActionResult DeleteConfirmed(int id)//metodo que recibe como parametro un numero de aula para confirmar su eliminacion
            //de la DB
        {
            Aula aula = db.Aulas.Find(id);//busca el numero de aula en la DB
            db.Aulas.Remove(aula);//elimina el aula
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
