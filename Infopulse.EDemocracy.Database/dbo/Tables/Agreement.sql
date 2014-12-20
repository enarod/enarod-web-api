CREATE TABLE [dbo].[Agreement] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [ShortDesc]   NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [CandidateID] BIGINT         NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    [CreatedDate] DATETIME       NOT NULL,
    [CreatedBy]   BIGINT         NOT NULL,
    [UpdatedDate] DATETIME       NULL,
    [UpdatedBy]   BIGINT         NULL,
    CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Agreement_Candidate] FOREIGN KEY ([CandidateID]) REFERENCES [dbo].[Candidate] ([ID]),
    CONSTRAINT [FK_Agreement_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Agreement_People] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[People] ([ID]),
    CONSTRAINT [FK_Agreement_People1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[People] ([ID])
);

