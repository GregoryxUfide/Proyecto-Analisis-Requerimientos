CREATE PROCEDURE sp_ListarLimpiezas
AS
BEGIN
    SELECT 
        lh.Id, 
        lh.TareasCompletadas, 
        lh.NombreConserje, 
        lh.FechaHora,
        h.NumHabitacion
    FROM LimpiezaHabitacion lh
    INNER JOIN Habitacion h ON lh.HabitacionId = h.Id
    ORDER BY lh.FechaHora DESC;
END;