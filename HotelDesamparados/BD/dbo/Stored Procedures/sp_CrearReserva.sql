CREATE PROCEDURE sp_CrearReserva
    @fechaInicio DATE,
    @fechaFinal DATE,
    @nombreReservante NVARCHAR(50),
    @telefono INT,
    @correo VARCHAR(50),
    @numHabitacion INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si la habitación existe
    IF NOT EXISTS (SELECT 1 FROM dbo.Habitacion WHERE NumHabitacion = @numHabitacion)
    BEGIN
        SELECT -1 AS Codigo, 'Error: La habitación especificada no existe.' AS Mensaje;
        RETURN;
    END

    -- Verificar si la habitación ya está reservada en el rango de fechas
    IF EXISTS (
        SELECT 1
        FROM dbo.Reservas
        WHERE numHabitacion = @numHabitacion
          AND (
              (@fechaInicio BETWEEN fechaInicio AND fechaFinal)
              OR (@fechaFinal BETWEEN fechaInicio AND fechaFinal)
              OR (fechaInicio BETWEEN @fechaInicio AND @fechaFinal)
              OR (fechaFinal BETWEEN @fechaInicio AND @fechaFinal)
          )
    )
    BEGIN
        SELECT -2 AS Codigo, 'Error: La habitación ya está reservada en esas fechas.' AS Mensaje;
        RETURN;
    END

    -- Insertar nueva reserva
    INSERT INTO dbo.Reservas
    (
        fechaInicio,
        fechaFinal,
        nombreReservante,
        telefono,
        correo,
        numHabitacion
    )
    VALUES
    (
        @fechaInicio,
        @fechaFinal,
        @nombreReservante,
        @telefono,
        @correo,
        @numHabitacion
    );

    SELECT 1 AS Codigo, 'Reserva creada correctamente.' AS Mensaje;
END;
