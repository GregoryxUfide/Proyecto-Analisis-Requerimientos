CREATE PROCEDURE sp_EstadoReserva
    @IdReserva INT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reservas
    SET Estado = @Estado
    WHERE IdReserva = @IdReserva;
END