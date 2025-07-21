-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_EstadoEmpleado
	@Id INT,
    @Estado BIT
AS
BEGIN
    UPDATE Empleado
    SET Estado = @Estado
    WHERE Id = @Id
END