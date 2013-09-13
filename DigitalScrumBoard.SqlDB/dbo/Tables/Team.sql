CREATE TABLE [dbo].[Team] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (255) NOT NULL

    CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([ID] ASC)
);

