CREATE PROCEDURE sp_ActualizarEmpleado
    @Id INT,
    @NumeroEmpleado VARCHAR(50),
    @SalarioEmpleado DECIMAL(18,2),    
    @RolId INT,
    @Estado BIT
AS
BEGIN
    UPDATE Empleado
    SET NumeroEmpleado = @NumeroEmpleado,
        SalarioEmpleado = @SalarioEmpleado,                
        Estado = @Estado,
        RolId = @RolId
    WHERE Id = @Id
END