CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Email] varchar(255) NOT NULL,
	[TeamId] INT NOT NULL,
	[Password] varchar(255) NOT NULL,
	[ImageURL] varchar(255) NULL,
	FOREIGN KEY ([TeamId]) REFERENCES Team(ID)
)
