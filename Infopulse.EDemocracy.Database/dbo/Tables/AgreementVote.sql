CREATE TABLE [dbo].[AgreementVote] (
    [ID]            BIGINT   IDENTITY (1, 1) NOT NULL,
    [AgreementID]   BIGINT   NOT NULL,
    [PersonID]      BIGINT   NOT NULL,
    [CertificateID] BIGINT   NOT NULL,
    [CreatedDate]   DATETIME NOT NULL,
    [SignedData]    TEXT     NOT NULL,
    [SignedHash]    TEXT     NOT NULL,
    CONSTRAINT [PK_AgreementVote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AgreementVote_Agreement] FOREIGN KEY ([AgreementID]) REFERENCES [dbo].[Agreement] ([ID]),
    CONSTRAINT [FK_AgreementVote_Certificate] FOREIGN KEY ([CertificateID]) REFERENCES [dbo].[Certificate] ([ID]),
    CONSTRAINT [FK_AgreementVote_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

