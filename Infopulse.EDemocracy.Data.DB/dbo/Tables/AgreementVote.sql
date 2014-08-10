CREATE TABLE [dbo].[AgreementVote] (
    [ID]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [AgreementID]           BIGINT         NOT NULL,
    [AgreementText]         NVARCHAR (MAX) NOT NULL,
	[AgreementHash]         VARCHAR(MAX)   NOT NULL,
    [ParticipantID]         BIGINT         NOT NULL,
    [SignatureHash]         VARCHAR (MAX)  NOT NULL,
    [CertificateThumbPrint] VARCHAR (MAX)  NOT NULL,
	[Issuer]                INT            NOT NULL,
    [CreatedDate]           DATETIME       NOT NULL,
    CONSTRAINT [PK_AgreementVote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AgreementVote_Agreement] FOREIGN KEY ([AgreementID]) REFERENCES [dbo].[Agreement] ([ID]),
    CONSTRAINT [FK_AgreementVote_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_AgreementVote_AgreementID]
    ON [dbo].[AgreementVote]([AgreementID] ASC);

