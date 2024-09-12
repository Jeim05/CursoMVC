using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Globalization;

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
                    cmd.Parameters.AddWithValue("TotalProducto", obj.TotlProducto);
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

        public List<DetalleVenta> ListarCompras(int idcliente)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion))
                {
                    string query = "select * from fn_ListarCompra(@idcliente)";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idcliente", idcliente);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(
                                new DetalleVenta()
                                {
                                    oProducto = new Producto()
                                    {
                                        Nombre = reader["Nombre"].ToString(),
                                        Precio = Convert.ToDecimal(reader["Precio"], new CultureInfo("es-CR")),
                                        RutaImagen = reader["RutaImagen"].ToString(),
                                        NombreImagen = reader["NombreImagen"].ToString()
                                    },
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Total = Convert.ToDecimal(reader["Total"], new CultureInfo("es-CR")),
                                    IdTransaccion = reader["IdTransaccion"].ToString()
                                });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<DetalleVenta>();
            }

            return lista;
        }



    }
}
