CREATE TABLE [dbo].[Repeat] (
    [Interval] NCHAR (10) NOT NULL,
    [Id]       INT        NOT NULL,
    CONSTRAINT [PK_Repeat] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20191129-053914]
    ON [dbo].[Repeat]([Interval] ASC, [Id] ASC);

