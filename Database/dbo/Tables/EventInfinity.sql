CREATE TABLE [dbo].[EventInfinity] (
    [EventId]   INT      NOT NULL,
    [TimeStart] DATETIME NOT NULL,
    [RepeatId]  INT      NOT NULL,
    CONSTRAINT [FK_EventInfinity_Events] FOREIGN KEY ([EventId]) REFERENCES [dbo].[Events] ([Id]),
    CONSTRAINT [FK_EventInfinity_Repeat] FOREIGN KEY ([RepeatId]) REFERENCES [dbo].[Repeat] ([Id])
);

