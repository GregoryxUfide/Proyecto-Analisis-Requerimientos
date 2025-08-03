CREATE PROCEDURE sp_CrearHabitacion
    @Capacidad INT,
    @Precio DECIMAL(10, 2),
    @NumHabitacion INT,
    @NumCamas INT,
    @Extras NVARCHAR(255),
    @Comentarios NVARCHAR(500)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Habitacion WHERE NumHabitacion = @NumHabitacion)
    BEGIN
        RAISERROR('El número de habitación ya existe en el sistema.', 16, 1);
        RETURN -1;
    END

    IF @Capacidad <= 0 OR @Precio <= 0 OR @NumCamas <= 0
    BEGIN
        RAISERROR('Capacidad, Precio y Número de Camas deben ser valores positivos.', 16, 1);
        RETURN -2;
    END

    INSERT INTO Habitacion (Capacidad, Precio, NumHabitacion, NumCamas, Extras, Comentarios, Estado)
    VALUES (@Capacidad, @Precio, @NumHabitacion, @NumCamas, @Extras, @Comentarios, 1);
END;