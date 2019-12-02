CREATE TABLE [dbo].[Events] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CalendarId]   INT            NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [TimeStart]    DATETIME       NOT NULL,
    [TimeFinish]   DATETIME       NOT NULL,
    [AllDay]       BIT            NOT NULL,
    [RepeatId]     INT            NOT NULL,
    [CreationDate] DATETIME       DEFAULT (getdate()) NOT NULL,
	[CreatorId]	   INT			   NOT NULL
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Events_Calendars] FOREIGN KEY ([CalendarId]) REFERENCES [dbo].[Calendars] ([Id]),
    CONSTRAINT [FK_Events_Repeat] FOREIGN KEY ([RepeatId]) REFERENCES [dbo].[Repeat] ([Id]),
    CONSTRAINT [FK_Creator_User] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20191129-060201]
    ON [dbo].[Events]([Id] ASC, [CalendarId] ASC);

