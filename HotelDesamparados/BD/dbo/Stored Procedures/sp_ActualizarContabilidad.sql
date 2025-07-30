CREATE PROCEDURE sp_ActualizarContabilidad
    @IdContabilidad INT,
    @Fecha DATE,
    @Monto DECIMAL(18,2),
    @Detalle VARCHAR(MAX),
    @Comentario VARCHAR(MAX)
AS
BEGIN
    UPDATE Contabilidad
    SET 
        Fecha = @Fecha,
        Monto = @Monto,
        Detalle = @Detalle,
        Comentario = @Comentario
    WHERE IdContabilidad = @IdContabilidad;
END;