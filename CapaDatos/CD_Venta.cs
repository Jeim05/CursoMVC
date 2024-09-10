using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Venta
    {
        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
           bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVenta", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdCliente", obj.IdCliente);
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotalProducto);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("Contacto", obj.Contacto);
                    cmd.Parameters.AddWithValue("IdDistrito", obj.IdDistrito);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("IdTransaccion", obj.IdTransaccion);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output; 
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output; 
                    cmd.CommandType = CommandType.StoredProcedure; 

                    conexion.Open();
                    cmd.ExecuteNonQuery(); 
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value); 
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString(); 
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        
        
    }
}
