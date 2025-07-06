
-- CREAR una nueva ubicación
CREATE PROCEDURE sp_CrearUbicacion
    @NombreUbicacionProducto VARCHAR(100),
    @DescripcionUbicacionProducto VARCHAR(100),
    @Estado BIT
AS
BEGIN
    INSERT INTO UbicacionProducto (NombreUbicacionProducto, DescripcionUbicacionProducto, Estado)
    VALUES (@NombreUbicacionProducto, @DescripcionUbicacionProducto, @Estado);
END;