CREATE TABLE [dbo].[Sprint] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
	[Name]			VARCHAR(50) NULL,
    [Goals]			VARCHAR(255) NOT NULL,
	[StartDate]		DATETIME NULL,
	[EndDate]	DATETIME NULL,
	[TeamId]	INT NULL
    CONSTRAINT [PK_Sprint] PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY ([TeamId]) REFERENCES Team(ID)
);

