CREATE PROCEDURE sp_CrearEmpleado
    @NumeroEmpleado VARCHAR(50),
    @SalarioEmpleado DECIMAL(18,2),
    @UsuarioId INT,
    @RolId INT,
    @Estado BIT
AS
BEGIN
    INSERT INTO Empleado (NumeroEmpleado, SalarioEmpleado, UsuarioId, RolId, Estado)
    VALUES (@NumeroEmpleado, @SalarioEmpleado, @UsuarioId, @RolId, @Estado)
END