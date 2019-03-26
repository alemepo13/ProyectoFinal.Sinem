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
    public class SalariosController : Controller
    {
        private SinemDBContext db = new SinemDBContext();//conexion a la base de datos 

        public ActionResult ConsultaSalarioProfesor()
        {
            var Usuario = (from U in db.Usuario where U.nombre == User.Identity.Name select U).FirstOrDefault();
            var L = db.Salarios.Where(x => x.idUsuario == Usuario.idUsuario);
            return View(L);
        }

        // GET: Salarios
        public ActionResult Index()//metodo que muestra en pantalla la lista de salarios que se encuentra en la DB
        {
            ViewBag.ListaProfesores = //from dm in db.DetalleMatriculas.ToList()
                    from u in db.Usuario.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join p in db.Permisos.ToList() on u.idUsuario equals p.idUsuario
                    select new Permiso2()
                    {
                        idUsuario = p.idUsuario,
                        idRol = p.idRol,
                        nombrecompleto = u.nombrecompleto,
                    };
            return View(db.Salarios.ToList());//Vista de los salarios en forma de lista
        }
        private void ListaDeProfesores(object o = null)
        {
            var l = //from dm in db.DetalleMatriculas.ToList()
                    from u in db.Usuario.ToList() // on dm.idgestionCurso equals gc.idGestionCurso
                    join p in db.Permisos.ToList() on u.idUsuario equals p.idUsuario
                    where p.idRol == 4
                    select new Permiso2()
                    {
                        idUsuario = p.idUsuario,
                        idRol = p.idRol,
                        nombrecompleto = u.nombrecompleto,
                    };

            ViewBag.ListaProfesores = new SelectList(l, "idUsuario", "nombrecompleto", o);
        }


        public ActionResult Create() {
            ListaDeProfesores();
            return View();

        }
        [HttpPost]
        public ActionResult Create(Salario m) {
            if (ModelState.IsValid) {

                m.fechaModifica = DateTime.Today;
                m.fechaRegistro = DateTime.Today;
                m.usuarioCrea = User.Identity.Name;
                m.usuarioModifica = User.Identity.Name;
                db.Salarios.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ListaDeProfesores();
            return View(m);
        }

        // GET: Salarios/Details/5
        public ActionResult Details(int? id)//metodo que recibe como parametro un numero de salario para buscarlo en la DB
            //y mostrar en una pagina nueva sus detalles
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//si no existe devuelve un mensaje de bad request
            }
            Salario salario = db.Salarios.Find(id);//busca el id del salario en la DB
            if (salario == null)
            {
                return HttpNotFound();//si no se encuentra devuelve un mensaje de error 
            }
            return View(salario);//devuelve los datos del salario con el id correspondiente 
        }
    }
}
