CREATE PROCEDURE sp_ListarReservas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.IdReserva,
        r.FechaInicio,
        r.FechaFinal,
        r.Nombre,
        r.Telefono,
        r.Correo,
        r.HabitacionId,
        r.Estado,
        h.NumHabitacion
    FROM Reservas r
    INNER JOIN Habitacion h ON r.HabitacionId = h.Id;
END