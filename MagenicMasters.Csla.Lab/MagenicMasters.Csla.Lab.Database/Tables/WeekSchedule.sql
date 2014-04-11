CREATE TABLE [dbo].[WeekSchedule]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DesignerId] INT NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [IntervalsInMinutes] INT NOT NULL, 
    [MaxHours] INT NOT NULL, 
    [IncludeHolidays] BIT NOT NULL, 
    CONSTRAINT [FK_WeekSchedule_Designer] FOREIGN KEY ([DesignerId]) REFERENCES [Designer]([Id])
)
