CREATE TABLE [dbo].[Sessions] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Token]           NVARCHAR (MAX) NOT NULL,
    [TimeLastEntered] DATETIME       NOT NULL,
    [UserId]          INT            NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

