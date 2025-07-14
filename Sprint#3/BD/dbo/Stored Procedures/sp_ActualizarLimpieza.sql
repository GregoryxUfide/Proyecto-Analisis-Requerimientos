 CREATE PROCEDURE sp_ActualizarLimpieza
    @Id INT,
    @TareasCompletadas NVARCHAR(500),
    @NombreConserje NVARCHAR(100),
    @Foto VARBINARY(MAX) = NULL
AS
BEGIN
    UPDATE LimpiezaHabitacion
    SET TareasCompletadas = @TareasCompletadas,
        NombreConserje = @NombreConserje,
        Foto = @Foto
    WHERE Id = @Id;
END;