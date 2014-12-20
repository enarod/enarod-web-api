CREATE TABLE [dbo].[Candidate] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [PersonID]    BIGINT         NOT NULL,
    [TypeID]      BIGINT         NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Candidate_Entity] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Candidate_Entity1] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Candidate_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

