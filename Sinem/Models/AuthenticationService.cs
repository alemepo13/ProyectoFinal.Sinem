using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;


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
    public void SignIn(Usuario U)
        {
            var db = new SinemDBContext();
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, U.nombre)
            };
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
    public void SignOut()
        {
            IOwinContext context= _context.Request.GetOwinContext();
            var am = context.Authentication;
            am.SignOut(AuthenticationType);
            
        }
    }
    
}