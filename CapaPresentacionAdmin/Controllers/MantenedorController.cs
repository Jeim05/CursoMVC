using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }

        public ActionResult Producto()
        {
            return View();
        }

         [HttpGet]
        public JsonResult ListarCategorias() {
            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
             
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet); // Se hace de esta manera, porque así se puede usar en la tata table
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            object resultado; // es de tipo object, dado a que va a variar si es editar o registrar y va a tomar un tipo de dato bool o int
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            { // Validamos que el Id sea 0
                resultado = new CN_Categoria().Registrar(objeto, out mensaje); 

            }
            else { 
                resultado= new CN_Categoria().Editar(objeto, out mensaje);
            }

            // Se devuelve la logica obtenida de acuerdo al metodo que se haya ejecutado, registrar o editar
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

          [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        
    }
}
