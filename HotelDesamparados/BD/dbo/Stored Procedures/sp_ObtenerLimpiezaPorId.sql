CREATE PROCEDURE sp_ObtenerLimpiezaPorId
    @Id INT
AS
BEGIN
    SELECT 
        lh.Id, 
        lh.TareasCompletadas, 
        lh.NombreConserje, 
        lh.Foto, 
        lh.FechaHora,
        h.NumHabitacion
    FROM LimpiezaHabitacion lh
    INNER JOIN Habitacion h ON lh.HabitacionId = h.Id
    WHERE lh.Id = @Id;
END;