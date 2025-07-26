
CREATE PROCEDURE sp_MarcarTokenComoUsado
    @Id INT
AS
BEGIN
    UPDATE TokensRecuperacion SET Usado = 1 WHERE Id = @Id;
END