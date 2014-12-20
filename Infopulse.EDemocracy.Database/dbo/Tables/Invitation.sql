CREATE TABLE [dbo].[Invitation] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [GroupID]     BIGINT         NOT NULL,
    [PersonID]    BIGINT         NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   BIGINT         NOT NULL,
    CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Invitation_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Invitation_Groups] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Groups] ([ID]),
    CONSTRAINT [FK_Invitation_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

