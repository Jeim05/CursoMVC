using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
//    USUARIO(
//IdUsuario int primary key identity,
//Nombres varchar(100),
//Apellidos varchar(100),
//Correo varchar(100),
//Clave varchar(150),
//Reestablecer bit default 0,
//Activo bit default 1,
//FechaRegistro datetime default getdate()

    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Reestablecer { get; set; }
        public bool Activo { get; set; }
    }
}
