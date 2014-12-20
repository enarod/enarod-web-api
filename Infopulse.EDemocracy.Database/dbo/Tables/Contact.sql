CREATE TABLE [dbo].[Contact] (
    [ID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [PersonID]      BIGINT         NOT NULL,
    [ContactTypeID] BIGINT         NOT NULL,
    [StatusID]      BIGINT         NOT NULL,
    [Value]         NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Contact_Entity] FOREIGN KEY ([ContactTypeID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Contact_Entity1] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Contact_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

