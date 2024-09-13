use DBCARRITO

SELECT * FROM USUARIO 
GO

create proc sp_RegistrarUsuario(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
     SET @Resultado = 0 
	 IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo)
	 begin 
	      INSERT INTO USUARIO(Nombres, Apellidos, Correo, Clave, Activo) 
		              values (@Nombres, @Apellidos, @Correo, @Clave, @Activo)

					  SET @Resultado = SCOPE_IDENTITY()
	end
	else
	  set @Mensaje = 'El correo del usuario ya existe'
end
go


create proc sp_EditarUsuarios(
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as 
begin 
    SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
	begin 
	    UPDATE TOP(1) USUARIO SET 
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		where IdUsuario = @IdUsuario

		SET @Resultado = 1
		end
		else
		  SET @Mensaje = 'El correo del usuario ya se encuentra registrado'
end

select * from USUARIO
delete from USUARIO where IdUsuario = 10


/*CATEGORIAS */
create proc sp_RegistrarCategoria(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)
   begin 
       INSERT INTO CATEGORIA(Descripcion, Activo) VALUES (@Descripcion, @Activo)
	   SET @Resultado = SCOPE_IDENTITY()
   end
   else
     SET @Mensaje = 'La categoria ya existe'
end

SELECT * FROM CATEGORIA
select IdCategoria, Descripcion, Activo from CATEGORIA

create proc sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion and IdCategoria != @IdCategoria)
   begin 
       UPDATE top (1) CATEGORIA SET 
	   Descripcion= @Descripcion,
	   Activo = @Activo
	   WHERE IdCategoria = @IdCategoria

	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'La categoria ya existe'
end


create proc sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM PRODUCTO p 
                 inner join CATEGORIA c on c.IdCategoria = p.IdCategoria
				 where p.IdCategoria = @IdCategoria)
   begin 
       DELETE TOP(1) FROM CATEGORIA WHERE IdCategoria = @IdCategoria
	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'La categoria se encuentra relacionada con un producto'
end


/*** MARCAS ***/
create proc sp_RegistrarMarca(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion = @Descripcion)
   begin 
       INSERT INTO MARCA(Descripcion, Activo) VALUES (@Descripcion, @Activo)
	   SET @Resultado = SCOPE_IDENTITY()
   end
   else
     SET @Mensaje = 'La Marca ya existe'
end

SELECT * FROM MARCA

create proc sp_EditarMarca(
@IdMarca int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM MARCA WHERE Descripcion = @Descripcion and IdMarca != @IdMarca)
   begin 
       UPDATE top (1) MARCA SET 
	   Descripcion= @Descripcion,
	   Activo = @Activo
	   WHERE IdMarca = @IdMarca

	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'La Marca ya existe'
end


create proc sp_EliminarMarca(
@IdMarca int,
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM PRODUCTO p 
                 inner join MARCA c on c.IdMarca = p.IdMarca
				 where p.IdCategoria = @IdMarca)
   begin 
       DELETE TOP(1) FROM MARCA WHERE IdMarca = @IdMarca
	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'La marca no se puede eliminar se encuentra relacionada con un producto'
end


/*** PRODUCTOS ***/
SELECT * FROM PRODUCTO

select IdProducto, Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo from PRODUCTO
SELECT p.IdProducto, p.Nombre, p.Descripcion, 
m.IdMarca, m.Descripcion[DesMarca], 
c.IdCategoria, c.Descripcion[DesCategoria],
p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo 
FROM PRODUCTO p
inner join MARCA m on m.IdMarca = p.IdMarca
inner join CATEGORIA c on c.IdCategoria = p.IdCategoria 


create proc sp_RegistrarProducto(
 @Nombre varchar(500),
 @Descripcion varchar(500),
 @IdMarca varchar(100),
 @IdCategoria varchar(100) ,
 @Precio decimal(10,2),
 @Stock int,
 @Activo bit,
 @Mensaje varchar(500) output,
 @Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre)
   begin 
       INSERT INTO Producto(Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo) 
	          VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @Activo)
	   SET @Resultado = SCOPE_IDENTITY()
   end
   else
     SET @Mensaje = 'El producto ya existe'
end


create proc sp_EditarProducto(
 @IdProducto int,
 @Nombre varchar(500),
 @Descripcion varchar(500),
 @IdMarca varchar(100),
 @IdCategoria varchar(100) ,
 @Precio decimal(10,2),
 @Stock int,
 @Activo bit,
 @Mensaje varchar(500) output,
 @Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM PRODUCTO WHERE Nombre = @Nombre and IdProducto != @IdProducto)
   begin 
       UPDATE top (1) PRODUCTO SET
	   Nombre = @Nombre,
	   Descripcion= @Descripcion,
	   IdMarca = @IdMarca,
	   IdCategoria = @IdCategoria,
	   Precio = @Precio,
	   Stock = @Stock,
	   Activo = @Activo
	   WHERE IdProducto = @IdProducto

	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'El producto ya existe'
