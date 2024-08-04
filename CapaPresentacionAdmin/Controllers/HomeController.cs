using System;
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



    }
}