﻿CREATE TABLE [dbo].[Petition] (
    [ID]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [LevelID]          BIGINT         NOT NULL,
    [AddressedTo]      NVARCHAR (MAX) NOT NULL,
    [Subject]          NVARCHAR (MAX) NOT NULL,
    [CategoryID]       BIGINT         NOT NULL,
    [Text]             NVARCHAR (MAX) NOT NULL,
    [Requirements]     NVARCHAR (MAX) NOT NULL,
    [KeyWords]         NVARCHAR (MAX) NOT NULL,
    [PetitionStatusID] INT            CONSTRAINT [DF_Petition_Status] DEFAULT ((1)) NOT NULL,
    [CreatedDate]      DATETIME       NOT NULL,
    [CreatedBy]        INT            NOT NULL,
    [EffectiveFrom]    DATETIME       NOT NULL,
    [EffectiveTo]      DATETIME       NOT NULL,
    [Limit]            BIGINT         NULL,
    [Email]            NVARCHAR (MAX) NULL,
    [OrganizationID]   INT            NULL,
    [ApprovedBy]       INT            NULL,
    [ApprovedDate]     DATETIME       NULL,
    CONSTRAINT [PK_Petition] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Petition_ApprovedBy] FOREIGN KEY ([ApprovedBy]) REFERENCES [auth].[App_User] ([Id]),
    CONSTRAINT [FK_Petition_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [auth].[App_User] ([Id]),
    CONSTRAINT [FK_Petition_Entity] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Entity] ([ID]),
    CONSTRAINT [FK_Petition_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([ID]),
    CONSTRAINT [FK_Petition_PetitionLevel] FOREIGN KEY ([LevelID]) REFERENCES [dbo].[PetitionLevel] ([ID]),
    CONSTRAINT [FK_Petition_Status] FOREIGN KEY ([PetitionStatusID]) REFERENCES [dbo].[PetitionStatus] ([ID])
);











