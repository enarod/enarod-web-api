CREATE TABLE [dbo].[Organization] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [Name]                     NVARCHAR (255)  NOT NULL,
    [Description]              NVARCHAR (MAX)  NULL,
    [PrivateDescription]       NVARCHAR (MAX)  NULL,
    [Logo]                     NVARCHAR (MAX)  NULL,
    [AcceptancePolicy]         NVARCHAR (MAX)  NULL,
    [PreliminaryVoteCount]     INT             NULL,
    [PreliminaryGatheringDays] INT             NULL,
    [VoteCount]                INT             NULL,
    [GatheringDays]            INT             NULL,
    [CreatedDate]              DATETIME2 (7)   CONSTRAINT [DF_Organization_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [CreatedBy]                NVARCHAR (4000) CONSTRAINT [DF_Organization_CreatedBy] DEFAULT ('Unknown DB user') NOT NULL,
    [ModifiedDate]             DATETIME2 (7)   NULL,
    [ModifiedBy]               NVARCHAR (4000) NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([ID] ASC)
);


