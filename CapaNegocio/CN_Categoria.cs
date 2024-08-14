using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categorias objCapaDatos = new CD_Categorias();

        public List<Categoria> Listar()
        {
            return objCapaDatos.Listar(); 
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la categoria no puede ser vacio";
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

          public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripción de la categoria no puede ser vacio";
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
    }
}
