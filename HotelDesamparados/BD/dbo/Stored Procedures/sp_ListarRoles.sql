CREATE PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT Id, Nombre, Descripcion, Estado
    FROM Roles;
END;