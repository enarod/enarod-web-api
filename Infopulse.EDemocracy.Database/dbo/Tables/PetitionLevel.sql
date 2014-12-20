CREATE TABLE [dbo].[PetitionLevel] (
    [ID]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NOT NULL,
    [Limit] BIGINT         NOT NULL,
    CONSTRAINT [PK_PetitionLevel] PRIMARY KEY CLUSTERED ([ID] ASC)
);

