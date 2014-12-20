CREATE TABLE [dbo].[Certificate] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [TypeID]       BIGINT        NOT NULL,
    [PersonID]     BIGINT        NOT NULL,
    [SerialNumber] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Certificate] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Certificate_Entity] FOREIGN KEY ([TypeID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Certificate_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

