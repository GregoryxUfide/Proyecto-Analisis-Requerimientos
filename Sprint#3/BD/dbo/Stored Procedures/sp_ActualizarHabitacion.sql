
CREATE PROCEDURE sp_ActualizarHabitacion
    @Id INT,
    @Capacidad INT,
    @Precio DECIMAL(10, 2),
    @NumHabitacion INT,
    @NumCamas INT,
    @Extras NVARCHAR(255),
    @Comentarios NVARCHAR(500)
AS
BEGIN
 IF EXISTS (SELECT 1 FROM [dbo].[Habitacion] 
                  WHERE [NumHabitacion] = @NumHabitacion AND [Id] <> @Id)
        BEGIN
            RAISERROR('El número de habitación ya está asignado a otra habitación.', 16, 1);
            RETURN;
        END
		 IF @Capacidad <= 0 OR @Precio <= 0 OR @NumCamas <= 0
        BEGIN
            RAISERROR('Capacidad, Precio y Número de Camas deben ser valores positivos.', 16, 1);
            RETURN -2;
        
        END
    UPDATE Habitacion
    SET Capacidad = @Capacidad,
        Precio = @Precio,
        NumHabitacion = @NumHabitacion,
        NumCamas = @NumCamas,
        Extras = @Extras,
        Comentarios = @Comentarios
    WHERE Id = @Id;
END;