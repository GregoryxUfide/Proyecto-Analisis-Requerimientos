CREATE TABLE [dbo].[Producto] (
    [IdProducto]          INT           IDENTITY (1, 1) NOT NULL,
    [NombreProducto]      VARCHAR (100) NOT NULL,
    [DescripcionProducto] VARCHAR (100) NULL,
    [IdUbicacionProducto] INT           NOT NULL,
    [CantidadProducto]    INT           NOT NULL,
    [CaducidadProducto]   DATE          NULL,
    [MarcaProducto]       VARCHAR (100) NULL,
    [EstadoProducto]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProducto] ASC),
    FOREIGN KEY ([IdUbicacionProducto]) REFERENCES [dbo].[UbicacionProducto] ([IdUbicacionProducto])
);

