CREATE TABLE [dbo].[LimpiezaHabitacion] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [TareasCompletadas] NVARCHAR (500)  NOT NULL,
    [NombreConserje]    NVARCHAR (100)  NOT NULL,
    [Foto]              VARBINARY (MAX) NULL,
    [FechaHora]         DATETIME        DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

