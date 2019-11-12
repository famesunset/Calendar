CREATE TABLE [dbo].[EventSchedule] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [EventId]    INT      NOT NULL,
    [TimeStart]  DATETIME NOT NULL,
    [TimeFInish] DATETIME NOT NULL,
    CONSTRAINT [PK_EventSchedule_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventSchedule_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id])
);

