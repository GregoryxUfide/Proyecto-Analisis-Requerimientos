CREATE TABLE [dbo].[Habitacion] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Capacidad]     INT             NOT NULL,
    [Precio]        DECIMAL (10, 2) NOT NULL,
    [NumHabitacion] INT             NOT NULL,
    [NumCamas]      INT             NOT NULL,
    [Extras]        NVARCHAR (500)  NULL,
    [Comentarios]   NVARCHAR (500)  NULL,
    [Estado]        BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_NumHabitacion] UNIQUE NONCLUSTERED ([NumHabitacion] ASC)
);

