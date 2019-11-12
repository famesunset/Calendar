﻿CREATE TABLE [dbo].[Calendars] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [AccessId] INT           NOT NULL,
    CONSTRAINT [PK_Calendars] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Calendars_Access] FOREIGN KEY ([AccessId]) REFERENCES [dbo].[Access] ([Id]) ON UPDATE CASCADE
);
