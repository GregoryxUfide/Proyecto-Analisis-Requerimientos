CREATE PROCEDURE sp_ObtenerEmpleadoPorId
    @Id INT
AS
BEGIN
    SELECT
        e.Id,
        e.NumeroEmpleado,
        e.SalarioEmpleado,
        e.UsuarioId,
        u.Username,
        e.RolId,
        r.Nombre AS RolNombre,
        e.Estado
    FROM Empleado e
    INNER JOIN Usuarios u ON e.UsuarioId = u.Id
    INNER JOIN Roles r ON e.RolId = r.Id
    WHERE e.Id = @Id
END