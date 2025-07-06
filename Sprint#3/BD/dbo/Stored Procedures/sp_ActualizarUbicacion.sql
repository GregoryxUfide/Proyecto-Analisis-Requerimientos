
-- EDITAR una ubicación existente
CREATE PROCEDURE sp_ActualizarUbicacion
@IdUbicacionProducto INT,
    @NombreUbicacionProducto VARCHAR(100),
    @DescripcionUbicacionProducto VARCHAR(100)
AS
BEGIN
    UPDATE UbicacionProducto
    SET NombreUbicacionProducto = @NombreUbicacionProducto,
        DescripcionUbicacionProducto = @DescripcionUbicacionProducto
    WHERE IdUbicacionProducto = @IdUbicacionProducto;
END;