CREATE TABLE [dbo].[DayScheduleOverride]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WeekScheduleId] INT NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_DayScheduleOverride_WeekSchedule] FOREIGN KEY ([WeekScheduleId]) REFERENCES [WeekSchedule]([Id])
)
