CREATE TABLE [dbo].[TimeUnits] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_TimeUnits] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [TimeUnits_Id_uindex]
    ON [dbo].[TimeUnits]([Id] ASC);

