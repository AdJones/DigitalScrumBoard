CREATE TABLE [dbo].[Sprint] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Goals]			VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_Sprint] PRIMARY KEY CLUSTERED ([ID] ASC)
);

