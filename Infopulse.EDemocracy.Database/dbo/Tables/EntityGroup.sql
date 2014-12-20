CREATE TABLE [dbo].[EntityGroup] (
    [ID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [ParentID] BIGINT        NULL,
    [Name]     VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_EntityGroup] PRIMARY KEY CLUSTERED ([ID] ASC)
);

