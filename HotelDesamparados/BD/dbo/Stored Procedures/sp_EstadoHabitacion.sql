CREATE PROCEDURE sp_EstadoHabitacion
    @Id INT,
    @Estado BIT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Habitacion WHERE Id = @Id)
    BEGIN
        UPDATE Habitacion
        SET Estado = @Estado
        WHERE Id = @Id;
    END
    ELSE
    BEGIN
        RAISERROR('Habitación no encontrada.', 16, 1);
    END
END;