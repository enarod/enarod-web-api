CREATE TABLE [dbo].[RoleFunction] (
    [ID]         BIGINT IDENTITY (1, 1) NOT NULL,
    [RoleID]     BIGINT NOT NULL,
    [FunctionID] BIGINT NOT NULL,
    [StatusID]   BIGINT NOT NULL,
    CONSTRAINT [PK_RoleFunction] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RoleFunction_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_RoleFunction_Function] FOREIGN KEY ([FunctionID]) REFERENCES [dbo].[Function] ([ID]),
    CONSTRAINT [FK_RoleFunction_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([ID])
);

