CREATE TABLE [dbo].[PetitionEmailVote] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PatitionID]  BIGINT        NOT NULL,
    [email]       VARCHAR (MAX) NOT NULL,
    [hash]        VARCHAR (MAX) NOT NULL,
    [CreatedDate] DATETIME      NOT NULL,
    [IsConfirmed] BIT           NOT NULL,
    CONSTRAINT [PK_PetitionEmailVote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PetitionEmailVote_Petition] FOREIGN KEY ([PatitionID]) REFERENCES [dbo].[Petition] ([ID])
);

