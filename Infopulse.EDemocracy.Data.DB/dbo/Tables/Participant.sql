CREATE TABLE [dbo].[Participant] (
    [ID]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (MAX) NOT NULL,
    [LastName]   NVARCHAR (MAX) NOT NULL,
    [MiddleName] NVARCHAR (MAX) NULL,
    [TaxID]      BIGINT         NULL,
    [Passport]   VARCHAR (8)    NOT NULL,
    [DOB]        DATE           NOT NULL,
    CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED ([ID] ASC)
);

