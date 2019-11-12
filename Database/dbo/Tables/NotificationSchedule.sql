CREATE TABLE [dbo].[NotificationSchedule] (
    [Id]               INT      IDENTITY (1, 1) NOT NULL,
    [EventScheduleId]  INT      NOT NULL,
    [NotificationTime] DATETIME NOT NULL,
    CONSTRAINT [PK_NotificationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NotificationSchedule_EventSchedule] FOREIGN KEY ([EventScheduleId]) REFERENCES [dbo].[EventSchedule] ([Id])
);

