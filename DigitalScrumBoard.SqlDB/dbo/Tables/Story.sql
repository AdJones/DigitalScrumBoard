CREATE TABLE [dbo].[Story] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Text]			VARCHAR(255) NOT NULL,
	[SprintId]		INT NOT NULL,
    CONSTRAINT [PK_Story] PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY ([SprintId]) REFERENCES Sprint(ID)
);

