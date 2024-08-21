using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CapaDatos
{
    public class CD_Productos
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    StringBuilder sb = new StringBuilder(); //  Esto nos permite hacer saltos de línea
                    sb.AppendLine("SELECT p.IdProducto, p.Nombre, p.Descripcion,");
                    sb.AppendLine("m.IdMarca, m.Descripcion[DesMarca],");
                    sb.AppendLine("c.IdCategoria, c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo");
                    sb.AppendLine("FROM PRODUCTO p");
                    sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();


                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    oMarca = new Marca() { IdMarca = Convert.ToInt32(reader["IdMarca"]), Descripcion = reader["DesMarca"].ToString() },
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(reader["IdCategoria"]), Descripcion = reader["DesCategoria"].ToString() },
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    RutaImagen = reader["RutaImagen"].ToString(),
                                    NombreImagen = reader["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(reader["Activo"]),
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Producto>();
            }

            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idAutogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
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

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
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

        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "update producto set RutaImagen = @rutaimagen, NombreImagen = @nombreImagen where IdProducto = @idproducto";

                    SqlCommand cmd = new SqlCommand(query, conexion); 
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.IdProducto);
                    cmd.CommandType = CommandType.Text; 

                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else {
                        Mensaje = "No se pudo actualizar imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado= false;
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", conexion); // Hacemos referencia al prcedimiento almacenado creado
                    cmd.Parameters.AddWithValue("IdProducto", id);
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
    }
}
