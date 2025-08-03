CREATE PROCEDURE sp_ListarHabitaciones
AS
BEGIN
    SELECT Id, Capacidad, Precio, NumHabitacion, NumCamas, Extras, Comentarios, Estado
    FROM Habitacion
    ORDER BY NumHabitacion ASC;
END;