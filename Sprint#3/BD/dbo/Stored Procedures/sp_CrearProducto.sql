
-- Crear Producto
CREATE PROCEDURE sp_CrearProducto
    @NombreProducto VARCHAR(100),
    @DescripcionProducto VARCHAR(100),
    @IdUbicacionProducto INT,
    @CantidadProducto INT,
    @CaducidadProducto DATE,
    @MarcaProducto VARCHAR(100),
    @EstadoProducto BIT
AS
BEGIN
    INSERT INTO Producto (NombreProducto, DescripcionProducto, IdUbicacionProducto, CantidadProducto, CaducidadProducto, MarcaProducto, EstadoProducto)
    VALUES (@NombreProducto, @DescripcionProducto, @IdUbicacionProducto, @CantidadProducto, @CaducidadProducto, @MarcaProducto, @EstadoProducto);
END;