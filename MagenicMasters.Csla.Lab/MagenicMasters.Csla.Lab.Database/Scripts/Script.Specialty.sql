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

SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Specialty] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[Specialty] AS Target
USING (VALUES
	(1,'Kitchen Design'),
	(2,'Bathroom Design'),
	(3,'Bedroom Design'),
	(4,'Landscape Design'),
	(5,'Living Area Design'),
	(6,'Pool and pool area design'),
	(7,'Home Architecture')
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
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Specialty] OFF