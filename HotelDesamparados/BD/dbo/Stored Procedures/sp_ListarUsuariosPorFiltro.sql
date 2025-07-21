CREATE PROCEDURE sp_ListarUsuariosPorFiltro
    @RolId INT = NULL,
    @Estado BIT = NULL,
    @Busqueda NVARCHAR(100) = NULL
AS
BEGIN
    SELECT 
        u.Id, u.Nombre, u.Apellidos, u.Gmail, u.Username, u.Estado, u.RolId,
        r.Nombre AS RolNombre
    FROM Usuarios u
    INNER JOIN Roles r ON u.RolId = r.Id
    WHERE (@RolId IS NULL OR u.RolId = @RolId)
      AND (@Estado IS NULL OR u.Estado = @Estado)
      AND (@Busqueda IS NULL OR u.Gmail LIKE '%' + @Busqueda + '%' OR u.Nombre LIKE '%' + @Busqueda + '%')
END