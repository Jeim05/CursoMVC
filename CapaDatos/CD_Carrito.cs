using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Carrito
    {
        public bool ExisteCarrito(int idcliente, int idproducto)
        {
            bool resultado = true;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_ExisteCarrito", conexion);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }
            return resultado;
        }


        public bool OperacionCarrito(int idcliente, int idproducto, bool suma, out string Mensaje)
        {
            bool resultado = true;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_OperacionCarrito", conexion);
                    cmd.Parameters.AddWithValue("IdCliente", idcliente);
                    cmd.Parameters.AddWithValue("IdProducto", idproducto);
                    cmd.Parameters.AddWithValue("Sumar", suma);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        //  SELECT COUNT(*) FROM CARRITO WHERE IdCliente = 1

        public int CantidadEnCarrito(int idcliente)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CARRITO WHERE IdCliente = @idcliente", connection);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    resultado = Convert.ToInt32( cmd.ExecuteScalar()); 
                }
            }
            catch (Exception ex)
            {

                resultado = false;
            }
            return resultado;
        }
    }
}
