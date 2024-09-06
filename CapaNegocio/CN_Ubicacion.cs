using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;

namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDatos = new CD_Ubicacion();

        public List<Provincia> ObtenerProvincia()
        {
            return objCapaDatos.ObtenerProvincia();
        }

        public List<Canton> ObtenerCanton(string idprovincia)
        {
            return objCapaDatos.ObtenerCanton(idprovincia);
        }

        public List<Distrito> ObtenerDistrito(string idprovincia, string idcanton)
        {
            return objCapaDatos.ObtenerDistrito(idprovincia, idcanton);
        }
    }
}
