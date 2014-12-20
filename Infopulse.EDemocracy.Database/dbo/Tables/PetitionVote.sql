CREATE TABLE [dbo].[PetitionVote] (
    [ID]            BIGINT   IDENTITY (1, 1) NOT NULL,
    [PetitionID]    BIGINT   NOT NULL,
    [PersonID]      BIGINT   NOT NULL,
    [CertificateID] BIGINT   NOT NULL,
    [CreatedDate]   DATETIME NOT NULL,
    [SignedData]    TEXT     NOT NULL,
    [SignedHash]    TEXT     NOT NULL,
    CONSTRAINT [PK_PetitionVote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PetitionVote_Certificate] FOREIGN KEY ([CertificateID]) REFERENCES [dbo].[Certificate] ([ID]),
    CONSTRAINT [FK_PetitionVote_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [FK_PetitionVote_Petition] FOREIGN KEY ([PetitionID]) REFERENCES [dbo].[Petition] ([ID])
);

