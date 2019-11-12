CREATE TABLE [dbo].[NotificationInfinity] (
    [EventId]            INT NOT NULL,
    [NotificationMinute] INT NOT NULL,
    CONSTRAINT [FK_NotificationInfinity_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id])
);

