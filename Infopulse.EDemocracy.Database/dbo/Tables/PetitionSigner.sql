CREATE TABLE [dbo].[PetitionSigner]
(
	[ID] INT NOT NULL IDENTITY(1, 1),
	[Email] NVARCHAR(MAX) NOT NULL,
	[FirstName] NVARCHAR(300),
	[MiddleName] NVARCHAR(300),
	[LastName] NVARCHAR(300),
	[AddressLine1] NVARCHAR(4000),
	[AddressLine2] NVARCHAR(4000),
	[City] NVARCHAR(300),
	[Region] NVARCHAR(300),
	[Country] NVARCHAR(300),
	[CreatedBy] NVARCHAR(4000) NOT NULL DEFAULT 'Unknown DB user',
	[CreatedDate] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
	[ModifiedBy] NVARCHAR(4000),
	[ModifiedDate] DATETIME2,
	CONSTRAINT PK_PetitionSigner PRIMARY KEY ([Id])
)