    CREATE TABLE [dbo].[Reservas] (
    [IdReserva]    INT            IDENTITY (1, 1) NOT NULL,
    [FechaInicio]  DATETIME       NOT NULL,
    [FechaFinal]   DATETIME       NOT NULL,
    [Nombre]       NVARCHAR (100) NOT NULL,
    [Telefono]     NVARCHAR (20)  NOT NULL,
    [Correo]       NVARCHAR (100) NOT NULL,
    [HabitacionId] INT            NOT NULL,
    [Estado]       BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdReserva] ASC),
    CONSTRAINT [FK_Reserva_Habitacion] FOREIGN KEY ([HabitacionId]) REFERENCES [dbo].[Habitacion] ([Id])
);

