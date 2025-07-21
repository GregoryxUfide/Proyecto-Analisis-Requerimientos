CREATE PROCEDURE sp_ListarLimpiezas
AS
BEGIN
    SELECT Id, TareasCompletadas, NombreConserje, FechaHora
    FROM LimpiezaHabitacion
    ORDER BY FechaHora DESC;
END;