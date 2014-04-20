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


SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[DesignerSpecialty] ON
MERGE INTO [MagenicMasters.Csla].[dbo].[DesignerSpecialty] AS Target
USING (VALUES
	(1,1,1),
	(2,2,1),
	(3,3,1),
	(4,4,1),
	(5,1,2),
	(6,2,2),
	(7,3,2),
	(8,4,2),
	(9,1,3),
	(10,2,3),
	(11,3,3),
	(12,4,3)

)
AS Source([Id], [DesignerId], [SpecialtyId])
ON Target.[Id] = Source.[Id]
WHEN MATCHED THEN 
    UPDATE SET 
	[DesignerId] = Source.[DesignerId],
	[SpecialtyId] = Source.[SpecialtyId]
WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id], [DesignerId], [SpecialtyId])
VALUES (
	Source.[Id], 
	Source.[DesignerId],
	Source.[SpecialtyId]
);
SET IDENTITY_INSERT [MagenicMasters.Csla].[dbo].[DesignerSpecialty] OFF