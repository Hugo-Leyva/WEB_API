
CREATE DATABASE DB_Productos;

USE DB_Productos;


--TABLAS

CREATE TABLE Usuario(
IdUsuario INT IDENTITY(1,1) NOT NULL,
Nombre VARCHAR(30),
Correo VARCHAR(40),
Contrasena VARCHAR(30)
);




CREATE TABLE Productos(
IdProducto INT IDENTITY(1,1) NOT NULL,
Nombre VARCHAR(30),
Marca VARCHAR(40),
CostoUnitario INT,
Cantidad INT,
PrecioVenta INT,
FechaCompra DATETIME
);


SELECT * FROM Usuario;



--CONSULTAR PRODUCTOS

GO
CREATE PROCEDURE spConsultarProducto
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Productos;
END
GO

EXEC spConsultarProducto



--Registrar Productos


go
CREATE PROCEDURE spRegistrarProducto
    @Nombre VARCHAR(30),
    @Marca VARCHAR(40),
    @CostoUnitario INT,
    @Cantidad VARCHAR(30),
    @PrecioVenta INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Productos(Nombre, Marca, CostoUnitario, Cantidad, PrecioVenta, FechaCompra)
    VALUES (@Nombre, @Marca, @CostoUnitario, @Cantidad, @PrecioVenta, GETDATE());
END
GO

EXEC spRegistrarProducto
    @Nombre = 'Pan',
    @Marca = 'Bimbo',
    @CostoUnitario = 30,
    @Cantidad = 20,
    @PrecioVenta = 50;

SELECT * FROM Productos;



--Modificar Productos
GO
CREATE PROCEDURE spEditarProducto
    @IdProducto INT,
    @Nombre VARCHAR(30) = NULL,
    @Marca VARCHAR(40) = NULL,
    @CostoUnitario INT = NULL,
    @Cantidad VARCHAR(30) = NULL,
    @PrecioVenta INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Productos
    SET
        Nombre = CASE WHEN @Nombre IS NOT NULL THEN @Nombre ELSE Nombre END,
        Marca = CASE WHEN @Marca IS NOT NULL THEN @Marca ELSE Marca END,
        CostoUnitario = CASE WHEN @CostoUnitario IS NOT NULL THEN @CostoUnitario ELSE CostoUnitario END,
        Cantidad = CASE WHEN @Cantidad IS NOT NULL THEN @Cantidad ELSE Cantidad END,
        PrecioVenta = CASE WHEN @PrecioVenta IS NOT NULL THEN @PrecioVenta ELSE PrecioVenta END
    WHERE IdProducto = @IdProducto;
END
GO


SELECT * FROM Productos;



--Eliminar Productos

GO
CREATE PROCEDURE spEliminarProducto
    @IdProducto INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Productos
    WHERE IdProducto = @IdProducto;
END
GO

SELECT * FROM Productos;

