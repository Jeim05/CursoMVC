using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDatos = new CD_Usuarios();
        // Con este metodo podremos acceder a todas las funcionalidades de la capa de datos de usuario. Aqui es como hacer una intancia de esa entidad

        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar();
        }
    }
}
