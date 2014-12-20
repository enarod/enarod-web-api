CREATE TABLE [dbo].[Photo] (
    [ID]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [PersonID]    BIGINT   NOT NULL,
    [StatusID]    BIGINT   NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [CreatedBy]   BIGINT   NOT NULL,
    [Photo]       IMAGE    NOT NULL,
    CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Photo_Entity] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Photo_People] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[People] ([ID])
);

