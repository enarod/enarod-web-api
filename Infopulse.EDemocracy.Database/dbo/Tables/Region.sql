CREATE TABLE [dbo].[Region] (
    [ID]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [LevelID] BIGINT         NOT NULL,
    CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Region_PetitionLevel] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[PetitionLevel] ([ID])
);

