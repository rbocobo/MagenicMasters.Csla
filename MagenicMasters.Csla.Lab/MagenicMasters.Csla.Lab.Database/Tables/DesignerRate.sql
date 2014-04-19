CREATE TABLE [dbo].[DesignerRate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DesignerId] INT NOT NULL, 
    [Rate] DECIMAL NOT NULL, 
    CONSTRAINT [FK_DesignerRate_ToDesigner] FOREIGN KEY ([DesignerId]) REFERENCES [Designer]([Id])
)
