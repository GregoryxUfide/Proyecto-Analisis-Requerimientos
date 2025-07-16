    CREATE TABLE [dbo].[Reservas]
    (
	    [IdReserva] INT IDENTITY(1,1) PRIMARY KEY,
        [fechaInicio] DATE NOT NULL, 
        [fechaFinal] DATE NOT NULL, 
        [nombreReservante] NVARCHAR(50) NOT NULL, 
        [telefono] INT NOT NULL, 
        [correo] VARCHAR(50) NULL, 
        [numHabitacion] INT NOT NULL 
        FOREIGN KEY ([numHabitacion]) REFERENCES [dbo].[Habitacion]([NumHabitacion])

    )
