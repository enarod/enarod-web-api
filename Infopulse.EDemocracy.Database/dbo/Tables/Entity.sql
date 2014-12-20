CREATE TABLE [dbo].[Entity] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [EntityGroupID] BIGINT        NOT NULL,
    [Name]          VARCHAR (MAX) NOT NULL,
    [Description]   VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Entity_EntityGroup] FOREIGN KEY ([EntityGroupID]) REFERENCES [dbo].[EntityGroup] ([ID])
);

