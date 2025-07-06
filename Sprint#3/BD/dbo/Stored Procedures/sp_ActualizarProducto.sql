
-- Editar Producto
CREATE PROCEDURE sp_ActualizarProducto
    @IdProducto INT,
    @NombreProducto VARCHAR(100),
    @DescripcionProducto VARCHAR(100),
    @IdUbicacionProducto INT,
    @CantidadProducto INT,
    @CaducidadProducto DATE,
    @MarcaProducto VARCHAR(100),
    @EstadoProducto BIT
AS
BEGIN
    UPDATE Producto
    SET NombreProducto = @NombreProducto,
        DescripcionProducto = @DescripcionProducto,
        IdUbicacionProducto = @IdUbicacionProducto,
        CantidadProducto = @CantidadProducto,
        CaducidadProducto = @CaducidadProducto,
        MarcaProducto = @MarcaProducto,
        EstadoProducto = @EstadoProducto
    WHERE IdProducto = @IdProducto;
END;