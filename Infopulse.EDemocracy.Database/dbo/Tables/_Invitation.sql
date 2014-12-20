CREATE TABLE [dbo].[_Invitation] (
    [ID]           BIGINT   IDENTITY (1, 1) NOT NULL,
    [InvitationID] BIGINT   NOT NULL,
    [ActionID]     BIGINT   NOT NULL,
    [StatusID]     BIGINT   NOT NULL,
    [CreatedDate]  DATETIME NOT NULL,
    [CreatedBy]    BIGINT   NOT NULL,
    CONSTRAINT [PK__Invitation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__Invitation_Entity] FOREIGN KEY ([ActionID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK__Invitation_Entity1] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK__Invitation_Invitation] FOREIGN KEY ([InvitationID]) REFERENCES [dbo].[Invitation] ([ID])
);

