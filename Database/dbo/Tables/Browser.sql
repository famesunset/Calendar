CREATE TABLE [dbo].[Browser] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Browser] NVARCHAR (MAX) NOT NULL,
    [UserId]  INT            NOT NULL,
    CONSTRAINT [Browser_pk] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Browser_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

