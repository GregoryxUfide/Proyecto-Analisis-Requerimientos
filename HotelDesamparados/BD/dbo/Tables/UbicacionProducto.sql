CREATE TABLE [dbo].[UbicacionProducto] (
    [IdUbicacionProducto]          INT           IDENTITY (1, 1) NOT NULL,
    [NombreUbicacionProducto]      VARCHAR (100) NOT NULL,
    [DescripcionUbicacionProducto] VARCHAR (100) NOT NULL,
    [Estado]                       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUbicacionProducto] ASC)
);

