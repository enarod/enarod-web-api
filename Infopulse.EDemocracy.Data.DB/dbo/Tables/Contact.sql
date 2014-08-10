CREATE TABLE [dbo].[Contact] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [ParticipantID] BIGINT        NOT NULL,
    [Phone]         VARCHAR (32)  NULL,
    [WorkPhone]     VARCHAR (32)  NULL,
    [CellPhone]     VARCHAR (32)  NULL,
    [email]         VARCHAR (MAX) NULL,
    [Actual]        BIT           NOT NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Contact_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ID])
);

