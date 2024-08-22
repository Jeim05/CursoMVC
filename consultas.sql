SELECT CONVERT(char(10),v.FechaVenta,103)[FechaVenta], c.Nombres, c.Apellidos,)
p.Nombre, p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion, 
FROM DETALLE_VENTA dv
INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
INNER JOIN VENTA v ON v.IdVenta = dv.IdVenta
INNER JOIN CLIENTE c ON c.IdCliente = v.IdCliente
