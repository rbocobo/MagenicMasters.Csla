CREATE TABLE [dbo].[DesignerRate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Description] NVARCHAR(50) NOT NULL, 
    [Rate] DECIMAL NOT NULL
)
