CREATE PROCEDURE sp_CrearEmpleado
    @NumeroEmpleado VARCHAR(50),
    @SalarioEmpleado DECIMAL(18,2),
    @UsuarioId INT,
    @Estado BIT
AS
BEGIN
    INSERT INTO Empleado (NumeroEmpleado, SalarioEmpleado, UsuarioId, Estado)
    VALUES (@NumeroEmpleado, @SalarioEmpleado, @UsuarioId, @Estado)
END
