CREATE PROCEDURE sp_GuardarTokenRecuperacion
    @UsuarioId INT,
    @Token NVARCHAR(100),
    @FechaExpiracion DATETIME
AS
BEGIN
    INSERT INTO TokensRecuperacion (UsuarioId, Token, FechaCreacion, FechaExpiracion, Usado)
    VALUES (@UsuarioId, @Token, GETDATE(), @FechaExpiracion, 0);
END