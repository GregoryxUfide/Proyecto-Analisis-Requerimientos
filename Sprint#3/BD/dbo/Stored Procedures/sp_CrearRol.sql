CREATE PROCEDURE sp_CrearRol
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(500),
    @Estado BIT
AS
BEGIN
    INSERT INTO Roles (Nombre, Descripcion, Estado)
    VALUES (@Nombre, @Descripcion, @Estado);
END;