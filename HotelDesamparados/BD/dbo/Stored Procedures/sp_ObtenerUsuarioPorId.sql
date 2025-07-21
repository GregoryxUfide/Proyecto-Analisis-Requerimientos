
CREATE PROCEDURE sp_ObtenerUsuarioPorId
    @Id INT
AS
BEGIN
    SELECT U.Id, U.Nombre, U.Apellidos, U.Gmail, U.Username, U.Estado, U.RolId, R.Nombre AS RolNombre
    FROM Usuarios U
    INNER JOIN Roles R ON U.RolId = R.Id
    WHERE U.Id = @Id;
END;