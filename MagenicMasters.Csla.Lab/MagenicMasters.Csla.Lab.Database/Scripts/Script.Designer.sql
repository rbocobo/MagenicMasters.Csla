/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Designer] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[Designer] AS Target
USING (VALUES
	(1,'Ned Stark', 1),
	(2,'Aegon Targaryen', 0),
	(3,'Roose Bolton', 1),
	(4,'Tywin Lannister', 0)
)
AS Source([Id], [Name], [IsFull])
ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
    UPDATE SET 
	[Name] = Source.[Name],
	[IsFull] = Source.[IsFull]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name], [IsFull])
VALUES (
	Source.[Id], 
	Source.[Name],
	Source.[IsFull]
);
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Designer] OFF