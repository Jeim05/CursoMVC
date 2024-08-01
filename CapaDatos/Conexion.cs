using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        public static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();  /* Ese nombre cadena proviene desde el Web.config, que es el nombre que le asignammos a la conexion */

    }
}
