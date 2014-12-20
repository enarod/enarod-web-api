CREATE TABLE [dbo].[Groups] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Groups_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID])
);

