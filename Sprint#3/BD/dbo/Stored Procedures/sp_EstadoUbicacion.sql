CREATE PROCEDURE sp_EstadoUbicacion
    @IdUbicacionProducto INT,
    @Estado BIT
AS
BEGIN
    UPDATE UbicacionProducto
    SET Estado = @Estado
    WHERE IdUbicacionProducto = @IdUbicacionProducto;
END;
