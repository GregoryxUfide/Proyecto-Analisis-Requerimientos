CREATE PROCEDURE sp_EstadoRol
@Id INT,
    @Estado BIT
AS
BEGIN
    UPDATE Roles
    SET Estado = @Estado
    WHERE Id = @Id;
END;
