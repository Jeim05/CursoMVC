using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Ubicacion
    {
        //AGREGAR AL SCRIPT DE LA BD
        // select * from PROVINCIA
        // select * from CANTON where IdProvincia = 1
        // select * from DISTRITO where IdProvincia = @IdProvincia and IdCanton = @IdCanton

         public List<Provincia> ObtenerProvincia()
        {
            List < Provincia > lista = new List<Provincia>();

            try {

                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion)){
                    string query = "select * from PROVINCIA";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();


                    // SqlDataReader hace la lectura y da el resultado de la ejecucion de la consulta
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read())
                        {
                            lista.Add(
                                new Provincia()
                                {
                                    IdProvincia = reader["IdProvincia"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                        }
                    }
                }
            }
            catch { 
            lista = new List<Provincia>();
            }
            return lista;
        }

        
         public List<Canton> ObtenerCanton(string idprovincia)
        {
            List < Canton > lista = new List<Canton>();

            try {

                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion)){
                    string query = "select * from CANTON where IdProvincia = @idprovincia";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();
                    
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read())
                        {
                            lista.Add(
                                new Canton()
                                {
                                    IdCanton = reader["IdCanton"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                        }
                    }
                }
            }
            catch { 
            lista = new List<Canton>();
            }
            return lista;
        }

        

        public List<Distrito> ObtenerDistrito(string idprovincia, string idcanton)
        {
            List < Distrito > lista = new List<Distrito>();

            try {

                using (SqlConnection oconexion = new SqlConnection(Conexion.conexion)){
                    string query = "select * from DISTRITO where IdProvincia = @IdProvincia and IdCanton = @IdCanton";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdProvincia", idprovincia);
                    cmd.Parameters.AddWithValue("@IdCanton", idcanton);
                    cmd.CommandType = CommandType.Text; // Se da este instruccion ya que el query es un texto simple
                    oconexion.Open();
                    
                    using(SqlDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read())
                        {
                            lista.Add(
                                new Distrito()
                                {
                                    IdDistrito = reader["IdDistrito"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                        }
                    }
                }
            }
            catch { 
            lista = new List<Distrito>();
            }
            return lista;
        }
    }
}
