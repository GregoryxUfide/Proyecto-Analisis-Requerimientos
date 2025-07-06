CREATE TABLE [dbo].[Usuarios] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]     NVARCHAR (50)  NOT NULL,
    [Apellidos]  NVARCHAR (100) NOT NULL,
    [Gmail]      NVARCHAR (100) NOT NULL,
    [Username]   NVARCHAR (50)  NOT NULL,
    [Contrasena] NVARCHAR (200) NOT NULL,
    [Estado]     BIT            NOT NULL,
    [RolId]      INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([RolId]) REFERENCES [dbo].[Roles] ([Id]),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

