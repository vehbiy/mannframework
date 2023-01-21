CREATE TABLE [dbo].[Item] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [InsertTime] DATETIME      DEFAULT (getdate()) NOT NULL,
    [DeleteTime] DATETIME      NULL,
    [Name]       VARCHAR (100) NOT NULL,
    [IsEnum]     BIT           NOT NULL,
    [ProjectId]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id])
);

