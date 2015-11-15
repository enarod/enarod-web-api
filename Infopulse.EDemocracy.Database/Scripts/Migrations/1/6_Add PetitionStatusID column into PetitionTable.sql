CREATE TABLE [dbo].[PetitionStatus] (
    [ID]   INT            NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_PetitionStatus] PRIMARY KEY CLUSTERED ([ID] ASC)
);
go

-- dbo.PetitionStatus
;merge dbo.PetitionStatus as target
using
(
	values
	(1, N'Модерація'),
	(2, N'Сбір підписів'),
	(3, N'На розгляді'),
	(4, N'З відповіддю')
) as source (ID, Name)
on target.ID = source.ID

when matched then
	update
	set
		Name = source.Name

when not matched by target then
	insert (ID, Name)
	values (ID, Name)

when not matched by source then
	delete;



alter table dbo.Petition
add PetitionStatusID int not null default(1)
go

alter table dbo.Petition
add constraint FK_Petition_Status foreign key (PetitionStatusID) references dbo.PetitionStatus(ID)