-- =========================================
-- CRUD ROLES
-- =========================================

CREATE PROCEDURE sp_CrearRol
    @Nombre NVARCHAR(50)
AS
BEGIN
    INSERT INTO Roles (Nombre)
    VALUES (@Nombre);
END;
GO

CREATE PROCEDURE sp_ObtenerRolPorId
    @Id INT
AS
BEGIN
    SELECT Id, Nombre
    FROM Roles
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_ListarRoles
AS
BEGIN
    SELECT Id, Nombre
    FROM Roles;
END;
GO

CREATE PROCEDURE sp_ActualizarRol
    @Id INT,
    @Nombre NVARCHAR(50)
AS
BEGIN
    UPDATE Roles
    SET Nombre = @Nombre
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_EliminarRol
    @Id INT
AS
BEGIN
    DELETE FROM Roles
    WHERE Id = @Id;
END;
GO


-- =========================================
-- CRUD USUARIOS
-- =========================================

CREATE PROCEDURE sp_CrearUsuario
    @Nombre NVARCHAR(50),
    @Apellidos NVARCHAR(100),
    @Gmail NVARCHAR(100),
    @Username NVARCHAR(50),
    @Contrasena NVARCHAR(200),
    @Estado BIT,
    @RolId INT
AS
BEGIN
    INSERT INTO Usuarios (Nombre, Apellidos, Gmail, Username, Contrasena, Estado, RolId)
    VALUES (@Nombre, @Apellidos, @Gmail, @Username, @Contrasena, @Estado, @RolId);
END;
GO

CREATE PROCEDURE sp_ObtenerUsuarioPorId
    @Id INT
AS
BEGIN
    SELECT U.Id, U.Nombre, U.Apellidos, U.Gmail, U.Username, U.Estado, U.RolId, R.Nombre AS RolNombre
    FROM Usuarios U
    INNER JOIN Roles R ON U.RolId = R.Id
    WHERE U.Id = @Id;
END;
GO

CREATE PROCEDURE sp_ListarUsuarios
AS
BEGIN
    SELECT U.Id, U.Nombre, U.Apellidos, U.Gmail, U.Username, U.Estado, U.RolId, R.Nombre AS RolNombre
    FROM Usuarios U
    INNER JOIN Roles R ON U.RolId = R.Id;
END;
GO

CREATE PROCEDURE sp_ActualizarUsuario
    @Id INT,
    @Nombre NVARCHAR(50),
    @Apellidos NVARCHAR(100),
    @Gmail NVARCHAR(100),
    @Username NVARCHAR(50),
    @Contrasena NVARCHAR(200),
    @Estado BIT,
    @RolId INT
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre,
        Apellidos = @Apellidos,
        Gmail = @Gmail,
        Username = @Username,
        Contrasena = @Contrasena,
        Estado = @Estado,
        RolId = @RolId
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE sp_EliminarUsuario
    @Id INT
AS
BEGIN
    DELETE FROM Usuarios
    WHERE Id = @Id;
END;
GO


-- =========================================
-- CRUD UbicacionProducto
-- =========================================
-- LISTAR todas las ubicaciones
CREATE PROCEDURE sp_ListarUbicaciones
AS
BEGIN
    SELECT IdUbicacionProducto, NombreUbicacionProducto, DescripcionUbicacionProducto
    FROM UbicacionProducto;
END
GO

-- CREAR una nueva ubicación
CREATE PROCEDURE sp_CrearUbicacion
    @NombreUbicacionProducto VARCHAR(100),
    @DescripcionUbicacionProducto VARCHAR(100)
AS
BEGIN
    INSERT INTO UbicacionProducto (NombreUbicacionProducto, DescripcionUbicacionProducto)
    VALUES (@NombreUbicacionProducto, @DescripcionUbicacionProducto);
END
GO

-- EDITAR una ubicación existente
CREATE PROCEDURE sp_ActualizarUbicacion
    @IdUbicacionProducto INT,
    @NombreUbicacionProducto VARCHAR(100),
    @DescripcionUbicacionProducto VARCHAR(100)
