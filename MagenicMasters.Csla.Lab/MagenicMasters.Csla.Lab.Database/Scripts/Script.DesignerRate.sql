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

SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[DesignerRate] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[DesignerRate] AS Target
USING (VALUES
	(1,1,300),
	(2,2,400),
	(3,3,340),
	(4,4,360)

)
AS Source([Id], [DesignerId], [Rate])
ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
    UPDATE SET 
	[DesignerId] = Source.[DesignerId],
	[Rate] = Source.[Rate]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [DesignerId], [Rate])
VALUES (
	Source.[Id], 
	Source.[DesignerId],
	Source.[Rate]
);
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[DesignerRate] OFF