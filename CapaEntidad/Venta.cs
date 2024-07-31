
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
//    VENTA(
//IdVenta int primary key identity,
//IdCliente int references CLIENTE(IdCliente),
//TotalProducto int,
//MontoTotal decimal(10,2),
//Contacto varchar(50),
//IdDistrito varchar(10),
//Telefono varchar(50),
//Direccion varchar(500),
//IdTransaccion varchar(50),
//FechaVenta datetime default getdate()

    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int TotlProducto { get; set; }
        public decimal MontoTotal { get; set; }
        public string Contacto { get; set; }
        public string IdDistrito { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string FechaTexto { get; set; }
        public string IdTransaccion { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; }
    }
}
