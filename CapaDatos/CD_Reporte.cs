using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CapaDatos
{
    public class CD_Reporte
    {
        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure; 
                    oconexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = reader["FechaVenta"].ToString(),
                                    Cliente = reader["Cliente"].ToString(),
                                    Producto = reader["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"],new CultureInfo("es-CR")),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(reader["Total"], new CultureInfo("es-CR")),
                                    IdTransaccion = reader["IdTransaccion"].ToString()
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Reporte>();
            }

            return lista;
        }


        public Dashboard VerDashboard()
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
                                TotalProducto = Convert.ToInt32(reader["TotalProducto"]),
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
