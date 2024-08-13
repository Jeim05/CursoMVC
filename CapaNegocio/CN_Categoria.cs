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
            return objCapaDatos.Listar(); // Metodo que devuelve la lista con los usuarios registrados
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            // Antes de registrar un un usuario aplicamos toda la capa de negocio que se debe hacer antes de registrar en la BD
            Mensaje = string.Empty;


            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "El nombre del usario no puede ser vacio";
            }

            // En esta validación indicamos que si mensaje sigue siendo vacio, significa que no hubo error
            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();
                string asunto = "Credenciales creación de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    obj.Clave = CN_Recursos.ConvertirSha256(clave); // Encriptamos la clave
                    return objCapaDatos.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }

            }
            else
            {
                return 0;
            }


        }
    }
}
