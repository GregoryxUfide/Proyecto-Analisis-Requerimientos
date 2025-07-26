CREATE TABLE [dbo].[TokensRecuperacion] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [UsuarioId]       INT            NOT NULL,
    [Token]           NVARCHAR (100) NOT NULL,
    [FechaCreacion]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [FechaExpiracion] DATETIME       NOT NULL,
    [Usado]           BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TokensRecuperacion_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);

