using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public Dashboard verDashboard()
        {
            Dashboard objeto = new Dashboard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                   
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;  
                    oconexion.Open();

                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            objeto = new Dashboard()
                            {
                                TotalCliente = Convert.ToInt32(reader["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(reader["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(reader["TotalPoducto"]),
                            }; 
                        }
                    }
                }
            }
            catch
            {
                objeto = new Dashboard();
            }

            return objeto;
        }
    }
}
