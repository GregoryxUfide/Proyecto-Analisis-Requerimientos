CREATE PROCEDURE sp_ObtenerLimpiezaPorId
    @Id INT
AS
BEGIN
    SELECT Id, TareasCompletadas, NombreConserje, Foto, FechaHora
    FROM LimpiezaHabitacion
    WHERE Id = @Id;
END;