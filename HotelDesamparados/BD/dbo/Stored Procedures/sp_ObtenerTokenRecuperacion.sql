CREATE PROCEDURE sp_ObtenerTokenRecuperacion
    @UsuarioId INT
AS
BEGIN
    SELECT TOP 1 Id, UsuarioId, Token, FechaCreacion, FechaExpiracion, Usado
    FROM TokensRecuperacion
    WHERE UsuarioId = @UsuarioId
      AND Usado = 0
      AND FechaExpiracion > GETDATE()
    ORDER BY FechaCreacion DESC;
END
