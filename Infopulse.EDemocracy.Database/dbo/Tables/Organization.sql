CREATE TABLE [dbo].[Organization]
(
	[ID] INT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] NVARCHAR(MAX),
	[PrivateDescription] NVARCHAR(MAX),
	[Logo] NVARCHAR(MAX),
	[AcceptancePolicy] NVARCHAR(MAX),
	[PreliminaryVoteCount] INT,
	[PreliminaryGatheringDays] INT,
	[VoteCount] INT,
	[GatheringDays] INT,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[CreatedBy] NVARCHAR(4000) NOT NULL DEFAULT 'Unknown DB user',
	[ModifiedDate] DATETIME2,
	[ModifiedBy] NVARCHAR(4000),
	CONSTRAINT PK_Organization PRIMARY KEY([ID])
)
