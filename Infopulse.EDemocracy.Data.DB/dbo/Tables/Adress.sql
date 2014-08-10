CREATE TABLE [dbo].[Adress] (
    [ID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [ParticipantID] BIGINT         NOT NULL,
    [Country]       NVARCHAR (MAX) NOT NULL,
    [ZIPCode]       VARCHAR (10)   NULL,
    [City]          NVARCHAR (MAX) NOT NULL,
    [Street]        NVARCHAR (MAX) NOT NULL,
    [Building]      VARCHAR (8)    NOT NULL,
    [Flat]          VARCHAR (8)    NULL,
    [Actual]        BIT            NOT NULL,
    CONSTRAINT [PK_Adress] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Adress_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ID])
);

