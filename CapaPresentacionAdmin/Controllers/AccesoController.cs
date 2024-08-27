using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

using System.Web.Security;

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

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);

                ViewBag.Error = null;
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveActual, string nuevaClave, string confirmarClave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idusuario)).FirstOrDefault();

            if (oUsuario.Clave != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vactual"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vactual"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vactual"] = "";
            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Usuarios().CambiarClave(int.Parse(idusuario), nuevaClave, out mensaje);
            if (respuesta) {
              return RedirectToAction("Index");
            }
            else{
               TempData["IdUsuario"] = idusuario;
                ViewBag.Error = mensaje;
                return View(); // Retorna la misma vista en la que estamos
                
            }

        }

       [HttpPost]
       public ActionResult Reestablecer(string correo){
       Usuario oUsuario = new Usuario();
       oUsuario = new CN_Usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();

       if(oUsuario == null){
       ViewBag.Error = "No se encontró un usuario relacionado a ese correo";
        return View();
       }

        string mensaje = string.Empty;
        bool respuesta = new CN_Usuarios().ReestablecerClave(oUsuario.IdUsuario, correo, out mensaje);

         if (respuesta) {
       ViewBag.Error = null;
        return RedirectToAction("Index", "Acceso");
        }
        else{
         ViewBag.Error = mensaje;
         return View();
        }
       }

       public ActionResult CerrarSesion(){
            FormsAuthentication.SignOut();
         return RedirectToAction("Index", "Acceso");
       }

       
    }
}
