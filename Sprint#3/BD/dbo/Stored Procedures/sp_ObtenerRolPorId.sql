
CREATE PROCEDURE sp_ObtenerRolPorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre, Descripcion, Estado
    FROM Roles
    WHERE Id = @Id;
END;
