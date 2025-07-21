-- Obtener usuario por correo con rol
CREATE PROCEDURE sp_ObtenerUsuarioPorCorreo
    @Gmail NVARCHAR(100)
AS
BEGIN
    SELECT U.Id, U.Nombre, U.Apellidos, U.Gmail, U.Username, U.Contrasena, U.Estado, U.RolId, R.Nombre AS RolNombre
    FROM Usuarios U
    INNER JOIN Roles R ON U.RolId = R.Id
    WHERE U.Gmail = @Gmail;
END;