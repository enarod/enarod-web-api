CREATE TABLE [dbo].[RegionCandidate] (
    [ID]          BIGINT IDENTITY (1, 1) NOT NULL,
    [RegionID]    BIGINT NOT NULL,
    [CandidateID] BIGINT NOT NULL,
    CONSTRAINT [PK_RegionCandidate] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RegionCandidate_Candidate] FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidate] ([ID]),
    CONSTRAINT [FK_RegionCandidate_Region] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Region] ([ID])
);