AS
BEGIN
    UPDATE UbicacionProducto
    SET NombreUbicacionProducto = @NombreUbicacionProducto,
        DescripcionUbicacionProducto = @DescripcionUbicacionProducto
    WHERE IdUbicacionProducto = @IdUbicacionProducto;
END
GO

-- DETALLE de una ubicación específica por ID
CREATE PROCEDURE sp_ObtenerUbicacionPorId
    @IdUbicacionProducto INT
AS
BEGIN
    SELECT IdUbicacionProducto, NombreUbicacionProducto, DescripcionUbicacionProducto
    FROM UbicacionProducto
    WHERE IdUbicacionProducto = @IdUbicacionProducto;
END
GO
-- =========================================
-- CRUD Producto
-- =========================================
-- Listar Productos
CREATE PROCEDURE sp_ListarProductos
AS
BEGIN
    SELECT 
        P.IdProducto,
        P.NombreProducto,
        P.DescripcionProducto,
        P.IdUbicacionProducto,
        U.NombreUbicacionProducto,
        P.CantidadProducto,
        P.CaducidadProducto,
        P.MarcaProducto,
        P.EstadoProducto
    FROM Producto P
    INNER JOIN UbicacionProducto U ON P.IdUbicacionProducto = U.IdUbicacionProducto;
END;
GO

-- Crear Producto
CREATE PROCEDURE sp_CrearProducto
    @NombreProducto VARCHAR(100),
    @DescripcionProducto VARCHAR(100),
    @IdUbicacionProducto INT,
    @CantidadProducto INT,
    @CaducidadProducto DATE,
    @MarcaProducto VARCHAR(100),
    @EstadoProducto BIT
AS
BEGIN
    INSERT INTO Producto (NombreProducto, DescripcionProducto, IdUbicacionProducto, CantidadProducto, CaducidadProducto, MarcaProducto, EstadoProducto)
    VALUES (@NombreProducto, @DescripcionProducto, @IdUbicacionProducto, @CantidadProducto, @CaducidadProducto, @MarcaProducto, @EstadoProducto);
END;
GO

-- Editar Producto
CREATE PROCEDURE sp_EditarProducto
    @IdProducto INT,
    @NombreProducto VARCHAR(100),
    @DescripcionProducto VARCHAR(100),
    @IdUbicacionProducto INT,
    @CantidadProducto INT,
    @CaducidadProducto DATE,
    @MarcaProducto VARCHAR(100),
    @EstadoProducto BIT
AS
BEGIN
    UPDATE Producto
    SET NombreProducto = @NombreProducto,
        DescripcionProducto = @DescripcionProducto,
        IdUbicacionProducto = @IdUbicacionProducto,
        CantidadProducto = @CantidadProducto,
        CaducidadProducto = @CaducidadProducto,
        MarcaProducto = @MarcaProducto,
        EstadoProducto = @EstadoProducto
    WHERE IdProducto = @IdProducto;
END;
GO

-- Detalle de Producto
CREATE PROCEDURE sp_ObtenerProductoPorId
    @IdProducto INT
AS
BEGIN
    SELECT 
        P.IdProducto,
        P.NombreProducto,
        P.DescripcionProducto,
        P.IdUbicacionProducto,
        U.NombreUbicacionProducto,
        P.CantidadProducto,
        P.CaducidadProducto,
        P.MarcaProducto,
        P.EstadoProducto
    FROM Producto P
    INNER JOIN UbicacionProducto U ON P.IdUbicacionProducto = U.IdUbicacionProducto
    WHERE P.IdProducto = @IdProducto;
END;
GO

-- Cambiar EstadoProducto a Inactivo
CREATE PROCEDURE sp_InactivarProducto
    @IdProducto INT
AS
BEGIN
    UPDATE Producto
    SET EstadoProducto = 0
    WHERE IdProducto = @IdProducto;
END;
GO
