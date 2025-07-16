CREATE PROCEDURE sp_CrearReserva
    @IdReserva INT,
    @fechaInicio DATE,
    @fechaFinal DATE,
    @nombreReservante NVARCHAR(50),
    @telefono INT,
    @correo VARCHAR(50),
    @numHabitacion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Habitacion WHERE NumHabitacion = @numHabitacion)
    BEGIN
        INSERT INTO dbo.Reservas
        (
            IdReserva,
            fechaInicio,
            fechaFinal,
            nombreReservante,
            telefono,
            correo,
            numHabitacion
        )
        VALUES
        (
            @IdReserva,
            @fechaInicio,
            @fechaFinal,
            @nombreReservante,
            @telefono,
            @correo,
            @numHabitacion
        );

        SELECT 'Reserva creada correctamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'Error: La habitación especificada no existe.' AS Mensaje;
    END
END;