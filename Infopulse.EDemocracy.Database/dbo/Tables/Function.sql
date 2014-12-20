CREATE TABLE [dbo].[Function] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (MAX)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Function_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID])
);

