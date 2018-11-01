using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
//importaciones

namespace Sinem.Models
{
    public class AuthenticationService
    {
        private readonly HttpContextBase _context;
        private const string AuthenticationType = "ApplicationCookie";

    public AuthenticationService(HttpContextBase context)
        {
            _context = context;
        }
    public void SignIn(Usuario U)//metodo para iniciar sesion con el usuario como parametro
        {
            var db = new SinemDBContext();//establece conexion con la DB
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, U.nombre)
            };
            //consulta la tabla de roles para verificar si el usuario se encuentra en ella
            var Roles = from P in db.Permisos
                        join R in db.Roles
                        on P.idRol equals R.idRol
                        where P.idUsuario == U.idUsuario
                        select R;
            foreach (var item in Roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, item.nombre));
            }


            ClaimsIdentity identity = new ClaimsIdentity(claim, AuthenticationType);
            IOwinContext context = _context.Request.GetOwinContext();
            var am = context.Authentication;
            am.SignIn(identity);
        }
    public void SignOut()//metodo para cerrar sesion
        {
            IOwinContext context= _context.Request.GetOwinContext();
            var am = context.Authentication;
            am.SignOut(AuthenticationType);
            
        }
    }
    
}