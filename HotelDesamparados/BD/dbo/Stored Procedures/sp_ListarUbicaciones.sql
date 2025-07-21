CREATE PROCEDURE sp_ListarUbicaciones
AS
BEGIN
    SELECT IdUbicacionProducto, NombreUbicacionProducto, DescripcionUbicacionProducto, Estado
    FROM UbicacionProducto;
END;