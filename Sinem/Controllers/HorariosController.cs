﻿using System;
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
    public class HorariosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos

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
            return View();//devuelve la vista
        }

        // POST: Horarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]//para realizar la peticion al servidor
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHorario,dia,hora,tiempoDuracion,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Horario horario)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos del nuevo horario,
            //lleva como parametros los datos del nuevo horario, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Horarios.Add(horario);//agrega el horario a la DB
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
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHorario,dia,hora,tiempoDuracion,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Horario horario)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados del horario,
            //lleva como parametros los datos a editar del horario, ingresados por un usuario
            if (ModelState.IsValid)//si el post al servidor se hizo 
            {
                db.Entry(horario).State = EntityState.Modified;//modifica los datos  del horario a la DB
                db.SaveChanges();//guarda los cambios de la DB
                return RedirectToAction("Index");//lo devuelve al inicio
            }
            return View(horario);//devuelve los datos de ese horario
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
            return View(horario);//devuelve los datos de ese horario
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]//para realizar la peticion de borrar al servidor
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)//metodo que recibe como parametro un numero de horario para confirmar su eliminacion
            //de la DB
        {
            Horario horario = db.Horarios.Find(id);//busca el numero de horario en la DB
            db.Horarios.Remove(horario);//elimina el horario
            db.SaveChanges();//guarda los cambios de la DB
            return RedirectToAction("Index");//lo devuelve al inicio
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
