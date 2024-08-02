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
        
        public List<Usuario> Listar()
        {
            return objCapaDatos.Listar(); // Metodo que devuelve la lista con los usuarios registrados
        }
    }
}
