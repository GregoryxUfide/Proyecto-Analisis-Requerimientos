CREATE PROCEDURE sp_CambiarEstadoReserva
    @Id INT,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reservas
    SET Estado = @Estado
    WHERE IdReserva = @Id;
END