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
    }
}
