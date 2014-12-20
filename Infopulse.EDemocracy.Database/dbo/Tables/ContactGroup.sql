CREATE TABLE [dbo].[ContactGroup] (
    [ID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [GroupID]       BIGINT         NOT NULL,
    [ContactTypeID] BIGINT         NOT NULL,
    [StatusID]      BIGINT         NOT NULL,
    [Value]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ContactGroup] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ContactGroup_Entity] FOREIGN KEY ([ContactTypeID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_ContactGroup_Entity1] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_ContactGroup_Groups] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Groups] ([ID])
);

