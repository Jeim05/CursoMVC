using CapaDatos;
using CapaDatos.Scripts;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marca
    {
        private CD_Marcas objCapaDatos = new CD_Marcas();

        public List<Marca> Listar()
        {
            return objCapaDatos.Listar();
        }

        public int Registrar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la marca no puede ser vacio";
            }

            // En esta validación indicamos que si mensaje sigue siendo vacio, significa que no hubo error
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Registrar(obj, out Mensaje);

            }
            else
            {
                return 0;
            }


        }

        public bool Editar(Marca obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la Marca no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDatos.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDatos.Eliminar(id, out Mensaje);
        }

        public List<Marca> ListarMarcaPorCategoria(int idcategoria)
        {
            return objCapaDatos.ListarMarcaPorCategoria(idcategoria);
        }
    }
}
