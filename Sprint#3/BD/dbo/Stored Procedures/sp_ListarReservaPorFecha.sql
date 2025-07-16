CREATE PROCEDURE sp_ListarReservaPorFecha
    @fechaDesde DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        R.IdReserva,
        R.fechaInicio,
        R.fechaFinal,
        R.nombreReservante,
        R.telefono,
        R.correo,
        H.NumHabitacion
    FROM dbo.Reservas R
    INNER JOIN dbo.Habitacion H ON R.numHabitacion = H.NumHabitacion
    WHERE R.fechaInicio >= @fechaDesde;
END;
