CREATE PROCEDURE sp_ActualizarContrasena
    @Id INT,
    @Contrasena NVARCHAR(255)
AS
BEGIN
    UPDATE Usuarios
    SET Contrasena = @Contrasena
    WHERE Id = @Id;
END