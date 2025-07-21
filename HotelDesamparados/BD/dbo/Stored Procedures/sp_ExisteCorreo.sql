
-- Verificar si existe correo
CREATE PROCEDURE sp_ExisteCorreo
    @Gmail NVARCHAR(100)
AS
BEGIN
    SELECT COUNT(1)
    FROM Usuarios
    WHERE Gmail = @Gmail;
END;