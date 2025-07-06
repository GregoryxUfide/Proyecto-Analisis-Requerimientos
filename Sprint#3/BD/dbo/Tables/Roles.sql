CREATE TABLE [dbo].[Roles] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Nombre] NVARCHAR (50) NOT NULL,
    [Descripcion] NVARCHAR(500) NOT NULL, 
    [Estado] BIT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

