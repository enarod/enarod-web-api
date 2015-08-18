alter table dbo.Petition
drop FK_Petition_People

alter table dbo.Petition
alter column CreatedBy int not null

alter table dbo.Petition
add constraint FK_Petition_CreatedBy foreign key (CreatedBy) references auth.App_User