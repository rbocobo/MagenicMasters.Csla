CREATE TABLE [dbo].[Cancellation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Window] INT NOT NULL, 
    [Fee] DECIMAL NOT NULL,
)
