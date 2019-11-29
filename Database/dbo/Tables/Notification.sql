CREATE TABLE [dbo].[Notification] (
    [EventId]    INT NOT NULL,
    [Before]     INT NOT NULL,
    [TimeUnitId] INT NOT NULL,
    CONSTRAINT [FK_Notification_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]),
    CONSTRAINT [FK_Notification_TimeUnits] FOREIGN KEY ([TimeUnitId]) REFERENCES [dbo].[TimeUnits] ([Id])
);

