CREATE PROCEDURE sp_ActualizarUsuario
    @Id INT,
    @Nombre NVARCHAR(100),
    @Apellidos NVARCHAR(100),
    @Gmail NVARCHAR(100),
    @Username NVARCHAR(50),
    @Estado BIT,
    @RolId INT
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Apellidos = @Apellidos,
        Gmail = @Gmail,
        Username = @Username,
        Estado = @Estado,
        RolId = @RolId
    WHERE Id = @Id;
END
