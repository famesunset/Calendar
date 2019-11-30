CREATE TABLE [dbo].[UsersCalendars] (
    [UserId]     INT NOT NULL,
    [CalendarId] INT NOT NULL,
    [IsAccepted] BIT DEFAULT ((0)) NOT NULL,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE,
    CONSTRAINT [FK_UsersCalendars_Calendars] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendars] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_UsersCalendars]
    ON [dbo].[UsersCalendars]([UserId] ASC) WITH (STATISTICS_NORECOMPUTE = ON);

