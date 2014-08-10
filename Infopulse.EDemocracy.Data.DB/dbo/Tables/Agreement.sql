CREATE TABLE [dbo].[Agreement] (
    [ID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [ShortDesc] NVARCHAR (MAX) NOT NULL,
    [Text]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Agreement] PRIMARY KEY CLUSTERED ([ID] ASC)
);

