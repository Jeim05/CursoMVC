using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Aqui lo que hacemos es una referencia de la Capa Entidad. Para poder acceder a las clases.
// Esa referencia se agrega posicionandose sobre el proyecto CapaDatos > click derecho > agregar > referencias
using CapaEntidad;

using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Usuarios
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {
                    string query = "SELECT IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo FROM USUARIO";
                    SqlCommand cmd = new SqlCommand (query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    
                    using(SqlDataReader dr = cmd.ExecuteReader()) { 
                        while(dr.Read())
                        {
                            lista.Add(
                                new Usuario() { 
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Reestablecer  = Convert.ToBoolean(dr["Reestablecer"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                });
                        }
                    }

                }
            }
            catch
            {
                lista = new List<Usuario>();
            }

            return lista;
        }
    }
}
