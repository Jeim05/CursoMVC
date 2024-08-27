using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDatos = new CD_Cliente();

        public int Registrar(Cliente obj, out string Mensaje)
        {
            // Antes de registrar un un Cliente aplicamos toda la capa de negocio que se debe hacer antes de registrar en la BD
            Mensaje = string.Empty;

            // Validamos que el nombre no sea vacio o que no contenga espacios vacios
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El nombre del Cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido del Cliente no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo del Cliente no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                obj.Clave = CN_Recursos.ConvertirSha256(obj.Clave); 
                return objCapaDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public List<Cliente> Listar()
        {
            return objCapaDatos.Listar(); 
        }

        public bool CambiarClave(int idCliente, string nuevaClave, out string Mensaje)
        {
            return objCapaDatos.CambiarClave(idCliente, nuevaClave, out Mensaje);
        }

        public bool ReestablecerClave(int idCliente, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaClave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDatos.ReestablecerClave(idCliente, CN_Recursos.ConvertirSha256(nuevaClave), out Mensaje);

            if (resultado)
            {
                string asunto = "Reestablecer contraseña";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);

                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }
        }


    }
}
