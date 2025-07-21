CREATE TABLE [dbo].[Empleado] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [NumeroEmpleado]  VARCHAR (50)    NOT NULL,
    [SalarioEmpleado] DECIMAL (18, 2) NOT NULL,
    [UsuarioId]       INT             NOT NULL,
    [RolId]           INT             NOT NULL,
    [Estado]          BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Empleado_Rol] FOREIGN KEY ([RolId]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_Empleado_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);

