CREATE PROCEDURE sp_EliminarReserva
    @IdReserva INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Reservas WHERE IdReserva = @IdReserva;

END;
