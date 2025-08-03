CREATE PROCEDURE sp_ObtenerHabitacionPorId
    @Id INT
AS
BEGIN
    SELECT Id, Capacidad, Precio, NumHabitacion, NumCamas, Extras, Comentarios, Estado
    FROM Habitacion
    WHERE Id = @Id;
END;