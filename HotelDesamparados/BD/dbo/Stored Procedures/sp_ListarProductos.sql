CREATE PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT 
        P.IdProducto,
        P.NombreProducto,
        P.DescripcionProducto,
        P.IdUbicacionProducto,
        U.NombreUbicacionProducto,
        P.CantidadProducto,
        P.CaducidadProducto,
        P.MarcaProducto,
        P.EstadoProducto
    FROM Producto P
    INNER JOIN UbicacionProducto U ON P.IdUbicacionProducto = U.IdUbicacionProducto;
END;