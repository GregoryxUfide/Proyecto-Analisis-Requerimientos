CREATE PROCEDURE sp_ListarEmpleados
AS
BEGIN
    SELECT
        e.Id,
        e.NumeroEmpleado,
        e.SalarioEmpleado,
        e.UsuarioId,
        u.Username,
        u.RolId,
        r.Nombre AS RolNombre,
        e.Estado
    FROM Empleado e
    INNER JOIN Usuarios u ON e.UsuarioId = u.Id
    INNER JOIN Roles r ON u.RolId = r.Id
END
