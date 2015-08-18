CREATE TABLE [dbo].[UserDetail] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [UserID]       INT             NOT NULL,
    [FirstName]    NVARCHAR (300)  NULL,
    [MiddleName]   NVARCHAR (300)  NULL,
    [LastName]     NVARCHAR (300)  NULL,
    [ZipCode]      NVARCHAR (10)   DEFAULT ('00000') NULL,
    [AddressLine1] NVARCHAR (4000) NULL,
    [AddressLine2] NVARCHAR (4000) NULL,
    [City]         NVARCHAR (300)  NULL,
    [Region]       NVARCHAR (300)  NULL,
    [Country]      NVARCHAR (300)  NULL,
    [CreatedBy]    NVARCHAR (4000) DEFAULT ('Unknown DB user') NOT NULL,
    [CreatedDate]  DATETIME2 (7)   DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]   NVARCHAR (4000) NULL,
    [ModifiedDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserDetail_Auth_App_User] FOREIGN KEY ([UserID]) REFERENCES [auth].[App_User] ([Id]),
    CONSTRAINT [UQ_UserId] UNIQUE NONCLUSTERED ([UserID] ASC)
);

