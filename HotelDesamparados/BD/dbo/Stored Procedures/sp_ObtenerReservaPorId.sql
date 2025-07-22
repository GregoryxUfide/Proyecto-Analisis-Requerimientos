CREATE PROCEDURE sp_ObtenerReservaPorId
    @IdReserva INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdReserva,
        fechaInicio,
        fechaFinal,
        nombreReservante,
        telefono,
        correo,
        numHabitacion
    FROM dbo.Reservas
    WHERE IdReserva = @IdReserva;
END;

