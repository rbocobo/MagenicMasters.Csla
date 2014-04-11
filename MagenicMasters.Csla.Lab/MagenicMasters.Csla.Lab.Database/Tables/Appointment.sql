CREATE TABLE [dbo].[Appointment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [DesignerId] INT NOT NULL, 
    [SpecialtyId] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Fee] DECIMAL NOT NULL, 
    [PartialFee] DECIMAL NOT NULL, 
    [CancelWindow] INT NOT NULL, 
    [Status] INT NOT NULL, 
    CONSTRAINT [FK_Appointment_ToCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]), 
    CONSTRAINT [FK_Appointment_ToDesigner] FOREIGN KEY ([DesignerId]) REFERENCES [Designer]([Id])
)
