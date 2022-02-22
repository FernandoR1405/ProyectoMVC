using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Supermercado_APP.Models;

namespace Supermercado_APP.Controllers
{
    public class LoginController : Controller
    {
        SupermercadoDBEntities db = new SupermercadoDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Usu_UsuarioNombre, string txtpass)
        {
            var usuario = db.UDP_Inicio_Sesion(Usu_UsuarioNombre, txtpass).ToList();

            if (usuario.Count > 0)
            {
                foreach (var item in usuario)
                {
                    Session["UsuarioId"] = item.Usu_Id;
                    Session["NombreUsuario"] = item.Usu_UsuarioNombre;
                }
                return RedirectToAction("Principal");
            }
            else
            {
                ModelState.AddModelError("Usu_UsuarioNombre", "El Usuario o contrasena son incorrectas");
                return View();
            }
        }

        public ActionResult Principal()
        {
            return View();
        }



    }
}