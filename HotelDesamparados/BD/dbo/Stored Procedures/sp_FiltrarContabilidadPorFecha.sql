
CREATE PROCEDURE sp_FiltrarContabilidadPorFecha
    @Mes INT = NULL,
    @Anio INT = NULL
AS
BEGIN
    SELECT IdContabilidad, Fecha, Monto, Detalle, Comentario
    FROM Contabilidad
    WHERE 
        (@Mes IS NULL OR MONTH(Fecha) = @Mes)
        AND (@Anio IS NULL OR YEAR(Fecha) = @Anio)
END