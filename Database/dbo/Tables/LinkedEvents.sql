CREATE TABLE [dbo].[LinkedEvents] (
    [EventBaseId]   INT NOT NULL,
    [EventLinkedId] INT NOT NULL,
    CONSTRAINT [PK_LinkedEvents] PRIMARY KEY CLUSTERED ([EventBaseId] ASC),
    CONSTRAINT [FK_LinkedEvents_Events] FOREIGN KEY ([EventBaseId]) REFERENCES [dbo].[Events] ([Id])
);

