﻿/*
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
	(1,1,2),
	(2,2,2),
	(3,3,2),
	(4,4,2),
	(1,1,3),
	(2,2,3),
	(3,3,3),
	(4,4,3)

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