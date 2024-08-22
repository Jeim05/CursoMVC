using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        // Devolvemos la lista de usuarios que viene desde la capa de Negocio
        [HttpGet]
        public JsonResult ListarUsuarios() {
            List<Usuario> oLista = new List<Usuario>();
            oLista = new CN_Usuarios().Listar();
             
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet); // Se hace de esta manera, porque así se puede usar en la tata table
        }

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado; // es de tipo object, dado a que va a variar si es editar o registrar y va a tomar un tipo de dato bool o int
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)
            { // Validamos que el Id sea 0
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje); // Se llama al metodo de la capa de negocio para registrar el usuario

            }
            else { 
                resultado= new CN_Usuarios().Editar(objeto, out mensaje);
            }

            // Se devuelve la logica obtenida de acuerdo al metodo que se haya ejecutado, registrar o editar
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VistaDashboard()
        {
            Dashboard objeto = new CN_Reporte().VerDashboard();
            return Json(new {resultado = objeto}, JsonRequestBehavior.AllowGet);
        }

     }
}