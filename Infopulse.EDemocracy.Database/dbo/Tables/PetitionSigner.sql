CREATE TABLE [dbo].[PetitionSigner] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (MAX)  NOT NULL,
    [FirstName]    NVARCHAR (300)  NULL,
    [MiddleName]   NVARCHAR (300)  NULL,
    [LastName]     NVARCHAR (300)  NULL,
    [AddressLine1] NVARCHAR (4000) NULL,
    [AddressLine2] NVARCHAR (4000) NULL,
    [City]         NVARCHAR (300)  NULL,
    [Region]       NVARCHAR (300)  NULL,
    [Country]      NVARCHAR (300)  NULL,
    [CreatedBy]    NVARCHAR (4000) CONSTRAINT [DF_PetitionSigner_CreatedBy] DEFAULT ('Unknown DB user') NOT NULL,
    [CreatedDate]  DATETIME2 (7)   CONSTRAINT [DF_PetitionSigner_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (4000) NULL,
    [ModifiedDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_PetitionSigner] PRIMARY KEY CLUSTERED ([ID] ASC)
);