end


create proc sp_EliminarProducto(
 @IdProducto int,
 @Mensaje varchar(500) output,
 @Resultado int output
)
as
begin 
   SET @Resultado = 0 
   IF NOT EXISTS (SELECT * FROM DETALLE_VENTA dv 
                 inner join PRODUCTO P on dv.IdProducto = p.IdProducto
				 where dv.IdProducto = @IdProducto)
   begin 
       DELETE TOP(1) FROM PRODUCTO WHERE IdProducto = @IdProducto
	   SET @Resultado = 1
   end
   else
     SET @Mensaje = 'El producto no se puede eliminar se encuentra relacionada a una venta'
end

SELECT * FROM PRODUCTO
DELETE FROM PRODUCTO
SELECT * FROM CATEGORIA
SELECT * FROM MARCA 

/* PROCEDIMIENTO PARA REPORTES DEL DASHBOARD*/
CREATE PROC sp_ReporteDashboard
as
begin 
  SELECT
    (SELECT COUNT(*) FROM CLIENTE) [TotalCliente],
    (SELECT ISNULL(SUM(CANTIDAD),0) FROM DETALLE_VENTA) [TotalVenta],
    (SELECT COUNT(*) FROM PRODUCTO) [TotalProducto]
END

EXEC sp_ReporteDashboard

/* CONSULTA PARA VER EL REPORTE DE VENTAS EN LA TABLA */
CREATE PROC sp_ReporteVentas(
 @fechainicio varchar(10),
 @fechafin varchar(10),
 @idtransaccion varchar(10)
 )
 AS 
 BEGIN
  SET DATEFORMAT dmy;

SELECT CONVERT(char(10),v.FechaVenta,103)[FechaVenta], CONCAT( c.Nombres, ' ', c.Apellidos)[Cliente],
p.Nombre[Producto], p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion  
FROM DETALLE_VENTA dv
INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
INNER JOIN VENTA v ON v.IdVenta = dv.IdVenta
INNER JOIN CLIENTE c ON c.IdCliente = v.IdCliente
WHERE CONVERT(date, v.FechaVenta) between @fechainicio and @fechafin
and v.IdTransaccion = iif(@idtransaccion = '', v.IdTransaccion, @idTransaccion)

end


/*** PROCEDIMIENTOS PARA CLIENTES ***/
select * FROM cliente
GO

CREATE PROC sp_RegistrarCliente(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
AS
 BEGIN 
   SET @Resultado = 0
   IF NOT EXISTS (SELECT * FROM CLIENTE WHERE Correo = @Correo)
   BEGIN
      INSERT INTO CLIENTE(Nombres, Apellidos, Correo, Clave, Reestablecer) 
		              values (@Nombres, @Apellidos, @Correo, @Clave,0)
	   
	   SET @Resultado = SCOPE_IDENTITY()
	END
	ELSE
	 SET @Mensaje = 'El correo del cliente ya existe'
END

SELECT * FROM MARCA

declare @idcategoria int = 0
select distinct m.IdMarca, m.Descripcion from PRODUCTO p
inner join CATEGORIA c on c.IdCategoria = p.IdCategoria
inner join MARCA m on m.IdMarca = p.IdMarca and m.Activo = 1
where c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)


/*** PROCEDIMIENTOS ALMACENADOS PARA CARRITO ***/
create proc sp_ExisteCarrito(
 @IdCliente int,
 @IdProducto int,
 @Resultado bit output
)
 as
  begin
    if exists (select * from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto)
	  set @Resultado = 1
     else
	   set @Resultado = 0
  end


/* Si sumar aplica toma un valor de 1, caso contrario va a tomar 0*/
create proc sp_OperacionCarrito(
 @IdCliente int,
 @IdProducto int,
 @Sumar bit,
 @Mensaje varchar(500) output,
 @Resultado bit output
)
 as
   begin 
     set @Resultado = 1
	 set @Mensaje = ''

	 declare @existecarrito bit = iif(exists(select * from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto),1,0)
	 declare @stockproducto int = (select stock from PRODUCTO where IdProducto = @IdProducto)

	 BEGIN TRY

	   BEGIN TRANSACTION OPERACION

		 if(@Sumar = 1)
		  begin
		    
			if(@stockproducto > 0)
			 begin 

			  if(@existecarrito = 1)
			     update CARRITO SET Cantidad = Cantidad + 1 where IdCliente = @IdCliente and IdProducto = @IdProducto
			  else
			     insert into CARRITO(IdCliente, IdProducto, Cantidad) values(@IdCliente, @IdProducto, 1)

			     update PRODUCTO SET Stock = Stock - 1 where IdProducto = @IdProducto
		      end
		      else
		        begin
		          set @Resultado = 0
			      set @Mensaje = 'El producto no cuenta con stock disponible'
	           end

		     end
		       else
			     begin
			      update CARRITO set Cantidad = Cantidad - 1 where IdCliente = @IdCliente and IdProducto = @IdProducto
				  update PRODUCTO set Stock = Stock + 1 where IdProducto = @IdProducto
			end

			COMMIT TRANSACTION OPERACION

		 END TRY
         BEGIN CATCH 
		    set @Resultado = 0
			set @Mensaje = ERROR_MESSAGE()
			ROLLBACK TRANSACTION OPERACION
		END CATCH
 end	
 


 /*Se crea una función de tipo tabla para obtener los datos del carrito de un determindo cliente*/
 CREATE FUNCTION fn_obtenerCarritoCliente(
  @idcliente int
 )
 returns table
  as 
    return (
	  select p.IdProducto, m.Descripcion[DesMarca], p.Nombre, p.Precio, c.Cantidad, p.RutaImagen, p.NombreImagen
      from CARRITO c 
      inner join PRODUCTO p on p.IdProducto = c.IdProducto
      inner join MARCA m on m.IdMarca = p.IdMarca 
      where c.IdCliente = @idcliente
)

