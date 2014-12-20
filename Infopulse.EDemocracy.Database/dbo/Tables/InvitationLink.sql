CREATE TABLE [dbo].[InvitationLink] (
    [ID]           UNIQUEIDENTIFIER NOT NULL,
    [InvitationID] BIGINT           NOT NULL,
    [StatusID]     BIGINT           NOT NULL,
    [CreatedDate]  DATETIME         NOT NULL,
    [CreatedBy]    BIGINT           NOT NULL,
    [RunDate]      DATETIME         NULL,
    [RunBy]        BIGINT           NULL,
    CONSTRAINT [PK_InvitationLink] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_InvitationLink_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_InvitationLink_Invitation] FOREIGN KEY ([InvitationID]) REFERENCES [dbo].[Invitation] ([ID])
);

