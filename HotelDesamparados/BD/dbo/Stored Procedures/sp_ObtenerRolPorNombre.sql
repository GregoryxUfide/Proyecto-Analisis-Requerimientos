
-- Obtener rol por nombre (retorna Id)
CREATE PROCEDURE sp_ObtenerRolPorNombre
    @Nombre NVARCHAR(50)
AS
BEGIN
    SELECT Id
    FROM Roles
    WHERE Nombre = @Nombre;
END;