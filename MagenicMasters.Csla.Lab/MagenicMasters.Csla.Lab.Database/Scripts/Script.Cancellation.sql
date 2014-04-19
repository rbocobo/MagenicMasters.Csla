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

SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Cancellation] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[Cancellation] AS Target
USING (VALUES
	(1,5,100)
)
AS Source([Id], [Window], [Fee])
ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
    UPDATE SET 
	[Window] = Source.[Window],
	[Fee] = Source.[Fee]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [Window], [Fee])
VALUES (
	Source.[Id], 
	Source.[Window],
	Source.[Fee]
);
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[Cancellation] OFF