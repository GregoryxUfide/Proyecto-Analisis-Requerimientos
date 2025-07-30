
CREATE PROCEDURE sp_ObtenerContabilidadPorId
    @IdContabilidad INT
AS
BEGIN
    SELECT 
        IdContabilidad,
        Fecha,
        Monto,
        Detalle,
        Comentario
    FROM Contabilidad
    WHERE IdContabilidad = @IdContabilidad;
END;