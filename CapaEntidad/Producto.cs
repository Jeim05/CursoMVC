using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }
        public string PrecioTexto { get; set; } // Guarda el precio en formato texto segun la region
        public int Stock { get; set; }
        public string RutaImagen { get; set;}
        public string NombreImagen { get; set;}
        public bool Activo { get; set; }
        public string Base64 { get; set; } // Va a almacenar todo el valor de la imagen
        public string Extension { get; set; } //Guarda la extensión de la imagen que vamos a guardar

    }
}
