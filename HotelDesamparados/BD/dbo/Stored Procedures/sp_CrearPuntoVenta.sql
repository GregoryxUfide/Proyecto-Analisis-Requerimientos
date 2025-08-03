CREATE PROCEDURE sp_CrearPuntoVenta
    @DescripcionVenta NVARCHAR(200),
    @Metodo_Pago NVARCHAR(50),
    @Descuento DECIMAL(10,2),
    @ReservaId INT,
    @EmpleadoId INT
AS
BEGIN
    INSERT INTO PuntoVenta (DescripcionVenta, Metodo_Pago, Descuento, ReservaId, EmpleadoId)
    VALUES (@DescripcionVenta, @Metodo_Pago, @Descuento, @ReservaId, @EmpleadoId);
END