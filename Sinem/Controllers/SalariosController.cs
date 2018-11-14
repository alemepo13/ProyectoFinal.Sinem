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

        // GET: Salarios
        public ActionResult Index()//metodo que muestra en pantalla la lista de salarios que se encuentra en la DB
        {
            return View(db.Salarios.ToList());//Vista de los salarios en forma de lista
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
