CREATE PROCEDURE sp_CrearLimpieza
    @TareasCompletadas NVARCHAR(500),
    @NombreConserje NVARCHAR(100),
    @Foto VARBINARY(MAX) = NULL,
    @HabitacionId INT
AS
BEGIN
    INSERT INTO LimpiezaHabitacion (TareasCompletadas, NombreConserje, Foto, HabitacionId)
    VALUES (@TareasCompletadas, @NombreConserje, @Foto, @HabitacionId);
END;