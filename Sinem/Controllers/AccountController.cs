using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Sinem.Models;

namespace Sinem.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Login() // en este metodo aqui se puede visualizar  la pantalla del login con sus respectivos campos que son el usuario y la contraseña que esta en la DB
        {
            return View();
        }


    /*  
      public JsonResult Login(string user)
      {
         System.Threading.Thread.Sleep(200);
         var SeachData = db.SinemDBContext.Where(x => x.StuName == user).SingleOrDefault();
         if (SeachData != null)
         {
             return Json(1);
         }
         else
         {
             return Json(0);
         }

     }

    */

    /*

     [AllowAnonymous]
     public string CheckUserName(string input)
     {
         bool ifuser = WebSecurity.UserExists(input);

         if (ifuser == false)
         {
             return "Available";
         }

         if (ifuser == true)
         {
             return "Not Available";
         }

         return "";
     }

     */

    [HttpPost()]

        public ActionResult Login(string user, string pass) //en este metodo que recibe como parametro el usuario y la contraseña del account para buscarlo en la base de datos
        // aparte de  mostrar tambien en una pagina nueva con sus respectivos detalles
        {
            var acs = new AuthenticationService(ControllerContext.HttpContext);
            var db = new SinemDBContext(); // se envian o verifican los datos en la db
            if (ModelState.IsValid) // aqui se puede ver si los datos son validos
            {
                var Usuario = (from U in db.Usuario where U.usuario == user select U).FirstOrDefault();
                if(Usuario!= null && Usuario.contraseña==pass)
                {
                    acs.SignIn(Usuario);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(); // devuelve los datos
        }
        public ActionResult Logout() // aqui se puede cerrar sesion
        {
            var acs =new  AuthenticationService(ControllerContext.HttpContext);
            acs.SignOut();
            return RedirectToAction("Login"); 

        }
    }
}