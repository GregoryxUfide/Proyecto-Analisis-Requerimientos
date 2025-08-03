CREATE PROCEDURE sp_ActualizarPuntoVenta
    @Id INT,
    @DescripcionVenta NVARCHAR(200),
    @Metodo_Pago NVARCHAR(50),
    @Descuento DECIMAL(10,2),
    @ReservaId INT,
    @EmpleadoId INT
AS
BEGIN
    UPDATE PuntoVenta
    SET 
        DescripcionVenta = @DescripcionVenta,
        Metodo_Pago = @Metodo_Pago,
        Descuento = @Descuento,
        ReservaId = @ReservaId,
        EmpleadoId = @EmpleadoId
    WHERE Id = @Id;
END