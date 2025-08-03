CREATE TABLE [dbo].[PuntoVenta] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [DescripcionVenta] NVARCHAR (200)  NOT NULL,
    [Metodo_Pago]      NVARCHAR (50)   NOT NULL,
    [Descuento]        DECIMAL (10, 2) NOT NULL,
    [ReservaId]        INT             NOT NULL,
    [EmpleadoId]       INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PuntoVenta_Empleado] FOREIGN KEY ([EmpleadoId]) REFERENCES [dbo].[Empleado] ([Id]),
    CONSTRAINT [FK_PuntoVenta_Reserva] FOREIGN KEY ([ReservaId]) REFERENCES [dbo].[Reservas] ([IdReserva])
);

