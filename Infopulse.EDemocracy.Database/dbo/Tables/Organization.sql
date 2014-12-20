CREATE TABLE [dbo].[Organization]
(
	[ID] INT NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(4000),
	[Logo] NVARCHAR(MAX),
	[AcceptancePolicy] NVARCHAR(MAX),
	[PreliminaryVoteCount] INT,
	[PreliminaryGatheringDays] INT,
	[VoteCount] INT,
	[GatheringDays] INT,
	CONSTRAINT PK_Organization PRIMARY KEY([ID])
)
