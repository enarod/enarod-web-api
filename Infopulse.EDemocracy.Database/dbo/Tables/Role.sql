CREATE TABLE [dbo].[Role] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (MAX)  NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [StatusID]    BIGINT         NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Role_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID])
);

