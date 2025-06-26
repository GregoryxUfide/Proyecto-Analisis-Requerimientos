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
GO

-- Verificar si existe correo
CREATE PROCEDURE sp_ExisteCorreo
    @Gmail NVARCHAR(100)
AS
BEGIN
    SELECT COUNT(1)
    FROM Usuarios
    WHERE Gmail = @Gmail;
END;
GO

-- Obtener rol por nombre (retorna Id)
CREATE PROCEDURE sp_ObtenerRolPorNombre
    @Nombre NVARCHAR(50)
AS
BEGIN
    SELECT Id
    FROM Roles
    WHERE Nombre = @Nombre;
END;
GO
