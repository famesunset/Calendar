CREATE TABLE [dbo].[Users] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)  NOT NULL,
    [Mobile]            NVARCHAR (50)  NULL,
    [Email]             NVARCHAR (50)  NOT NULL,
    [CalendarDefaultId] INT            NOT NULL,
    [IdentityId]        NVARCHAR (MAX) NOT NULL,
    [Picture]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

