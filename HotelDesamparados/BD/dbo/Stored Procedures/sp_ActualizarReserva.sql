CREATE PROCEDURE sp_ActualizarReserva
    @IdReserva INT,
    @FechaInicio DATETIME,
    @FechaFinal DATETIME,
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(100),
    @HabitacionId INT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reservas
    SET FechaInicio = @FechaInicio,
        FechaFinal = @FechaFinal,
        Nombre = @Nombre,
        Telefono = @Telefono,
        Correo = @Correo,
        HabitacionId = @HabitacionId,
        Estado = @Estado
    WHERE IdReserva = @IdReserva;
END