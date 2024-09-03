using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Scripts
{
    public class CD_Marcas
    {
        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "select IdMarca, Descripcion, Activo from MARCA";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();


                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new Marca
                                {
                                    IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Marca>();
            }

            return lista;
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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

        public bool Editar(Marca obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdMarca", obj.IdMarca);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
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
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdMarca", id);
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


        public List<Marca> ListarMarcaPorCategoria(int idcategoria)
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select distinct m.IdMarca, m.Descripcion from PRODUCTO p");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");
                    sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca and m.Activo = 1");
                    sb.AppendLine("where c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcategoria ", idcategoria);
                    cmd.CommandType = CommandType.Text; 
                    oconexion.Open();

                          
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new Marca
                                {
                                    IdMarca = Convert.ToInt32(reader["IdMarca"]),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Marca>();
            }

            return lista;
        }
    }
}
