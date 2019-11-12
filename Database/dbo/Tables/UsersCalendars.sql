CREATE TABLE [dbo].[UsersCalendars] (
    [UserId]     INT NOT NULL,
    [CalendarId] INT NOT NULL,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON UPDATE CASCADE,
    CONSTRAINT [FK_UsersCalendars_Calendars] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendars] ([Id])
);

