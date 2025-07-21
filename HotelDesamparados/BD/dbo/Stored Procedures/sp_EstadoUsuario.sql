CREATE PROCEDURE sp_EstadoUsuario
    @Id INT,
    @Estado BIT
AS
BEGIN
    UPDATE Usuarios
    SET Estado = @Estado
    WHERE Id = @Id;
END;