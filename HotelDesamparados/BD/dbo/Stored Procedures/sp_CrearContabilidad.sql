
CREATE PROCEDURE sp_CrearContabilidad
    @Fecha DATE,
    @Monto DECIMAL(18,2),
    @Detalle VARCHAR(MAX),
    @Comentario VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Contabilidad (Fecha, Monto, Detalle, Comentario)
    VALUES (@Fecha, @Monto, @Detalle, @Comentario);
END;