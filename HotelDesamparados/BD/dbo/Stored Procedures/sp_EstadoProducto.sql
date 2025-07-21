CREATE PROCEDURE sp_EstadoProducto
    @IdProducto INT,
    @EstadoProducto BIT
AS
BEGIN
    UPDATE Producto
    SET EstadoProducto = @EstadoProducto
    WHERE IdProducto = @IdProducto;
END;