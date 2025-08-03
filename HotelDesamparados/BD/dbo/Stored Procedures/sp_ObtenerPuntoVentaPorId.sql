CREATE PROCEDURE sp_ObtenerPuntoVentaPorId
    @Id INT
AS
BEGIN
    SELECT 
        pv.Id,
        pv.DescripcionVenta,
        pv.Metodo_Pago,
        pv.Descuento,
        r.IdReserva AS IdReserva,
        r.Correo AS CorreoReserva,
        e.UsuarioId AS EmpleadoUsuarioId
    FROM PuntoVenta pv
    INNER JOIN Reservas r ON pv.ReservaId = r.IdReserva
    INNER JOIN Empleado e ON pv.EmpleadoId = e.Id
    WHERE pv.Id = @Id;
END