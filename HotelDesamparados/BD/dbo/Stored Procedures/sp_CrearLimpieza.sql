CREATE PROCEDURE sp_CrearLimpieza
    @TareasCompletadas NVARCHAR(500),
    @NombreConserje NVARCHAR(100),
    @Foto VARBINARY(MAX) = NULL
AS
BEGIN
    INSERT INTO LimpiezaHabitacion (TareasCompletadas, NombreConserje, Foto)
    VALUES (@TareasCompletadas, @NombreConserje, @Foto);
END;