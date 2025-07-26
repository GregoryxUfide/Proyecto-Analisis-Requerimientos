CREATE PROCEDURE sp_ActualizarEmpleado
    @Id INT,
    @NumeroEmpleado VARCHAR(50),
    @SalarioEmpleado DECIMAL(18,2),    
    @UsuarioId INT,
    @Estado BIT
AS
BEGIN
    UPDATE Empleado
    SET NumeroEmpleado = @NumeroEmpleado,
        SalarioEmpleado = @SalarioEmpleado,                
        UsuarioId = @UsuarioId,
        Estado = @Estado
    WHERE Id = @Id
END
