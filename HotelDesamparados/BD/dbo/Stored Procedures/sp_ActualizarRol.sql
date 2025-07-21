
CREATE PROCEDURE sp_ActualizarRol
@Id INT,
    @Nombre NVARCHAR(50),
    @Descripcion NVARCHAR(500)
AS
BEGIN
    UPDATE Roles
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE Id = @Id;
END;