select * from fn_obtenerCarritoCliente(3)


/*PROCEDIMIENTO ALMACENADO PARA ELIMINAR PRODUCTOS DEL CARRITO*/
CREATE PROC sp_EliminarCarrito(
 @IdCliente int,
 @IdProducto int, 
 @Resultado bit output
)
as
begin 
   set @Resultado = 1
   declare @cantidadproducto int = (select Cantidad from CARRITO where IdCliente = @IdCliente and IdProducto = @IdProducto)

   BEGIN TRY 
     BEGIN TRANSACTION OPERACION
	   UPDATE PRODUCTO SET Stock = Stock + @cantidadproducto where IdProducto = @IdProducto
	   DELETE TOP (1) FROM CARRITO WHERE IdCliente = @IdCliente and @IdProducto = @IdProducto

	   COMMIT TRANSACTION OPERACION
   END TRY
   BEGIN CATCH 
     set @Resultado = 0
	 ROLLBACK TRANSACTION OPERACION
   END CATCH
END


CREATE TYPE [dbo].[EDetalle_Venta] as TABLE(
    [IdProducto] int NULL,
    [Cantidad] int NULL,
    [Total] decimal(18,2) NULL
)


/***** PROCEDIMIENTO ALMACENADO PARA ADMINISTRAR LA VENTAS *****/
create proc sp_RegistrarVenta(
    @IdCliente int,
    @TotalProducto int,
    @MontoTotal decimal(18,2),
    @Contacto varchar(100),
    @IdDistrito varchar(6),
    @Telefono varchar(100),
    @Direccion varchar(100),
    @IdTransaccion varchar(50),
    @DetalleVenta [EDetalle_Venta] READONLY,
    @Resultado bit output,
    @Mensaje varchar(500) output
)
 as 
  begin 
    begin try
          declare @idventa int = 0
          set @Resultado = 1
          set @Mensaje = ''

          begin transaction registro 
          
             INSERT INTO VENTA(IdCliente, TotalProducto, MontoTotal, Contacto, IdDistrito, Telefono, Direccion, IdTransaccion)
                    VALUES(@IdCliente, @TotalProducto, @MontoTotal, @Contacto, @IdDistrito, @Telefono, @Direccion, @IdTransaccion)

             SET @idventa = SCOPE_IDENTITY()

             INSERT INTO DETALLE_VENTA(IdVenta, IdProducto, Cantidad, Total)
              SELECT @idventa, IdProducto, Cantidad, Total from @DetalleVenta

              DELETE FROM CARRITO WHERE IdCliente = @IdCliente
          
            commit transaction registro
      end try
      begin catch
        set @Resultado = 0
        set @Mensaje = ERROR_MESSAGE()
        rollback transaction registro
      end catch
  end 


CREATE FUNCTION fn_ListarCompra(
    @idcliente int
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT P.RutaImagen, p.NombreImagen, p.Nombre, p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion  FROM DETALLE_VENTA dv
    INNER JOIN PRODUCTO p ON p.IdProducto = dv.IdProducto
    INNER JOIN VENTA v ON v.IdVenta = dv.IdVenta 
    WHERE v.IdCliente = @idcliente
)

SELECT * FROM USUARIO
 SELECT * FROM CARRITO
 SELECT * FROM MARCA
 SELECT * FROM CATEGORIA
 SELECT * FROM CLIENTE
 SELECT * FROM PROVINCIA
 SELECT * FROM CANTON
 SELECT * FROM DISTRITO
 SELECT * FROM PRODUCTO
 SELECT * FROM VENTA
 SELECT * FROM DETALLE_VENTA


 select * from CANTON where IdProvincia = 01
 select * from DISTRITO where IdProvincia = '01' and IdCanton = '0101'


 select count(*) from CARRITO WHERE IdCliente = 1
