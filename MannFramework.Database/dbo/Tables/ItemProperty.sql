CREATE TABLE [dbo].[ItemProperty] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [InsertTime]  DATETIME      DEFAULT (getdate()) NOT NULL,
    [DeleteTime]  DATETIME      NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [IsNullable]  BIT           NOT NULL,
    [Type]        INT           NOT NULL,
    [MappingType] INT           NOT NULL,
    [InnerTypeId] INT           NULL,
    [MinLength]   INT           NOT NULL,
    [MaxLength]   INT           NOT NULL,
    [IsUnicode]   BIT           NOT NULL,
    [ItemId]      INT           NOT NULL,
    [AccessorType] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([InnerTypeId]) REFERENCES [dbo].[Item] ([Id]),
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Item] ([Id])
);

