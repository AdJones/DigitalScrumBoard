CREATE TABLE [dbo].[Task] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Text]          VARCHAR (255) NOT NULL,
    [Left]          INT           NOT NULL,
    [Top]           INT           NOT NULL,
    [Type]          INT           NOT NULL,
    [OwnerID]       INT           NOT NULL,
    [StoryID]      INT           NOT NULL,
    [CurrentCol]    VARCHAR (50)  NOT NULL,
    [TimeRemaining] INT           NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([ID] ASC),
	FOREIGN KEY ([StoryID]) REFERENCES Story(ID)
);

