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
    [Authorize(Roles ="Administrador")]
    public class UsuariosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos 

        // GET: Usuarios
        [Authorize (Roles ="Administrador,Empleado")]
        public ActionResult Index()//muestra en la pagina principal la lista de usuarios 
        {
            return View(db.Usuario.ToList());//devuelve los usuarios en forma de lista
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)//metodo que recibe como parametro un numero de usuario para buscarlo en la DB
                                            //y mostrar en una pagina nueva sus detalles
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//Si no existe devuelve un bad request
            }
            Usuario usuario = db.Usuario.Find(id);//busca el id del usuario dentro de la DB
            if (usuario == null)
            {
                return HttpNotFound();//si no existe el id devuelve una pantalla de error
            }
            return View(usuario);//Devuelve los datos del usuario correspondiente 
        }

        // GET: Usuarios/Create
        public ActionResult Create()//metodo para crear una pagina, con los campos para crear un nuevo usuario
        {
            return View();//devuelve la vista 
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "idUsuario,idDireccion,cedula,nombre,apellido,telefono,correo,fechaNacimiento,usuario,contraseña,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Usuario usuario)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos del nuevo  usuario,
            //lleva como parametros los datos del nuevo usuario, ingresados manualmente 
            if (ModelState.IsValid)//Si se realizo el post al servidor 
            {
                db.Usuario.Add(usuario);//Agrega el horario a la BD
                db.SaveChanges();//GUarda los cambios en la BD
                return RedirectToAction("Index");//Redirige al usuario a la pagina principal 
            }

            return View(usuario);//devuelve los datos de ese usuario
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)//metodo que recibe como parametro un numero de horario para buscarlo en la DB
        {
            if (id == null)//SI es nulo devuelve un bad request
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);//busca el id de usuario dentro de la BD
            if (usuario == null)//si es nulo devuelve una pantalla de error
            {
                return HttpNotFound();
            }
            return View(usuario);//devuelve los datos del usuario deseado 
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "idUsuario,idDireccion,cedula,nombre,apellido,telefono,correo,fechaNacimiento,usuario,contraseña,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Usuario user)
        {//metodo para crear una pagina nueva en donde se van a mostrar los datos actualizados del usuario,
            //lleva como parametros los datos a editar del usuario, ingresados manualmente
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;//modifica los datos en la base de datos 
                user.fechaModifica = DateTime.Now;//Asigna la fecha actual como ultima modificacion 
                user.usuarioModifica = User.Identity.Name;//Identifica al usuario que realizo la modificacion 
                db.SaveChanges();//guarda los datos dentro de la DB 
                return RedirectToAction("Index");//redirige al usuario al menu principal
            }
            return View(user);//devuelve los datos del usuario deseado 
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
