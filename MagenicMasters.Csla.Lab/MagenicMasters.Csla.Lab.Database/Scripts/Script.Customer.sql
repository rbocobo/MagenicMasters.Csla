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

SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Customer] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[Customer] AS Target
USING (VALUES
	(1,'Edmure Tully'),
	(2,'Margaery Tyrell'),
	(3,'Theon Greyjoy')
)
AS Source([Id], [Name])
ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
    UPDATE SET 
	[Name] = Source.[Name]
	
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Name])
VALUES (
	Source.[Id], 
	Source.[Name]
);
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Customer] OFF