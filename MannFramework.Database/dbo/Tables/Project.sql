CREATE TABLE [dbo].[Project] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [InsertTime] DATETIME       DEFAULT (getdate()) NOT NULL,
    [DeleteTime] DATETIME       NULL,
    [Name]       NVARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

