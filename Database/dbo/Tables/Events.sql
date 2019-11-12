CREATE TABLE [dbo].[Events] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CalendarId]   INT            NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Notification] NVARCHAR (MAX) NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [TimeStart]    DATETIME       NULL,
    [TimeFinish]   DATETIME       NULL,
    [AllDay]       BIT            NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_Calendars] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendars] ([Id])
);

