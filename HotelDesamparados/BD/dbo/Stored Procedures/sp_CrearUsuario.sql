CREATE PROCEDURE sp_CrearUsuario
    @Nombre NVARCHAR(50),
    @Apellidos NVARCHAR(100),
    @Gmail NVARCHAR(100),
    @Username NVARCHAR(50),
    @Contrasena NVARCHAR(200),
    @Estado BIT,
    @RolId INT
AS
BEGIN
    INSERT INTO Usuarios (Nombre, Apellidos, Gmail, Username, Contrasena, Estado, RolId)
    VALUES (@Nombre, @Apellidos, @Gmail, @Username, @Contrasena, @Estado, @RolId);
END;