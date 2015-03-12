CREATE TABLE [dbo].[Settings]
(
	[ID] INT NOT NULL,
	[Key] nvarchar(max) not null,
	[Value] nvarchar(max),
	[Description] nvarchar(max),
	constraint PK_Settings primary key ([ID])
)