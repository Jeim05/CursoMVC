using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
           Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null) {
                ViewBag.Error = "Correo o contraseña incorrecta";
                return View();
            }
            else
            {
                if (oUsuario.Reestablecer) {
                    TempData["IdUsuario"] = oUsuario.IdUsuario;
                    return RedirectToAction("CambiarClave");
                }

                ViewBag.Error = null;
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idUsuario, string claveActual, string nuevaClave, string confirmarClave)
        {
          Usuario oUsuario = new Usuario();
          oUsuario = new CN_Usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idUsuario) ).FirstOrDefault();

          if(oUsuario.Clave != CN_Recursos.ConvertirSha256(claveActual)) {
             TempData["IdUsuario"] = oUsuario.IdUsuario;
             ViewBag.Error = "La contraseña actual no es correcta";
             return View();
          }
          else if(nuevaClave != confirmarClave) {
             TempData["IdUsuario"] = oUsuario.IdUsuario;
             ViewBag.Error = "Las contraseñas no coinciden";
             return View();
          }
         
            return View();
        }
    }
}
