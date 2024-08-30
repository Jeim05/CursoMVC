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
    public class CD_Cliente
    {
        public int Registrar(Cliente obj, out string Mensaje)
        {
            int idAutogenerado = 0; // Se recibe el id del Cliente al momento de hacer el registro
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCliente", conexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output; // Parametros de salida 
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output; // Parametros de salida
                    cmd.CommandType = CommandType.StoredProcedure; // Se indica que es un procedimiento almacenado

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    idAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value); // Retornamos el parametro de salida del Procedimiento
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString(); // Se obtiene el mensaje del procedimiento almacenado
                }
            }
            catch (Exception ex)
            {
                idAutogenerado = 0;
                Mensaje = ex.Message;
            }
            return idAutogenerado;
        }
        
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "select IdCliente, Nombres, Apellidos, Correo, Clave, Reestablecer FROM cliente";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();


                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new Cliente()
                                {
                                    IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(reader["Reestablecer"]),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Cliente>();
            }

            return lista;
        }


        public bool CambiarClave(int idCliente, string nuevaClave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clave = @nuevaClave, reestablecer = 0 where IdCliente = @id", connection);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@nuevaClave", nuevaClave);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false; // decimos que si el numero de filas afectadas es mayor a 0 va a ser true, caso contrario hubo un problema en eliminar
                }
            }
            catch (Exception ex)
            {

                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool ReestablecerClave(int idCliente, string clave, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("update Cliente set clave = @clave, reestablecer = 1 where IdCliente = @id", connection);
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();

                    // decimos que si el numero de filas afectadas es mayor a 0 va a ser true, caso contrario hubo un problema en eliminar
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
