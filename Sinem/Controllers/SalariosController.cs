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
        private SinemDBContext db = new SinemDBContext(); 

        // GET: Salarios
        public ActionResult Index()
        {
            return View(db.Salarios.ToList());
        }

        // GET: Salarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salario salario = db.Salarios.Find(id);
            if (salario == null)
            {
                return HttpNotFound();
            }
            return View(salario);
        }
    }
}
