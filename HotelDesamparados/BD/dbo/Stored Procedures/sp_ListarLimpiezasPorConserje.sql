CREATE PROCEDURE sp_ListarLimpiezasPorConserje
    @NombreConserje NVARCHAR(100)
AS
BEGIN
    SELECT Id, TareasCompletadas, FechaHora
    FROM LimpiezaHabitacion
    WHERE NombreConserje = @NombreConserje
    ORDER BY FechaHora DESC;
END;