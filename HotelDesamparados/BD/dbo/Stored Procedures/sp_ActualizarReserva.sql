CREATE PROCEDURE sp_ActualizarReserva
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

    -- Validar que la habitación exista
    IF NOT EXISTS (SELECT 1 FROM dbo.Habitacion WHERE NumHabitacion = @numHabitacion)
    BEGIN
        SELECT 'Error: La habitación especificada no existe.' AS Mensaje;
        RETURN;
    END

    -- Actualizar datos sin validar la reserva
    UPDATE dbo.Reservas
    SET
        fechaInicio = @fechaInicio,
        fechaFinal = @fechaFinal,
        nombreReservante = @nombreReservante,
        telefono = @telefono,
        correo = @correo,
        numHabitacion = @numHabitacion
    WHERE
        IdReserva = @IdReserva;

    SELECT 'Reserva actualizada correctamente.' AS Mensaje;
END;
