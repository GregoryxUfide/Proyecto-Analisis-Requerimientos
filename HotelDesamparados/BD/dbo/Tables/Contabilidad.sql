CREATE TABLE [dbo].[Contabilidad] (
    [IdContabilidad] INT           IDENTITY (1, 1) NOT NULL,
    [Fecha]          DATE          NOT NULL,
    [Monto]          DECIMAL (18)  NOT NULL,
    [Detalle]        VARCHAR (MAX) NOT NULL,
    [Comentario]     VARCHAR (MAX) NOT NULL
);

