using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Sinem.Models;


namespace Sinem.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {


        private SinemDBContext db = new SinemDBContext();

        private void ListaDeDirecciones(object direccion = null)
        {
            ViewBag.ListaDirecciones = new SelectList(db.Direcciones.ToList(), "idDireccion", "descripcion", direccion);
        }

        private void ListaDeRoles(object rol = null)
        {
            ViewBag.ListaDeRoles = new SelectList(db.Roles.ToList(), "idRol", "nombre", rol);
        }

        // GET: Usuarios
        [Authorize(Roles = "Administrador,Empleado")]
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        //En esta seccion es donde está desarrollado los metodos de cada una de las clases con los respectivos atributos 

         public JsonResult IsUserNameAvailable(string usuario)
        {
            return Json(!db.Usuario.Any(x => x.usuario == usuario),
                                                 JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cedulas(string cedula)
        {
            return Json(!db.Usuario.Any(x => x.cedula == cedula),
                                                 JsonRequestBehavior.AllowGet);
        }

        public JsonResult Telefonos(int telefono)
        {
            return Json(!db.Usuario.Any(x => x.telefono == telefono),
                                                 JsonRequestBehavior.AllowGet);
        }

        public JsonResult Correos(string correo)
        {
            return Json(!db.Usuario.Any(x => x.correo == correo),
                                                 JsonRequestBehavior.AllowGet);
        }

/*
        public JsonResult logines(string login)
        {
            return Json(!db.Usuario.Any(x => x.login == login),
                                                 JsonRequestBehavior.AllowGet);
        }
*/

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
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

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ListaDeDirecciones();
            ListaDeRoles();
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "idDireccion,cedula,nombre,apellido,telefono,correo,fechaNacimiento,usuario,contraseña,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica,idRol")] Usuario U)
        {
            ListaDeDirecciones();
            ListaDeRoles();
            if (ModelState.IsValid)
            {
                db.Usuario.Add(U);
                db.SaveChanges();
                var U2 = db.Usuario.Where(x => x.cedula == U.cedula).FirstOrDefault();
                db.Permisos.Add(new Permiso { idUsuario = U2.idUsuario, idRol = U.idRol });
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(U);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
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
            ListaDeDirecciones(usuario.idDireccion);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "idUsuario,idDireccion,cedula,nombre,apellido,telefono,correo,fechaNacimiento,usuario,contraseña,fechaRegistro,usuarioCrea,fechaModifica,usuarioModifica")] Usuario user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                user.fechaModifica = DateTime.Now;
                user.usuarioModifica = User.Identity.Name;
                db.SaveChanges();
                return RedirectToAction("Confirmacion","Home",new { mensaje="El usuario ha sido modificado", cont="Usuarios" });
            }
            return View(user);
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
            var matriculas = db.DetalleMatriculas.Where(x => x.idEstudiante == id || x.idProfesor == id).Count();
            //var cursos= db.GestionCursos.Where(x=>x.)
            if (matriculas > 0)
                return View("Noeliminar");
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);

            var l = //from dm in db.DetalleMatriculas.ToList()
                    from u in db.Usuario.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join p in db.Permisos.ToList() on u.idUsuario equals p.idUsuario
                    where p.idUsuario == id
                    select new PermisoDelete()
                    {
                        idUsuario = p.idUsuario,
                        idRol = p.idRol,
                    };

            Permiso permiso = db.Permisos.Find(usuario.idUsuario, l.FirstOrDefault().idRol);
            db.Permisos.Remove(permiso);
            db.SaveChanges();

            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ListaProvincias(string id) {
            string s = "";
            string sel;
            Dictionary<string,string> sl = new Dictionary<string, string>();
            int id1;
            int.TryParse(id, out id1);
            Direccion d = id?.Length > 0 ? db.Direcciones.Where(x => x.idDireccion == id1).FirstOrDefault() : null;
            var lista = db.Direcciones.ToList();
            foreach (var l in lista) {
                if (sl.Keys.Contains(l.provincia)) continue;
                if (d == null) sel = "";
                else sel = d.provincia == l.provincia ? "selected='selected'" : "";
                sl.Add(l.provincia, $"<option value='{l.provincia}' {sel}>{l.provincia}</option>");
            }
            foreach (var i in sl) s += i.Value;
            return Content("<option value=''>Selecciona</option>"+s);
        }

        public ActionResult ListaCantones(string id, string idProv)
        {
            string s = "";
            string sel;
            Dictionary<string, string> sl = new Dictionary<string, string>();
            int id1;
            int.TryParse(id, out id1);
            Direccion d = id?.Length > 0 ? db.Direcciones.Where(x => x.idDireccion == id1).FirstOrDefault() : null;

            if (d == null && idProv.Length <= 0) return Content("");
            var lista = db.Direcciones.ToList();
            foreach (var l in lista)
            {
                if ((d == null && (l.provincia == idProv))
                   || ((d != null) && (l.provincia == d.provincia)))
                {
                    if (sl.Keys.Contains(l.provincia + "_" + l.canton)) continue;
                    if (d == null) sel = "";
                    else sel = d.canton == l.canton ? "selected='selected'" : "";
                    sl.Add(l.provincia + "_" + l.canton, $"<option value='{l.canton}' {sel}>{l.canton}</option>");
                }
            }
            foreach (var i in sl) s += i.Value;
            return Content("<option value=''>Selecciona</option>" + s);
        }
        public ActionResult ListaDistritos(string id, string idProv, string idCant)
        {
            string s = "";
            string sel;
            Dictionary<string, string> sl = new Dictionary<string, string>();
            int id1;
            int.TryParse(id, out id1);
            Direccion d = id?.Length > 0 ? db.Direcciones.Where(x => x.idDireccion == id1).FirstOrDefault() : null;
            if (d == null && idProv.Length <= 0) return Content("");
            var lista = db.Direcciones.ToList();
            foreach (var l in lista)
            {
                if ((d == null && (l.provincia == idProv && l.canton == idCant) ) 
                   || ((d != null) && ((l.provincia == d.provincia && l.canton == d.canton))))
                {
                    if (sl.Keys.Contains(l.provincia + "_" + l.canton + "_" + l.distrito)) continue;
                    if (d == null) sel = "";
                    else sel = d.canton == l.canton ? "selected='selected'" : "";
                    sl.Add(l.provincia + "_" + l.canton + "_" + l.distrito, $"<option value='{l.idDireccion}' {sel}>{l.distrito}</option>");
                }
            }
            foreach (var i in sl) s += i.Value;
            return Content("<option value=''>Selecciona</option>" + s);
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
