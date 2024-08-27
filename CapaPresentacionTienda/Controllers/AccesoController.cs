using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Registrar(Cliente objeto)
        {
          int resultado;
          string mensaje = string.Empty;

          ViewData["Nombres"] = string.IsNullOrEmpty(objeto.Nombres) ? "" : objeto.Nombres;
          ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Nombres) ? "" : objeto.Apellidos;
          ViewData["Correo"] = string.IsNullOrEmpty(objeto.Nombres) ? "" : objeto.Correo;

          if(objeto.Clave != objeto.ConfirmarClave){
          ViewBag.Error = "Las contraseñas no coinciden";
          return View();
          }
          resultado = new CN_Cliente().Registrar(objeto, out mensaje);

          if(resultado > 0){
            ViewBag.Error = null;
            return RedirectToAction("Index","Acceso");
          }else{
           ViewBag.Error = mensaje;
           return View();
          }
        }


        [HttPost]
        public ActionResult Index(string correo, string clave)
        {
         Cliente oCliente = null;
         oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo && item.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

         if(oCliente){
            ViewBag.Error = "Correo o contraseña incorrecta";
           return View();
         }else{
           if(oCliente.Reestablecer){
             TempData["IdCliente"] = oCliente.IdCliente;
              return RedirectToAction("CambiarClave","Acceso");
           }
           else{
           FormsAuthentication.SetAuthCookie(oCleinte.Correo, false);
           Session["Cliente"]; = oCliente  // Guarda la información del cliente a través de todo el proyecto
           ViewBag.Error = null;
            return RedirectToAction("Index","Tienda");
           }
         }
         
            return View();
        }


        [HttPost]
        public ActionResult Reestablecer()
        {
            Cliente oUsuario = new Cliente();
           oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo).FirstOrDefault();

       if(oCliente == null){
       ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
        return View();
       }

        string mensaje = string.Empty;
        bool respuesta = new CN_Cliente().ReestablecerClave(oCliente.IdCliente, correo, out mensaje);

         if (respuesta) {
       ViewBag.Error = null;
        return RedirectToAction("Index", "Acceso");
        }
        else{
         ViewBag.Error = mensaje;
         return View();
        }
        }

         [HttPost]
        public ActionResult CambiarClave(string idcliente, string claveActual, string nuevaClave, string confirmarClave)
        {
           Cliente oCliente = new Cliente();
            oCliente = new CN_Cliente().Listar().Where(u => u.IdUsuario == int.Parse(idcliente)).FirstOrDefault();

            if (oCliente.Clave != CN_Recursos.ConvertirSha256(claveActual))
            {
                TempData["IdCliente"] = idusuario;
                ViewData["vactual"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaClave != confirmarClave)
            {
                TempData["IdCliente"] = idcliente;
                ViewData["vactual"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }

            ViewData["vactual"] = "";
            nuevaClave = CN_Recursos.ConvertirSha256(nuevaClave);

            string mensaje = string.Empty;
            bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idcliente), nuevaClave, out mensaje);
            if (respuesta) {
              return RedirectToAction("Index");
            }
            else{
               TempData["IdCliente"] = idcliente;
                ViewBag.Error = mensaje;
                return View(); // Retorna la misma vista en la que estamos
                
            }
        }

          public ActionResult CerrarSesion(){
            FormsAuthentication.SignOut();
         return RedirectToAction("Index", "Acceso");
       }
        
    }
}
