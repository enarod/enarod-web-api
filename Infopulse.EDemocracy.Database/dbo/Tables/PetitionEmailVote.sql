CREATE TABLE [dbo].[PetitionEmailVote] (
    [ID]				BIGINT			IDENTITY (1, 1) NOT NULL,
    [PetitionID]		BIGINT			NOT NULL,
    [Email]				VARCHAR (MAX)	NOT NULL,
    [PetitionSignerID]	INT					NULL,
	[Hash]				VARCHAR (MAX)	NOT NULL,
    [CreatedDate]		DATETIME		NOT NULL,
    [IsConfirmed]		BIT				NOT NULL,	
    CONSTRAINT [PK_PetitionEmailVote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PetitionEmailVote_Petition] FOREIGN KEY ([PetitionID]) REFERENCES [dbo].[Petition] ([ID]),
	CONSTRAINT [FK_PetitionEmailVote_PetitionSigner] FOREIGN KEY ([PetitionSignerID]) REFERENCES [dbo].[PetitionSigner]([ID])
)