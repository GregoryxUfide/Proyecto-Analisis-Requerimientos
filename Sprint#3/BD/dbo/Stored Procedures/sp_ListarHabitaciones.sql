CREATE PROCEDURE sp_ListarHabitaciones
AS
BEGIN
    SELECT Id, Capacidad, Precio, NumHabitacion, NumCamas, Extras, Comentarios
    FROM Habitacion
	ORDER BY 
	[NumHabitacion] ASC;-- Ordenar por número de habitación
END;