
CREATE PROCEDURE sp_ObtenerTokenRecuperacion
    @UsuarioId INT,
    @Token NVARCHAR(100)
AS
BEGIN
    SELECT Id, UsuarioId, Token, FechaCreacion, FechaExpiracion, Usado
    FROM TokensRecuperacion
    WHERE UsuarioId = @UsuarioId AND Token = @Token AND Usado = 0 AND FechaExpiracion > GETDATE();
END