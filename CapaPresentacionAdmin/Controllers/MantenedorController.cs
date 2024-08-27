using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;

using System.Security.AccessControl;

namespace CapaPresentacionAdmin.Controllers
{
    [Authorize]
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

        /*****  METODOS CATEGORIAS *****/

        #region CATEGORIA
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
        #endregion

        /*****  METODOS MARCAS *****/
        #region MARCAS
        [HttpGet]
        public JsonResult ListarMarcas()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = new CN_Marca().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet); // Se hace de esta manera, porque así se puede usar en la tata table
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            object resultado; // es de tipo object, dado a que va a variar si es editar o registrar y va a tomar un tipo de dato bool o int
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            { // Validamos que el Id sea 0
                resultado = new CN_Marca().Registrar(objeto, out mensaje);

            }
            else
            {
                resultado = new CN_Marca().Editar(objeto, out mensaje);
            }

            // Se devuelve la logica obtenida de acuerdo al metodo que se haya ejecutado, registrar o editar
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Marca().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /***** METODOS PRODUCTOS *****/
        #region PRODUCTOS
        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto); // Convertimos el producto en texto a objeto

            decimal precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CR"), out precio))
            {
                oProducto.Precio = precio;
            }
            else
            {
                return Json(new {operacionExitosa=false, mensaje = "El formato del precio debe ser ##.##"}, JsonRequestBehavior.AllowGet);
            }


            if (oProducto.IdProducto == 0)
            {
                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);

                if (idProductoGenerado !=0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }

            }
            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }

            // Si la siguiente condición se cumple entonces procede a guardar la imagen
            if (operacion_exitosa)
            {
                if(archivoImagen != null)
                {
                    // Logica para guardar la imagen en la carpeta
                    /* Agregamos en el webConfig la configuración de la ruta de la carpeta donde guardar la imagen 
                     y hacemos la referencia de la configuración aqui*/

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(),extension);
                    /*DirectoryInfo di = new DirectoryInfo(ruta_guardar);
                    DirectorySecurity ds = di.GetAccessControl();
                    ds.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    di.SetAccessControl(ds);*/

                    try
                    {
                      
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar));

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }

                }
            }

            return Json(new { operacionExitosa = operacion_exitosa, idGenerado = oProducto.IdProducto , mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen,oProducto.NombreImagen), out conversion);

            return Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oProducto.NombreImagen)
            },
             JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
