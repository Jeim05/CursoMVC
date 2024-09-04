using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Carrito
    {
        private CD_Carrito objCapaDato = new CD_Carrito();

        public bool ExisteCarrito(int idcliente, int idproducto){
            return objCapaDato.ExisteCarrito(idcliente, idproducto);
        }

         public bool OperacionCarrito(int idcliente, int idproducto, bool suma, out string Mensaje){
             return objCapaDato.OperacionCarrito(idcliente, idproducto, suma, out Mensaje);
         }

         public int CantidadEnCarrito(int idcliente){
              return objCapaDato.CantidadEnCarrito(idcliente);
         }

    }
}
