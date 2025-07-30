
CREATE PROCEDURE sp_ListarContabilidad 
AS
BEGIN
    SELECT 
        IdContabilidad,
        Fecha,
        Monto,
        Detalle,
        Comentario
    FROM Contabilidad;
END;