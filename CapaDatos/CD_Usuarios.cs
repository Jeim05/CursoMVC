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
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List < Usuario > lista = new List<Usuario>();

            try {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion)){
                    string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();


                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read())
                        {
                            lista.Add(
                                new Usuario
                                {
                                    IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                                    Nombres = reader["Nombres"].ToString(),
                                    Apellidos = reader["Apellidos"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Reestablecer = Convert.ToBoolean(reader["Reestablecer"]),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                });
                        }
                    }
                }
            }
            catch { 
            lista = new List<Usuario>();
            }

            return lista;
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idAutogenerado = 0; // Se recibe el id del usuario al momento de hacer el registro
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
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

        public bool Editar(Usuario obj, out string Mensaje)
        {
           bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuarios", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output; // Parametros de salida 
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output; // Parametros de salida
                    cmd.CommandType = CommandType.StoredProcedure; // Se indica que es un procedimiento almacenado

                    conexion.Open(); // Se abre la conexión 
                    cmd.ExecuteNonQuery(); // Se ejecuta el procedimiento
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value); // Retornamos el parametro de salida del Procedimiento
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString(); // Se obtiene el mensaje del procedimiento almacenado
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje= string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("delete top(1) from usuario where IdUsuario = @id", connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    connection.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                resultado=false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
