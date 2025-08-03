CREATE PROCEDURE sp_ListarPuntoVenta
AS
BEGIN
    SELECT 
        pv.Id,
        pv.DescripcionVenta,
        pv.Metodo_Pago,
        pv.Descuento,
        r.IdReserva AS IdReserva,
        r.Correo AS CorreoReserva,
        e.UsuarioId AS EmpleadoUsuarioId,
        u.Nombre AS NombreEmpleado
    FROM PuntoVenta pv
    INNER JOIN Reservas r ON pv.ReservaId = r.IdReserva
    INNER JOIN Empleado e ON pv.EmpleadoId = e.Id
    INNER JOIN Usuarios u ON e.UsuarioId = u.Id;
END