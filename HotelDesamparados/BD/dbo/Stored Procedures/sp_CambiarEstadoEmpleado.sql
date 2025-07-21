CREATE PROCEDURE sp_CambiarEstadoEmpleado
    @Id INT,
    @Estado BIT
AS
BEGIN
    UPDATE Empleado
    SET Estado = @Estado
    WHERE Id = @Id
END