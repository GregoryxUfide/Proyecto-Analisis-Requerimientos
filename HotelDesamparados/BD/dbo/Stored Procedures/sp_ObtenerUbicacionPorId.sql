
-- DETALLE de una ubicación específica por ID
CREATE PROCEDURE sp_ObtenerUbicacionPorId
    @IdUbicacionProducto INT
AS
BEGIN
    SELECT IdUbicacionProducto, NombreUbicacionProducto, DescripcionUbicacionProducto, Estado
    FROM UbicacionProducto
    WHERE IdUbicacionProducto = @IdUbicacionProducto;
END;