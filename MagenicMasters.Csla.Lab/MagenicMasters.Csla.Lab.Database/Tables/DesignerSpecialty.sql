CREATE TABLE [dbo].[DesignerSpecialty]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DesignerId] INT NOT NULL, 
    [SpecialtyId] INT NOT NULL, 
    CONSTRAINT [FK_DesignerSpecialty_Designer] FOREIGN KEY ([DesignerId]) REFERENCES [Designer]([Id]), 
    CONSTRAINT [FK_DesignerSpecialty_Specialty] FOREIGN KEY ([SpecialtyId]) REFERENCES [Specialty]([Id])
)
