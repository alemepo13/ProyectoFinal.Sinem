using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sinem.Models;

namespace Sinem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost()]
        public ActionResult Login(string user, string pass)
        {
            var acs = new AuthenticationService(ControllerContext.HttpContext);
            var db = new SinemDBContext();
            if (ModelState.IsValid)
            {
                var Usuario = (from U in db.Usuario where U.usuario == user select U).FirstOrDefault();
                if(Usuario!= null && Usuario.contraseña==pass)
                {
                    acs.SignIn(Usuario);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            var acs =new  AuthenticationService(ControllerContext.HttpContext);
            acs.SignOut();
            return RedirectToAction("Login");

        }
    }
}