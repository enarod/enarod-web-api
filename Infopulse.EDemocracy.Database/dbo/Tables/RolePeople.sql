CREATE TABLE [dbo].[RolePeople] (
    [ID]       BIGINT IDENTITY (1, 1) NOT NULL,
    [GroupID]  BIGINT NOT NULL,
    [RoleID]   BIGINT NOT NULL,
    [PersonID] BIGINT NOT NULL,
    [StatusID] BIGINT NOT NULL,
    CONSTRAINT [PK_RolePeople] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RolePeople_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_RolePeople_Groups] FOREIGN KEY ([GroupID]) REFERENCES [dbo].[Groups] ([ID]),
    CONSTRAINT [FK_RolePeople_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [FK_RolePeople_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([ID])
);

