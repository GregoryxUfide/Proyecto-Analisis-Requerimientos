
CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT U.Id, U.Nombre, U.Apellidos, U.Gmail, U.Username, U.Estado, U.RolId, R.Nombre AS RolNombre
    FROM Usuarios U
    INNER JOIN Roles R ON U.RolId = R.Id;
END;