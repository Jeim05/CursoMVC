using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objCapaDatos = new CD_Reporte();

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            return objCapaDatos.Ventas(fechainicio, fechafin, idtransaccion);
        }



        public Dashboard VerDashboard()
        {
            return objCapaDatos.VerDashboard(); 
        }
    }
}
