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

:r .\Script.Customer.sql

:r .\Script.Designer.sql

:r .\Script.Cancellation.sql

:r .\Script.PostDeployment.sql

:r .\Script.DesignerRate.sql

:r .\Script.DesignerSpecialty.sql

